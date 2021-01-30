using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DarkUI.Controls;
using Kawa.SExpressions;

namespace KiSSLab
{
	public class Timer
	{
		public int Interval { get; set; }
		public int TimeLeft { get; set; }
		public bool Repeat { get; set; }
		public object Action { get; set; }
	}

	public partial class Scene
	{
		public Dictionary<string, List<object>> Events { get; set; }
		public Dictionary<int, Timer> Timers { get; set; }
		public Dictionary<int, string> HashCodes;

		private Random rand = new Random();
		private Dictionary<string, object> scriptFunctions;
		private Dictionary<Symbol, object> scriptVariables;

		void LoadEvents(List<object> eventsNode)
		{
			scriptFunctions = new Dictionary<string, object>();
			var methods = typeof(Scene).GetMethods();
			foreach (var method in methods)
			{
				var attribs = method.GetCustomAttributes(true).OfType<ScriptFunctionAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var name = ((ScriptFunctionAttribute)attribs[0]).As;
				if (string.IsNullOrEmpty(name))
					name = method.Name.ToLowerInvariant();
				scriptFunctions.Add(name, method);
			}

			var scriptEvents = new Dictionary<string, object>();
			foreach (var method in methods)
			{
				var attribs = method.GetCustomAttributes(true).OfType<ScriptEventAttribute>().ToArray();
				if (attribs.Length == 0)
					continue;
				var name = ((ScriptEventAttribute)attribs[0]).As;
				if (string.IsNullOrEmpty(name))
					name = method.Name.ToLowerInvariant();
				scriptEvents.Add(name, method);
			}

			Func<string, List<object>, bool> addEvent = null;
			addEvent = (s, e) =>
			{
				if (Events.ContainsKey(s))
				{
					Events[s].AddRange(e);
					return false;
				}
				else
					Events[s] = e;
				return true;
			};

			foreach (var ev in eventsNode.Skip(1).Cast<List<object>>())
			{
				var trigger = ev[0] as List<object>;
				var result = ev.Skip(1).ToList();//[0] as List<object>;
				var trigForm = trigger[0] as Symbol;

				if (scriptEvents.ContainsKey(trigForm))
				{
					var key = ((System.Reflection.MethodInfo)scriptEvents[trigForm]).Invoke(this, new[] { trigger.ToArray() }).ToString();
					if (key == null)
						continue;
					addEvent(key, result);
				}
			}
		}

		private bool IsSafeObject(object obj)
		{
			return obj is Part || obj is Cell;
		}

		private object Send(object obj, List<object> cmd)
		{
			var properties = obj.GetType().GetProperties();
			for (var i = 1; i < cmd.Count; i++)
			{
				if (cmd[i] is Symbol && cmd[i].ToString().EndsWith(":"))
				{
					var propName = cmd[i].ToString();
					propName = propName.Remove(propName.Length - 1);
					var property = properties.FirstOrDefault(p => p.Name.Equals(propName, StringComparison.InvariantCultureIgnoreCase));

					//is there more?
					if (i + 1 < cmd.Count)
					{
						//followed by another potential property, so it's a getter.
						if (cmd[i + 1] is Symbol && cmd[i + 1].ToString().EndsWith(":"))
							continue;
						//this must be a value to set.
						i++;
						var valueToSet = Evaluate(cmd[i]);
						property.SetValue(obj, valueToSet, null);
						return valueToSet;
					}
					else
					{
						//This is a value to *get*.
						var valueGotten = property.GetValue(obj, null);
						return valueGotten;
					}
				}
			}
			return null;
		}

		public object Evaluate(object thing)
		{
			if (thing is List<object>)
			{
				var cmd = (List<object>)thing;
				if (cmd[0] is List<object>)
				{
					if (((List<object>)cmd[0])[0] is Symbol)
						cmd[0] = Evaluate(cmd[0]);
				}
				if (cmd[0] is Symbol)
				{
					var form = (cmd[0] as Symbol).ToString();
					if (scriptFunctions.ContainsKey(form))
					{
						return ((System.Reflection.MethodInfo)scriptFunctions[form]).Invoke(this, new[] { cmd.ToArray() });
					}
					else if (scriptVariables.ContainsKey(form))
					{
						var obj = scriptVariables[form];
						if (IsSafeObject(obj))
							cmd[0] = obj;
					}
				}
				if (IsSafeObject(cmd[0]))
					return Send(cmd[0], cmd);
			}
			else if (thing is Symbol)
			{
				//Handle variables
				if (scriptVariables.ContainsKey((Symbol)thing))
					return scriptVariables[(Symbol)thing];
				return thing;
			}
			return thing;
		}

		public T Evaluate<T>(object thing)
		{
			var ret = Evaluate(thing);
			if (typeof(T).Name == "Boolean" && ret is int)
			{
				ret = (int)ret == 1;
			}
			if (!(ret is T))
			{
				throw new InvalidCastException(string.Format("Evaluate<{0}> got a {1} instead.", typeof(T).Name, ret.GetType().Name));
			}
			return (T)ret;
		}

		public bool RunEvent(List<object> ev)
		{
			object ret = null;
			foreach (var cmd in ev.Cast<List<object>>())
			{
				ret = Evaluate(cmd);
			}
			return true;
		}

		public void Release(Part held, Cell cell)
		{
			//var somethingHappened = false;
			var somethingCollided = false;

			if (held == null)
				return;

			//FIXDROP: applies to FIXED
			//DROP: applies to all with a LESS THAN MAX FIX
			//RELEASE: always applies
			var fix = held.Locked ? "fixdrop" : "drop";
			var maybe = string.Format("{0}|{1}", fix, cell.ID);
			if (Events.ContainsKey(maybe))
			{
				RunEvent(Events[maybe]);
				Viewer.DrawScene();
				return;
			}
			maybe = string.Format("release|{0}", cell.ID);
			if (Events.ContainsKey(maybe))
			{
				RunEvent(Events[maybe]);
				Viewer.DrawScene();
				return;
			}

			foreach (var other in Parts)
			{
				if (other == held)
					continue;
				if (Tools.Overlap(held.Position, other.Position, held.Bounds, other.Bounds))
				{
					scriptVariables["#a"] = held.ID;
					scriptVariables["#b"] = other.ID;
					maybe = string.Format("collide|{0}|{1}", held.ID, other.ID);

					if (other.ID == "body")
						Clipboard.SetText(string.Format("((collide \"{0}\" \"{1}\") (moverel {2} {3}))", held.ID, other.ID, held.Position.X - other.Position.X, held.Position.Y - other.Position.Y));

					if (Events.ContainsKey(maybe))
					{
						if (!Tools.PixelOverlap(held, other))
							continue;

						if (other == held.LastCollidedWith)
						{
							somethingCollided = true;
							continue;
						}

						held.LastCollidedWith = other;
						somethingCollided = true;
						//somethingHappened = true;
						if (RunEvent(Events[maybe]))
							break;
					}
					maybe = string.Format("in|{0}|{1}", held.ID, other.ID);
					if (Events.ContainsKey(maybe))
					{
						if (other == held.LastCollidedWith)
						{
							somethingCollided = true;
							continue;
						}

						held.LastCollidedWith = other;
						somethingCollided = true;
						//somethingHappened = true;
						if (RunEvent(Events[maybe]))
							break;
					}
				}
			}
			if (!somethingCollided)
				held.LastCollidedWith = null;
			//if (somethingHappened)
				//Viewer.DrawScene();

		}

		public void Catch(Part held, Cell cell)
		{
			//FIXCATCH: applies to FIXED
			//CATCH: applies to all with a LESS THAN MAX FIX
			//PRESS: always applies
			var fix = held.Locked ? "fixcatch" : "catch";
			var maybe = string.Format("{0}|{1}", fix, cell.ID);
			if (Events.ContainsKey(maybe))
			{
				RunEvent(Events[maybe]);
				Viewer.DrawScene();
				return;
			}
			maybe = string.Format("press|{0}", cell.ID);
			if (Events.ContainsKey(maybe))
			{
				RunEvent(Events[maybe]);
				Viewer.DrawScene();
			}
		}

		public void Click(Cell cell)
		{
			var maybe = string.Format("click|{0}", cell.ID);
			if (Events.ContainsKey(maybe))
				RunEvent(Events[maybe]);
			else
			{
				maybe = string.Format("click|{0}", cell.Part.ID);
				if (Events.ContainsKey(maybe))
					RunEvent(Events[maybe]);
			}
		}

		public class ScriptFunctionAttribute : Attribute
		{
			public string As { get; private set; }
			public ScriptFunctionAttribute()
			{
			}
			public ScriptFunctionAttribute(string name)
			{
				As = name;
			}
		}

		public class ScriptEventAttribute : Attribute
		{
			public string As { get; private set; }
			public ScriptEventAttribute()
			{
			}
			public ScriptEventAttribute(string name)
			{
				As = name;
			}
		}
	}
}
