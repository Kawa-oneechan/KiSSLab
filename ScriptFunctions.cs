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
	public partial class Scene
	{
		[ScriptFunction("+")]
		public object Add(params object[] cmd)
		{
			var ret = Evaluate<int>(cmd[1]);
			for (var i = 2; i < cmd.Length; i++)
				ret += Evaluate<int>(cmd[i]);
			return ret;
		}

		[ScriptFunction("-")]
		public object Subtract(params object[] cmd)
		{
			var ret = Evaluate<int>(cmd[1]);
			for (var i = 2; i < cmd.Length; i++)
				ret -= Evaluate<int>(cmd[i]);
			return ret;
		}

		[ScriptFunction("*")]
		public object Multiply(params object[] cmd)
		{
			var ret = Evaluate<int>(cmd[1]);
			for (var i = 2; i < cmd.Length; i++)
				ret *= Evaluate<int>(cmd[i]);
			return ret;
		}

		[ScriptFunction("/")]
		public object Divide(params object[] cmd)
		{
			var ret = Evaluate<int>(cmd[1]);
			for (var i = 2; i < cmd.Length; i++)
			{
				var d = Evaluate<int>(cmd[i]);
				if (d == 0)
					return 0;
				ret /= d;
			}
			return ret;
		}

		[ScriptFunction("=")]
		public object SetVar(params object[] cmd)
		{
			var key = cmd[1] as Symbol;
			var val = Evaluate(cmd[2]);
			if (key == "true" || key == "false")
				return key;
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("==")]
		public object Equal(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a == b;
		}

		[ScriptFunction("!=")]
		public object NotEqual(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a != b;
		}

		[ScriptFunction("<")]
		public object LowerThan(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a < b;
		}

		[ScriptFunction("<=")]
		public object LowerEqual(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a <= b;
		}

		[ScriptFunction(">")]
		public object GreaterThan(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a > b;
		}

		[ScriptFunction(">=")]
		public object GreaterEqual(params object[] cmd)
		{
			var a = Evaluate<int>(cmd[1]);
			var b = Evaluate<int>(cmd[2]);
			return a >= b;
		}

		[ScriptFunction("not")]
		public object Not(params object[] cmd)
		{
			var a = Evaluate<bool>(cmd[1]);
			return !a;
		}

		//and
		
		//or

		[ScriptFunction("if")]
		public object If(params object[] cmd)
		{
			var expression = Evaluate<bool>(cmd[1]);
			if (expression)
			{
				for (var i = 2; i < cmd.Length; i++)
				{
					if (cmd[i] is Symbol && cmd[i].ToString() == "else")
						break;
					Evaluate(cmd[i]);
				}
			}
			else
			{
				for (var i = 2; i < cmd.Length; i++)
				{
					if (cmd[i] is Symbol && cmd[i].ToString() == "else")
					{
						for (var j = i + 1; j < cmd.Length; j++)
							Evaluate(cmd[j]);
					}
				}
			}
			return expression;
		}

		private object CelOrPart(object thing)
		{
			thing = Evaluate(thing);
			if (thing is Part)
				return (Part)thing;
			if (thing is Cel)
				return (Cel)thing;
			var id = Evaluate<string>(thing);
			object found = Parts.FirstOrDefault(o => o.ID == id);
			if (found == null)
				found = Cels.FirstOrDefault(o => o.ID == id);
			return found;
		}

		[ScriptFunction("part")]
		public object FindPart(params object[] cmd)
		{
			var thing = Evaluate(cmd[1]);
			var thingAsPart = thing as Part;
			if (thingAsPart == null)
			{
				var str = thing.ToString();
				thingAsPart = Parts.FirstOrDefault(o => o.ID == str);
			}
			return thingAsPart;
		}

		[ScriptFunction("cel")]
		public object FindCel(params object[] cmd)
		{
			var thing = Evaluate(cmd[1]);
			var thingAsPart = thing as Cel;
			if (thingAsPart == null)
			{
				var str = thing.ToString();
				thingAsPart = Cels.FirstOrDefault(o => o.ID == str);
			}
			return thingAsPart;
		}

		/// <summary>
		/// Returns true if a cel is mapped, or all component cels of an object are mapped, false otherwise.
		/// </summary>
		[ScriptFunction("mapped?")]
		public object IsMapped(params object[] cmd)
		{
			object mapThis = CelOrPart(cmd[1]);
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				return ((Cel)mapThis).Visible;
			else if (mapThis is Part)
				return ((Part)mapThis).Cels.All(c => c.Visible);
			return false;
		}

		/// <summary>
		/// Returns the number of component cels of an object that are mapped.
		/// </summary>
		[ScriptFunction]
		public object NumMapped(params object[] cmd)
		{
			object mapThis = CelOrPart(cmd[1]);
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				return ((Cel)mapThis).Visible ? 1 : 0;
			else if (mapThis is Part)
				return ((Part)mapThis).Cels.Count(c => c.Visible);
			return false;
		}

		[ScriptFunction]
		public object Random(params object[] cmd)
		{
			if (cmd.Length == 2)
				return rand.Next(Evaluate<int>(cmd[1]));
			else if (cmd.Length > 2)
				return rand.Next(Evaluate<int>(cmd[1]), Evaluate<int>(cmd[2]));
			return 0;
		}

		[ScriptFunction]
		public object Music(params object[] cmd)
		{
			var file = Evaluate<string>(cmd[1]);
			Viewer.Sound.PlayMusic(file);
			return 0;
		}

		[ScriptFunction]
		public object Sound(params object[] cmd)
		{
			var file = Evaluate<string>(cmd[1]);
			return Viewer.Sound.PlaySound(file);
		}

		[ScriptFunction]
		public object StopSound(params object[] cmd)
		{
			var sound = Evaluate<SoundSystem.Sound>(cmd[1]);
			sound.Stop();
			return 0;
		}

		/// <summary>
		/// Roughly equivalent to "movebyx(o1,o2,d) movebyy(o1,o2,d)"
		/// </summary>
		[ScriptFunction]
		public object MoveRel(params object[] cmd)
		{
			if (cmd[1] is int)
			{
				cmd = new[] { cmd[0], (Symbol)"#a", (Symbol)"#b", cmd[1], cmd[2] };
			}

			var maybeWhat = Evaluate(cmd[1]);
			var maybeTo = Evaluate(cmd[2]);

			var moveRelWhat = maybeWhat as Part;
			var moveRelTo = maybeTo as Part;
			if (moveRelWhat == null)
			{
				var str = maybeWhat.ToString();
				moveRelWhat = Parts.FirstOrDefault(o => o.ID == str);
			}
			if (moveRelTo == null)
			{
				var str = maybeTo.ToString();
				moveRelTo = Parts.FirstOrDefault(o => o.ID == str);
			}
			var moveRelByX = Evaluate<int>(cmd[3]);
			var moveRelByY = Evaluate<int>(cmd[4]);
			if (!(moveRelWhat == null || moveRelTo == null))
				moveRelWhat.Position = new Point(moveRelTo.Position.X + moveRelByX, moveRelTo.Position.Y + moveRelByY);
			return null;
		}

		[ScriptFunction]
		public object MoveTo(params object[] cmd)
		{
			if (cmd[1] is int)
			{
				cmd = new[] { cmd[0], (Symbol)"#a", cmd[1], cmd[2] };
			}

			var str = Evaluate(cmd[1]).ToString();
			var moveWhat = Parts.FirstOrDefault(o => o.ID == str);
			var moveToX = Evaluate<int>(cmd[2]);
			var moveToY = Evaluate<int>(cmd[3]);
			if (moveWhat != null)
				moveWhat.Position = new Point(moveToX, moveToY);
			return null;
		}

		[ScriptFunction]
		public object Map(params object[] cmd)
		{
			object mapThis = Cels.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				((Cel)mapThis).Visible = true;
			else if (mapThis is Part)
				((Part)mapThis).Visible = true;
			return mapThis;
		}

		[ScriptFunction]
		public object UnMap(params object[] cmd)
		{
			object mapThis = Cels.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				((Cel)mapThis).Visible = false;
			else if (mapThis is Part)
				((Part)mapThis).Visible = false;
			return mapThis;
		}

		[ScriptFunction]
		public object AltMap(params object[] cmd)
		{
			object mapThis = Cels.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				((Cel)mapThis).Visible = !((Cel)mapThis).Visible;
			else if (mapThis is Part)
			{
				foreach (var cel in ((Part)mapThis).Cels)
					cel.Visible = !cel.Visible;
				//((Part)mapThis).Visible = !((Part)mapThis).Visible;
			}
			return mapThis;
		}

		[ScriptFunction]
		public object Ghost(params object[] cmd)
		{
			object ghostThis = Cels.FirstOrDefault(o => o.ID == cmd[1].ToString());
			var tOrF = cmd.Length > 2 ? (Evaluate<int>(cmd[2]) > 0) : true;
			if (ghostThis == null)
				return null;
			if (ghostThis is Cel)
				((Cel)ghostThis).Ghost = tOrF;
			else if (ghostThis is Part)
			{
				foreach (var c in ((Part)ghostThis).Cels)
					c.Ghost = tOrF;
			}
			return ghostThis;
		}

		[ScriptFunction]
		public object Timer(params object[] cmd)
		{
			var delay = Evaluate<int>(cmd[1]); //(int)cmd[1];
			if (!(cmd[2] is int || cmd[2] is string || cmd[2] is Symbol))
			{
				//	MessageBox.Show(string.Format("Malformed \"timer\" command. An integer or string ID is expected, but got a {0}.", cmd[2].GetType().Name), Application.ProductName);
				//	continue;
			}
			var timerID = cmd[2].GetHashCode();
			if (!Timers.ContainsKey(timerID))
				Timers.Add(timerID, new Timer());
			var timer = Timers[timerID];
			if (delay <= 0)
			{
				timer.Action = null;
				timer.Interval = 0;
				timer.Repeat = false;
				return timer;
			}
			timer.Action = cmd[2];
			timer.Interval = timer.TimeLeft = delay;
			timer.Repeat = (cmd.Length == 4 && cmd[3] is Symbol && cmd[3].ToString() == "repeat");
			return timer;
		}

		[ScriptFunction]
		public object Notify(params object[] cmd)
		{
			DarkUI.Forms.DarkMessageBox.ShowInformation(Evaluate(cmd[1]).ToString(), Application.ProductName);
			return null;
		}
	}
}
