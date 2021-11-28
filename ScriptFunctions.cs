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

		[ScriptFunction("%")]
		public object Modulo(params object[] args)
		{
			var val = Evaluate<int>(args[0]);
			var mod = Evaluate<int>(args[1]);
			return val % mod;
		}

		[ScriptFunction("=")]
		public object SetVar(params object[] args)
		{
			var key = args[0] as Symbol;
			var val = Evaluate(args[1]);
			if (key == "true" || key == "false")
				return key;
			else if (key == "set")
				return ChangeSet(val);
			else if (key == "pal")
				return ChangeCol(val);
			else
				scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("++")]
		public object Increment(params object[] args)
		{
			var key = args[0] as Symbol;
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			val++;
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("--")]
		public object Decrement(params object[] args)
		{
			var key = args[0] as Symbol;
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			val--;
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("+=")]
		public object AddAssign(params object[] args)
		{
			var key = Evaluate<Symbol>(args[0]);
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			foreach (var arg in args.Skip(1))
				val += Evaluate<int>(arg);
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("-=")]
		public object SubAssign(params object[] args)
		{
			var key = Evaluate<Symbol>(args[0]);
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			foreach (var arg in args.Skip(1))
				val -= Evaluate<int>(arg);
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("*=")]
		public object MulAssign(params object[] args)
		{
			var key = Evaluate<Symbol>(args[0]);
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			foreach (var arg in args.Skip(1))
				val *= Evaluate<int>(arg);
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("/=")]
		public object DivAssign(params object[] args)
		{
			var key = Evaluate<Symbol>(args[0]);
			if (key == "true" || key == "false")
				return key;
			var val = (int)scriptVariables[key];
			foreach (var arg in args.Skip(1))
			{
				var div = Evaluate<int>(arg);
				if (div == 0)
				{
					val = 0;
					break;
				}
				val /= div;
			}
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("%=")]
		public object ModAssign(params object[] args)
		{
			var key = Evaluate<Symbol>(args[0]);
			if (key == "true" || key == "false")
				return key;
			var mod = Evaluate<int>(args[1]);
			var val = (int)scriptVariables[key];
			val %= mod;
			scriptVariables[key] = val;
			return key;
		}

		[ScriptFunction("==")]
		public object Equal(params object[] args)
		{
			var a = Evaluate(args[0]);
			var b = Evaluate(args[1]);
			if (a is string)
				return ((string)a).Equals(b.ToString());
			if (a is int && b is int)
				return (int)a == (int)b;
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

		[ScriptFunction]
		public object Not(params object[] args)
		{
			var a = Evaluate<bool>(args[0]);
			return !a;
		}

		[ScriptFunction]
		public object And(params object[] args)
		{
			return args.All(a => Evaluate<bool>(a));
		}

		[ScriptFunction]
		public object Or(params object[] args)
		{
			return args.Any(a => Evaluate<bool>(a));
		}

		/// <summary>
		/// Returns true if the argument is null, or an empty list.
		/// </summary>
		[ScriptFunction("nul?")]
		public object IsNull(params object[] args)
		{
			if (args[0] == null)
				return true;
			args[0] = Evaluate(args[0]);
			if (args[0] is List<object> && ((List<object>)args[0]).Count == 0)
				return true;
			return false;
		}

		/// <summary>
		/// Returns true if the argument is a list
		/// </summary>
		[ScriptFunction("list?")]
		public object IsList(params object[] args)
		{
			return args[0] is List<object>;
		}

		/// <summary>
		/// Returns true if the argument is a cel
		/// </summary>
		[ScriptFunction("cel?")]
		public object IsCel(params object[] args)
		{
			return args[0] is Cel;
		}

		/// <summary>
		/// Returns true if the argument is null
		/// </summary>
		[ScriptFunction("part?")]
		public object IsPart(params object[] args)
		{
			return args[0] is Part;
		}

		[ScriptFunction]
		public object Cat(params object[] args)
		{
			var sb = new StringBuilder();
			foreach (var arg in args)
				sb.Append(Evaluate(arg));
			return sb.ToString();
		}

		[ScriptFunction]
		public object Trim(params object[] args)
		{
			return Evaluate(args[0]).ToString().Trim();
		}

		[ScriptFunction]
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
		public object Cond(params object[] args)
		{
			object ret = null;
			for (var i = 0; i < args.Length; i++)
			{
				var c = args[i] as List<object>;
				if (c == null)
				{
					Notify("Malformed cond statement.");
					return null;
				}
				if ((c[0] is Symbol && (Symbol)c[0] == "else") || Evaluate<bool>(c[0]))
				{
					for (var j = 1; j < c.Count; j++)
						ret = Evaluate(c[j]);
					return ret;
				}
			}
			return ret;
		}

		[ScriptFunction]
		public object Switch(params object[] args)
		{
			object ret = null;
			var a = args[0];
			for (var i = 1; i < args.Length; i++)
			{
				var c = args[i] as List<object>;
				if (c == null)
				{
					Notify("Malformed switch statement.");
					return null;
				}
				var b = c[0];
				if ((c[0] is Symbol && (Symbol)c[0] == "else") || (bool)Equal(a, b))
				{
					for (var j = 1; j < c.Count; j++)
						ret = Evaluate(c[j]);
					return ret;
				}
			}
			return ret;
		}

		[ScriptFunction]
		public object SwitchTo(params object[] args)
		{
			object ret = null;
			var a = args[0];
			var b = 0;
			for (var i = 1; i < args.Length; i++, b++)
			{
				var c = args[i] as List<object>;
				if (c == null)
				{
					Notify("Malformed switchto statement.");
					return null;
				}
				if ((bool)Equal(a, b))
				{
					for (var j = 0; j < c.Count; j++)
						ret = Evaluate(c[j]);
					return ret;
				}
			}
			return ret;
		}

		[ScriptFunction]
		public object Range(params object[] args)
		{
			var start = Evaluate<int>(args[0]);
			var end = Evaluate<int>(args[1]);
			var ret = new List<object>();
			for (var i = start; i <= end; i++)
				ret.Add(i);
			return ret;
		}

		[ScriptFunction]
		public object ForEach(params object[] args)
		{
			var list = Evaluate(args[0]) as List<object>;
			if (list == null)
				return null;
			var iterator = args[1] as Symbol;
			var block = args.Skip(2).ToList();
			object ret = null;
			foreach (var item in list)
			{
				SetVar(iterator, item);
				foreach (var command in block)
					ret = Evaluate(command);
			}
			return ret;
		}

		[ScriptFunction]
		public object For(params object[] args)
		{
			var what = args[0] as List<object>;
			var iterator = what[0] as Symbol;
			var from = Evaluate<int>(what[1]);
			var to = Evaluate<int>(what[2]);
			var step = (what.Count > 3) ? Evaluate<int>(what[3]) : (from < to ? 1 : -1);
			if (step == 0) step = 1;
			var val = from;
			var block = args.Skip(1).ToList();
			object ret = null;
			while (true)
			{
				SetVar(iterator, val);
				foreach (var command in block)
					ret = Evaluate(command);
				val += step;
				if (step > 0 && val > to)
					break;
				if (step < 0 && val < to)
					break;
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
				if (celID.Contains('?') || celID.Contains('*'))
					return obj.Cels.Where(o => o.ID.SplatMatch(celID)).Cast<object>().ToList();
				return obj.Cels.Where(o => o.ID == celID).Cast<object>().ToList();
			}
			else if (id.Contains('?') || id.Contains('*'))
			{
				return Cels.Where(o => o.ID.SplatMatch(id)).Cast<object>().ToList();
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
		/// Returns true if a part is colliding with a given other part.
		/// </summary>
		[ScriptFunction("colliding?")]
		public object IsColliding(params object[] args)
		{
			object checkThis = CelsOrPart(args[0]);
			object checkThat = CelsOrPart(args[1]);
			if (checkThis == null || checkThat == null)
				return null;

			if (checkThis is List<object>) checkThis = (Cel)((List<object>)checkThis)[0];
			if (checkThat is List<object>) checkThat = (Cel)((List<object>)checkThat)[0];

			var thisPart = (checkThis is Cel) ? ((Cel)checkThis).Part : (Part)checkThis;
			var thatPart = (checkThat is Cel) ? ((Cel)checkThat).Part : (Part)checkThat;


			//if (checkThis is Part && checkThat is Part)
			//	return Tools.Overlap(thisPart.Position, thatPart.Position, thisPart.Bounds, thatPart.Bounds);

			if (!Tools.Overlap(thisPart.Position, thatPart.Position, thisPart.Bounds, thatPart.Bounds))
				return false;

			if (checkThis is Part)
			{
				if (checkThat is Part)
					return Tools.PixelOverlap(thisPart, thatPart);

				foreach (var cA in thisPart.Cels)
					if (Tools.PixelOverlap(thisPart, thatPart, cA, (Cel)checkThat))
						return true;
			}
			else if (checkThat is Part)
			{
				foreach (var cB in thatPart.Cels)
					if (Tools.PixelOverlap(thisPart, thatPart, (Cel)checkThis, cB))
						return true;
			}
			else
				return Tools.PixelOverlap(thisPart, thatPart, (Cel)checkThis, (Cel)checkThat);
			return false;
		}

		/// <summary>
		/// Returns true if a part is colliding with a given other part, pixel-imperfectly
		/// </summary>
		[ScriptFunction("inside?")]
		public object IsInside(params object[] args)
		{
			object checkThis = CelsOrPart(args[0]);
			object checkThat = CelsOrPart(args[1]);
			if (checkThis == null || checkThat == null)
				return null;

			if (checkThis is List<object>) checkThis = (Cel)((List<object>)checkThis)[0];
			if (checkThat is List<object>) checkThat = (Cel)((List<object>)checkThat)[0];

			var thisPart = (checkThis is Cel) ? ((Cel)checkThis).Part : (Part)checkThis;
			var thatPart = (checkThat is Cel) ? ((Cel)checkThat).Part : (Part)checkThat;

			if (!Tools.Overlap(thisPart.Position, thatPart.Position, thisPart.Bounds, thatPart.Bounds))
				return false;

			if (checkThis is Part)
			{
				if (checkThat is Part)
					return Tools.Overlap(thisPart.Position, thatPart.Position, thisPart.Bounds, thatPart.Bounds);

				foreach (var cA in thisPart.Cels)
					if (Tools.Overlap(
						new Point(thisPart.Position.X + cA.Offset.X, thisPart.Position.Y + cA.Offset.Y),
						new Point(thatPart.Position.X + ((Cel)checkThat).Offset.X, thatPart.Position.Y + ((Cel)checkThat).Offset.Y),
						cA.Image.Size, ((Cel)checkThat).Image.Size))
						return true;
			}
			else if (checkThat is Part)
			{
				foreach (var cB in thatPart.Cels)
					if (Tools.Overlap(
						new Point(thisPart.Position.X + ((Cel)checkThis).Offset.X, thisPart.Position.Y + ((Cel)checkThis).Offset.Y),
						new Point(thatPart.Position.X + cB.Offset.X, thatPart.Position.Y + cB.Offset.Y),
						((Cel)checkThis).Image.Size, cB.Image.Size))
						return true;
			}
			else
				return Tools.Overlap(
						new Point(thisPart.Position.X + ((Cel)checkThis).Offset.X, thisPart.Position.Y + ((Cel)checkThis).Offset.Y),
						new Point(thatPart.Position.X + ((Cel)checkThat).Offset.X, thatPart.Position.Y + ((Cel)checkThat).Offset.Y),
						((Cel)checkThis).Image.Size, ((Cel)checkThat).Image.Size);

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
				return 0;
			if (mapThis is Cel)
				return ((Cel)mapThis).Visible ? 1 : 0;
			else if (mapThis is Part)
				return ((Part)mapThis).Cels.Count(c => c.Visible);
			return 0;
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

		//[ScriptFunction]
		public object ChangeCol(params object[] args)
		{
			var col = Evaluate<int>(args[0]);
			if (col < 0) col = 0;
			if (col >= Viewer.Scene.Palettes) col = Viewer.Scene.Palettes - 1;
			Viewer.Scene.Palette = col;
			Viewer.UpdatePalAndSetButtons();
			return 0;
		}

		//[ScriptFunction]
		public object ChangeSet(params object[] args)
		{
			var set = Evaluate<int>(args[0]);
			if (set < 0) set = 0;
			if (set >= Viewer.Scene.Sets) set = Viewer.Scene.Sets - 1;
			Viewer.Scene.Set = set;
			Viewer.UpdatePalAndSetButtons();
			return 0;
		}

		[ScriptFunction]
		public object Music(params object[] args)
		{
			if (args.Length == 0)
			{
				Viewer.Sound.StopMusic();
				return 0;
			}
			var file = Evaluate<string>(args[0]);
			Viewer.Sound.PlayMusic(Mix, file);
			return 0;
		}

		[ScriptFunction]
		public object Sound(params object[] args)
		{
			var file = Evaluate<string>(args[0]);
			return Viewer.Sound.PlaySound(Mix, file);
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
			var moveWhat = (Part)FindPart(args[0]);
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
				mapThis = ((Part)mapThis).Cels.Cast<object>().ToList();
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
				mapThis = ((Part)mapThis).Cels.Cast<object>().ToList();
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
				mapThis = ((Part)mapThis).Cels.Cast<object>().ToList();
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
			var sb = new StringBuilder();
			foreach (var arg in args)
				sb.Append(Evaluate(arg).ToString());
			DarkUI.Forms.DarkMessageBox.ShowInformation(sb.ToString(), Application.ProductName);
			return null;
		}
	}
}
