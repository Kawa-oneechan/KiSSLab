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
			var key = cmd[1] as Symbol; //Evaluate<Symbol>(cmd[1]);
			var val = Evaluate(cmd[2]);
			scriptVariables[key] = val;
			return key;
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
		public object PlayMusic(params object[] cmd)
		{
			var file = Evaluate<string>(cmd[1]);
			Viewer.Sound.PlayMusic(file);
			return 0;
		}

		[ScriptFunction]
		public object PlaySound(params object[] cmd)
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

		[ScriptFunction]
		public object MoveRel(params object[] cmd)
		{
			if (cmd[1] is int)
			{
				cmd = new[] { cmd[0], (Symbol)"#a", (Symbol)"#b", cmd[1], cmd[2] };
			}

			var str = Evaluate(cmd[1]).ToString();
			var moveRelWhat = Parts.FirstOrDefault(o => o.ID == str);
			str = Evaluate(cmd[2]).ToString();
			var moveRelTo = Parts.FirstOrDefault(o => o.ID == str);
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
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = true;
			else if (mapThis is Part)
				((Part)mapThis).Visible = true;
			return mapThis;
		}

		[ScriptFunction]
		public object UnMap(params object[] cmd)
		{
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = false;
			else if (mapThis is Part)
				((Part)mapThis).Visible = false;
			return mapThis;
		}

		[ScriptFunction]
		public object AltMap(params object[] cmd)
		{
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Parts.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = !((Cell)mapThis).Visible;
			else if (mapThis is Part)
				((Part)mapThis).Visible = !((Part)mapThis).Visible;
			return mapThis;
		}

		[ScriptFunction]
		public object Ghost(params object[] cmd)
		{
			object ghostThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (ghostThis == null)
				return null;
			if (ghostThis is Cell)
				((Cell)ghostThis).Ghost = true;
			return ghostThis;
		}

		[ScriptFunction]
		public object UnGhost(params object[] cmd)
		{
			object ghostThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (ghostThis == null)
				return null;
			if (ghostThis is Cell)
				((Cell)ghostThis).Ghost = false;
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
			timer.Action = cmd[2];
			timer.Interval = timer.TimeLeft = delay;
			timer.Repeat = (cmd.Length == 4 && cmd[3] is Symbol && cmd[3].ToString() == "repeat");
			return timer;
		}
	}
}
