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
			scriptVariables = new Dictionary<Symbol, object>();

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
				//TODO: improve this.
				if (trigForm == "collide" || trigForm == "in")
				{
					var collideA = trigger[1] as string;
					var collideB = trigger[2] as string;
					addEvent(string.Format("{0}|{1}|{2}", trigForm, collideA, collideB), result);
				}
				else if (trigForm == "initialize")
				{
					addEvent(string.Format("initialize"), result);
				}
				else if (trigForm == "click")
				{
					var clickOn = trigger[1] as string;
					addEvent(string.Format("catch|{0}", clickOn), result);
				}
				else if (trigForm == "catch")
				{
					var clickOn = trigger[1] as string;
					addEvent(string.Format("catch|{0}", clickOn), result);
				}
				else if (trigForm == "fixcatch")
				{
					var clickOn = trigger[1] as string;
					addEvent(string.Format("fixcatch|{0}", clickOn), result);
				}
				else if (trigForm == "release")
				{
					var clickOn = trigger[1] as string;
					addEvent(string.Format("release|{0}", clickOn), result);
				}
				else if (trigForm == "fixrelease")
				{
					var clickOn = trigger[1] as string;
					addEvent(string.Format("fixrelease|{0}", clickOn), result);
				}
				else if (trigForm == "alarm")
				{
					if (!(trigger[1] is int || trigger[1] is string || trigger[1] is Symbol))
					{
						DarkUI.Forms.DarkMessageBox.ShowError(string.Format("Malformed \"alarm\" event. An integer or string ID is expected, but got a {0}.", trigger[1].GetType().Name), Application.ProductName);
						continue;
					}
					var timerID = trigger[1].GetHashCode();
					HashCodes[timerID] = trigger[1].ToString();
					if (trigger[1] is string)
						HashCodes[timerID] = string.Format("\"{0}\"", trigger[1]);
					addEvent(string.Format("alarm|{0}", timerID), result);
				}
			}
		}

		public object Evaluate(object thing)
		{
			if (thing is List<object>)
			{
				var cmd = (List<object>)thing;
				var form = (cmd[0] as Symbol).ToString(); ;

				if (scriptFunctions.ContainsKey(form))
				{
					return ((System.Reflection.MethodInfo)scriptFunctions[form]).Invoke(this, new[] { cmd.ToArray() });
				}
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

			if (cell != null)
			{
				var maybe = string.Format("release|{0}", cell.ID);
				if (held != null)
					maybe = string.Format("{0}|{1}", held.Locked ? "fixrelease" : "release", held.ID);
				if (Events.ContainsKey(maybe))
				{
					RunEvent(Events[maybe]);
					return;
				}
			}
			if (held == null)
				return;

			foreach (var other in Parts)
			{
				if (other == held)
					continue;
				if (Tools.Overlap(held.Position, other.Position, held.Bounds, other.Bounds))
				{
					scriptVariables["#a"] = held.ID;
					scriptVariables["#b"] = other.ID;
					var maybe = string.Format("collide|{0}|{1}", held.ID, other.ID);

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
			var maybe = string.Format("catch|{0}", cell.ID);
			if (Events.ContainsKey(maybe))
				RunEvent(Events[maybe]);
			maybe = string.Format("{0}|{1}", held.Locked ? "fixcatch" : "catch", held.ID);
			if (Events.ContainsKey(maybe))
				RunEvent(Events[maybe]);
			Viewer.DrawScene();
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
	}
}
