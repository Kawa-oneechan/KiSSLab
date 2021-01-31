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
		public object Add(params object[] args)
		{
			var ret = Evaluate<int>(args[0]);
			for (var i = 1; i < args.Length; i++)
				ret += Evaluate<int>(args[i]);
			return ret;
		}

		[ScriptFunction("-")]
		public object Subtract(params object[] args)
		{
			var ret = Evaluate<int>(args[0]);
			for (var i = 1; i < args.Length; i++)
				ret -= Evaluate<int>(args[i]);
			return ret;
		}

		[ScriptFunction("*")]
		public object Multiply(params object[] args)
		{
			var ret = Evaluate<int>(args[0]);
			for (var i = 1; i < args.Length; i++)
				ret *= Evaluate<int>(args[i]);
			return ret;
		}

		[ScriptFunction("/")]
		public object Divide(params object[] args)
		{
			var ret = Evaluate<int>(args[0]);
			for (var i = 1; i < args.Length; i++)
			{
				var d = Evaluate<int>(args[i]);
				if (d == 0)
					return 0;
				ret /= d;
			}
			return ret;
		}

		[ScriptFunction("=")]
		public object SetVar(params object[] args)
		{
			var key = args[0] as Symbol;
			var val = Evaluate(args[1]);
			if (key == "true" || key == "false")
				return key;
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("==")]
		public object Equal(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a == b;
		}

		[ScriptFunction("!=")]
		public object NotEqual(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a != b;
		}

		[ScriptFunction("<")]
		public object LowerThan(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a < b;
		}

		[ScriptFunction("<=")]
		public object LowerEqual(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a <= b;
		}

		[ScriptFunction(">")]
		public object GreaterThan(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a > b;
		}

		[ScriptFunction(">=")]
		public object GreaterEqual(params object[] args)
		{
			var a = Evaluate<int>(args[0]);
			var b = Evaluate<int>(args[1]);
			return a >= b;
		}

		[ScriptFunction("not")]
		public object Not(params object[] args)
		{
			var a = Evaluate<bool>(args[0]);
			return !a;
		}

		//and
		
		//or

		[ScriptFunction("if")]
		public object If(params object[] args)
		{
			var expression = Evaluate<bool>(args[0]);
			if (expression)
			{
				for (var i = 1; i < args.Length; i++)
				{
					if (args[i] is Symbol && args[i].ToString() == "else")
						break;
					Evaluate(args[i]);
				}
			}
			else
			{
				for (var i = 1; i < args.Length; i++)
				{
					if (args[i] is Symbol && args[i].ToString() == "else")
					{
						for (var j = i + 1; j < args.Length; j++)
							Evaluate(args[j]);
					}
				}
			}
			return expression;
		}

		[ScriptFunction]
		public object ForEach(params object[] args)
		{
			var what = args[0] as List<object>;
			var list = Evaluate<List<object>>(what[0]);
			var iterator = what[1] as Symbol;
			var block = args.Skip(1).ToList();
			object ret = null;
			foreach (var item in list)
			{
				SetVar(iterator, item);
				foreach (var command in block)
					ret = Evaluate(command);
			}
			return ret;
		}

		private object CelsOrPart(object args)
		{
			args = Evaluate(args);
			if (args is Part)
				return (Part)args;
			if (args is Cel)
				return (Cel)args;
			if (args is List<object>)
				return (List<object>)args;
			var id = Evaluate<string>(args);
			if (id.Contains('/'))
			{
				var objID = id.Remove(id.IndexOf('/'));
				var celID = id.Substring(id.IndexOf('/') + 1);
				var obj = Parts.FirstOrDefault(o => o.ID == objID);
				if (obj == null)
					return null;
				return obj.Cels.Where(o => o.ID == celID).Cast<object>().ToList();
			}
			var cels = Cels.Where(o => o.ID == id).Cast<object>().ToList();
			if (cels.Count == 0)
				return Parts.FirstOrDefault(o => o.ID == id);
			return cels;
		}

		[ScriptFunction("part")]
		public object FindPart(params object[] args)
		{
			var thing = Evaluate(args[0]);
			var thingAsPart = thing as Part;
			if (thingAsPart == null)
			{
				var str = thing.ToString();
				thingAsPart = Parts.FirstOrDefault(o => o.ID == str);
			}
			return thingAsPart;
		}

		[ScriptFunction("cels")]
		public object FindCels(params object[] args)
		{
			var thing = Evaluate(args[0]);
			var cel = thing as Cel;
			if (cel != null)
				return cel;
			var str = thing.ToString();
			var cels = Cels.Where(o => o.ID == str).Cast<object>().ToList();
			if (cels.Count == 1)
				return cels[0];
			return cels;
		}

		/// <summary>
		/// Returns true if a cel is mapped, or all component cels of an object are mapped, false otherwise.
		/// </summary>
		[ScriptFunction("mapped?")]
		public object IsMapped(params object[] args)
		{
			object mapThis = CelsOrPart(args[0]);
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
		public object NumMapped(params object[] args)
		{
			object mapThis = CelsOrPart(args[0]);
			if (mapThis == null)
				return null;
			if (mapThis is Cel)
				return ((Cel)mapThis).Visible ? 1 : 0;
			else if (mapThis is Part)
				return ((Part)mapThis).Cels.Count(c => c.Visible);
			return false;
		}

		[ScriptFunction]
		public object Random(params object[] args)
		{
			if (args.Length == 1)
				return rand.Next(Evaluate<int>(args[0]));
			else if (args.Length > 1)
				return rand.Next(Evaluate<int>(args[0]), Evaluate<int>(args[1]));
			return 0;
		}

		[ScriptFunction]
		public object Music(params object[] args)
		{
			var file = Evaluate<string>(args[0]);
			Viewer.Sound.PlayMusic(file);
			return 0;
		}

		[ScriptFunction]
		public object Sound(params object[] args)
		{
			var file = Evaluate<string>(args[0]);
			return Viewer.Sound.PlaySound(file);
		}

		[ScriptFunction]
		public object StopSound(params object[] args)
		{
			var sound = Evaluate<SoundSystem.Sound>(args[0]);
			sound.Stop();
			return 0;
		}

		/// <summary>
		/// Roughly equivalent to "movebyx(o1,o2,d) movebyy(o1,o2,d)"
		/// </summary>
		[ScriptFunction]
		public object MoveRel(params object[] args)
		{
			if (args[0] is int)
				args = new[] { (Symbol)"#a", (Symbol)"#b", args[0], args[1] };
			var moveRelWhat = (Part)FindPart(args[0]);
			var moveRelTo = (Part)FindPart(args[1]);
			var moveRelByX = Evaluate<int>(args[2]);
			var moveRelByY = Evaluate<int>(args[3]);
			if (!(moveRelWhat == null || moveRelTo == null))
				moveRelWhat.Position = new Point(moveRelTo.Position.X + moveRelByX, moveRelTo.Position.Y + moveRelByY);
			return null;
		}

		[ScriptFunction]
		public object MoveTo(params object[] args)
		{
			if (args[0] is int)
				args = new[] { (Symbol)"#a", args[0], args[1] };
			var str = Evaluate(args[0]).ToString();
			var moveWhat = Parts.FirstOrDefault(o => o.ID == str);
			var moveToX = Evaluate<int>(args[1]);
			var moveToY = Evaluate<int>(args[2]);
			if (moveWhat != null)
				moveWhat.Position = new Point(moveToX, moveToY);
			return null;
		}

		[ScriptFunction]
		public object Map(params object[] args)
		{
			var mapThis = CelsOrPart(args[0]);
			if (mapThis == null)
				return null;
			if (mapThis is Part)
				mapThis = ((Part)mapThis).Cels;
			foreach (var cel in (List<object>)mapThis)
				((Cel)cel).Visible = true;
			return mapThis;
		}

		[ScriptFunction]
		public object UnMap(params object[] args)
		{
			var mapThis = CelsOrPart(args[0]);
			if (mapThis == null)
				return null;
			if (mapThis is Part)
				mapThis = ((Part)mapThis).Cels;
			foreach (var cel in (List<object>)mapThis)
				((Cel)cel).Visible = false;
			return mapThis;
		}

		[ScriptFunction]
		public object AltMap(params object[] args)
		{
			var mapThis = CelsOrPart(args[0]);
			if (mapThis == null)
				return null;
			if (mapThis is Part)
				mapThis = ((Part)mapThis).Cels;
			foreach (var cel in (List<object>)mapThis)
				((Cel)cel).Visible = !((Cel)cel).Visible;
			return mapThis;
		}

		[ScriptFunction]
		public object Ghost(params object[] args)
		{
			var ghostThis = CelsOrPart(args[0]);
			if (ghostThis == null)
				return null;
			if (ghostThis is Part)
				ghostThis = ((Part)ghostThis).Cels;
			var tOrF = args.Length > 1 ? (Evaluate<int>(args[1]) > 0) : true;
			foreach (var cel in (List<Cel>)ghostThis)
				cel.Ghost = tOrF;
			return ghostThis;
		}

		[ScriptFunction]
		public object Timer(params object[] args)
		{
			if (!(args[0] is int || args[0] is string || args[0] is Symbol))
			{
				//	MessageBox.Show(string.Format("Malformed \"timer\" command. An integer or string ID is expected, but got a {0}.", what[1].GetType().Name), Application.ProductName);
				//	continue;
			}
			var timerID = args[0].GetHashCode();
			if (!Timers.ContainsKey(timerID))
				Timers.Add(timerID, new Timer());
			var timer = Timers[timerID];
			var delay = Evaluate<int>(args[1]);
			if (delay <= 0)
			{
				timer.Action = null;
				timer.Interval = 0;
				timer.Repeat = false;
				return timer;
			}
			timer.Action = args[0];
			timer.Interval = timer.TimeLeft = delay;
			timer.Repeat = (args.Length == 3 && args[2] is Symbol && args[2].ToString() == "repeat");
			return timer;
		}

		[ScriptFunction]
		public object Notify(params object[] args)
		{
			//Concat up all what?
			DarkUI.Forms.DarkMessageBox.ShowInformation(Evaluate(args[0]).ToString(), Application.ProductName);
			return null;
		}
	}
}
