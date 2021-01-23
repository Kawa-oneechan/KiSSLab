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
	public partial class Scene
	{
		[ScriptFunction("*")]
		public object Multiply(params object[] cmd)
		{
			var ret = Evaluate<int>(cmd[1]);
			for (var i = 2; i < cmd.Length; i++)
				ret *= Evaluate<int>(cmd[i]);
			return ret;
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
		public object MoveRel(params object[] cmd)
		{
			var str = Evaluate(cmd[1]).ToString();
			var moveRelWhat = Objects.FirstOrDefault(o => o.ID == str);
			str = Evaluate(cmd[2]).ToString();
			var moveRelTo = Objects.FirstOrDefault(o => o.ID == str);
			var moveRelByX = Evaluate<int>(cmd[3]); //(int)cmd[3];
			var moveRelByY = Evaluate<int>(cmd[4]); //(int)cmd[4];
			if (!(moveRelWhat == null || moveRelTo == null))
				moveRelWhat.Position = new Point(moveRelTo.Position.X + moveRelByX, moveRelTo.Position.Y + moveRelByY);
			return null;
		}

		[ScriptFunction]
		public object Map(params object[] cmd)
		{
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Objects.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = true;
			else if (mapThis is Object)
				((Object)mapThis).Visible = true;
			return mapThis;
		}

		[ScriptFunction]
		public object UnMap(params object[] cmd)
		{
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Objects.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = false;
			else if (mapThis is Object)
				((Object)mapThis).Visible = false;
			return mapThis;
		}

		[ScriptFunction]
		public object AltMap(params object[] cmd)
		{
			object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				mapThis = Objects.FirstOrDefault(o => o.ID == cmd[1].ToString());
			if (mapThis == null)
				return null;
			if (mapThis is Cell)
				((Cell)mapThis).Visible = !((Cell)mapThis).Visible;
			else if (mapThis is Object)
				((Object)mapThis).Visible = !((Object)mapThis).Visible;
			return mapThis;
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
