﻿using System;
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
			var maybe = ConvertSafeListToObj(obj);
			return obj is Part || obj is Cel || maybe is List<object>;
		}

		private List<object> ConvertSafeListToObj(object obj)
		{
			if (obj is List<Part>)
				return ((List<Part>)obj).Cast<object>().ToList();
			if (obj is List<Cel>)
				return ((List<Cel>)obj).Cast<object>().ToList();
			return null;
		}

		private object Send(object obj, List<object> cmd)
		{
			object ret = null;
			var properties = obj.GetType().GetProperties();
			for (var i = 1; i < cmd.Count; i++)
			{
				if (cmd[i] is Symbol && cmd[i].ToString().EndsWith(":"))
				{
					var propName = cmd[i].ToString();
					propName = propName.Remove(propName.Length - 1).ToLowerInvariant();
					var property = properties.FirstOrDefault(p => p.Name.Equals(propName, StringComparison.InvariantCultureIgnoreCase));
					var safe = property.GetCustomAttributes(true).OfType<ScriptPropertyAttribute>().Count() == 1;
					//Allow getting the count of a list even without the attribute.
					if (obj.GetType().Name == "List`1" && propName == "count")
						safe = true;

					//is there more?
					if (i + 1 < cmd.Count)
					{
						//followed by another potential property, so it's a getter.
						if (cmd[i + 1] is Symbol && cmd[i + 1].ToString().EndsWith(":"))
							continue;
						//this must be a value to set.
						i++;
						var valueToSet = Evaluate(cmd[i]);
						if (safe)
							property.SetValue(obj, valueToSet, null);
						else
							ret = null;
						ret = valueToSet;
					}
					else
					{
						//This is a value to *get*.
						ret = safe ? property.GetValue(obj, null) : null;
					}
				}
			}
			return ret;
		}

		public object Evaluate(object thing)
		{
			var workingWith = thing;
			if (thing is List<object>)
			{
				var cmd = (List<object>)thing;
				workingWith = cmd[0];
				if (workingWith is List<object>)
				{
					if (((List<object>)cmd[0]) is List<object>)
						workingWith = Evaluate(cmd[0]);
				}
				if (workingWith is Symbol)
				{
					var form = (workingWith as Symbol).ToString();
					if (scriptFunctions.ContainsKey(form))
					{
						var mi = (System.Reflection.MethodInfo)scriptFunctions[form];
						return mi.Invoke(this, new[] { cmd.Skip(1).ToArray() });
					}
					else if (scriptVariables.ContainsKey(form))
					{
						var obj = scriptVariables[form];
						if (IsSafeObject(obj))
							workingWith = obj;
					}
				}
				if (IsSafeObject(workingWith))
					return Send(workingWith, cmd);
			}
			else if (thing is Symbol)
			{
				//Handle variables
				if ((Symbol)thing == "set")
					return Set;
				else if ((Symbol)thing == "pal")
					return Palette;
				else if (scriptVariables.ContainsKey((Symbol)thing))
					return scriptVariables[(Symbol)thing];
				return thing;
			}
			return thing;
		}

		public T Evaluate<T>(object thing)
		{
			var ret = Evaluate(thing);
			if (typeof(T).Name == "Boolean")
			{
				if (ret is int)
					ret = (int)ret >= 1;
				else if (ret is List<object>)
					ret = ((List<object>)ret).Count > 0;
			}
			else if (typeof(T).Name == "List`1" && ret.GetType().Name == "List`1")
			{
				ret = ConvertSafeListToObj(ret);
			}
			else if (!(ret is T))
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

		public bool RunEvent(string ev)
		{
			if (Events.ContainsKey(ev))
				return RunEvent(Events[ev]);
			return false;
		}

		public bool RunEvent(string ev, params object[] args)
		{
			return RunEvent(string.Format(ev, args));
		}

		public void Release(Part held, Cel cel)
		{
			//var somethingHappened = false;
			var somethingCollided = false;

			if (held == null)
			{
				if (cel == null)
					return;
				held = cel.Part;
				if (held == null)
					return;
			}

			//FIXDROP: applies to FIXED
			//DROP: applies to all with a LESS THAN MAX FIX
			//RELEASE: always applies
			var fix = held.Fix > 0 ? "fixdrop" : "drop";
			if (RunEvent("{0}|{1}", fix, cel.ID))
			{
				Viewer.DrawScene();
				return;
			}
			else
			{
				if (RunEvent("{0}|{1}", fix, held.ID))
				{
					Viewer.DrawScene();
					return;
				}
			}
			if (RunEvent("release|{0}", cel.ID))
			{
				Viewer.DrawScene();
				return;
			}
			else
			{
				if (RunEvent("release|{0}", held.ID))
				{
					Viewer.DrawScene();
					return;
				}
			}

			foreach (var other in Parts)
			{
				if (other == held)
					continue;
				if (Tools.Overlap(held.Position, other.Position, held.Bounds, other.Bounds))
				{
					scriptVariables["#a"] = held.ID;
					scriptVariables["#b"] = other.ID;
					var maybe = string.Format("collide|{0}|{1}", held.ID, other.ID);

					if (!string.IsNullOrWhiteSpace(Viewer.Config.AutoCollide) && other.ID == Viewer.Config.AutoCollide)
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

		public void Catch(Part held, Cel cel)
		{
			//FIXCATCH: applies to FIXED
			//CATCH: applies to all with a LESS THAN MAX FIX
			//PRESS: always applies
			var fix = held.Fix > 0 ? "fixcatch" : "catch";
			if (RunEvent("{0}|{1}", fix, cel.ID))
			{
				Viewer.DrawScene();
				return;
			}
			else
			{
				if (RunEvent("{0}|{1}", fix, held.ID))
				{
					Viewer.DrawScene();
					return;
				}
			}
			if (RunEvent("press|{0}", cel.ID))
			{
				Viewer.DrawScene();
			}
			else
			{
				if (RunEvent("press|{0}", held.ID))
					Viewer.DrawScene();
			}
		}

		public void Key(bool up, string key)
		{
			//TODO: consider passing key as a meta variable instead, encourage switch use.
			if (RunEvent("key{1}|{0}", key, up ? "release" : "press"))
				Viewer.DrawScene();
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

	public class ScriptPropertyAttribute : Attribute
	{
	}
}
