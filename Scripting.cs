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
		public Dictionary<string, List<object>> Events { get; set; }
		public Dictionary<int, Timer> Timers { get; set; }
		public Dictionary<int, string> HashCodes;

		void LoadEvents(List<object> eventsNode)
		{
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
					addEvent(string.Format("click|{0}", clickOn), result);
				}
				else if (trigForm == "alarm")
				{
					if (!(trigger[1] is int || trigger[1] is string || trigger[1] is Symbol))
					{
						MessageBox.Show(string.Format("Malformed \"alarm\" event. An integer or string ID is expected, but got a {0}.", trigger[1].GetType().Name), Application.ProductName);
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

		public bool RunEvent(List<object> ev)
		{
			foreach (var cmd in ev.Cast<List<object>>())
			{
				var form = cmd[0] as Symbol;
				if (form == "moverel")
				{
					var moveRelWhat = Objects.FirstOrDefault(o => o.ID == cmd[1].ToString());
					var moveRelTo = Objects.FirstOrDefault(o => o.ID == cmd[2].ToString());
					var moveRelByX = (int)cmd[3];
					var moveRelByY = (int)cmd[4];
					if (moveRelWhat == null || moveRelTo == null)
						continue;
					moveRelWhat.Position = new Point(moveRelTo.Position.X + moveRelByX, moveRelTo.Position.Y + moveRelByY);
				}
				else if (form == "unmap" || form == "map" || form == "altmap")
				{
					object mapThis = Cells.FirstOrDefault(o => o.ID == cmd[1].ToString());
					if (mapThis == null)
						mapThis = Objects.FirstOrDefault(o => o.ID == cmd[1].ToString());
					if (mapThis == null)
						continue;
					if (form == "altmap")
					{
						if (mapThis is Cell)
							((Cell)mapThis).Visible = !((Cell)mapThis).Visible;
						else if (mapThis is Object)
							((Object)mapThis).Visible = !((Object)mapThis).Visible;
					}
					else
					{
						if (mapThis is Cell)
							((Cell)mapThis).Visible = form == "map";
						else if (mapThis is Object)
							((Object)mapThis).Visible = form == "map";
					}
				}
				else if (form == "timer")
				{
					var delay = (int)cmd[1];
					if (!(cmd[2] is int || cmd[2] is string || cmd[2] is Symbol))
					{
						MessageBox.Show(string.Format("Malformed \"timer\" command. An integer or string ID is expected, but got a {0}.", cmd[2].GetType().Name), Application.ProductName);
						continue;
					}
					var timerID = cmd[2].GetHashCode();
					if (!Timers.ContainsKey(timerID))
						Timers.Add(timerID, new Timer());
					var timer = Timers[timerID];
					timer.Tag = cmd[2];
					timer.Tick += (s, e) =>
					{
						if (!(cmd.Count == 4 && cmd[3] is Symbol && cmd[3].ToString() == "repeat"))
							((Timer)s).Stop();
						var tag = ((Timer)s).Tag;
						if (tag is List<object>)
						{
							RunEvent((List<object>)tag);
						}
						else
						{
							var alarmID = string.Format("alarm|{0}", tag.GetHashCode());
							if (Events.ContainsKey(alarmID))
								RunEvent(Events[alarmID]);
						}
						Viewer.DrawScene();
					};
					timer.Interval = delay;
					timer.Start();
				}
			}
			return true;
		}

		public void Release(Object held)
		{
			var somethingHappened = false;
			var somethingCollided = false;
			foreach (var other in Objects)
			{
				if (other == held)
					continue;
				if (Tools.Overlap(held.Position, other.Position, held.Bounds, other.Bounds))
				{
					var maybe = string.Format("collide|{0}|{1}", held.ID, other.ID);

					if (other.ID == "body")
						Clipboard.SetText(string.Format("((collide \"{0}\" \"{1}\") (moverel \"{0}\" \"{1}\" {2} {3}))", held.ID, other.ID, held.Position.X - other.Position.X, held.Position.Y - other.Position.Y));

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
						somethingHappened = true;
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
						somethingHappened = true;
						if (RunEvent(Events[maybe]))
							break;
					}
				}
			}
			if (!somethingCollided)
				held.LastCollidedWith = null;
			if (somethingHappened)
				Viewer.DrawScene();

		}

		public void Click(Cell cell)
		{
			var maybe = string.Format("click|{0}", cell.ID);
			if (Events.ContainsKey(maybe))
				RunEvent(Events[maybe]);
			else
			{
				maybe = string.Format("click|{0}", cell.Object.ID);
				if (Events.ContainsKey(maybe))
					RunEvent(Events[maybe]);
			}
		}

		public void Decode(DarkTreeView list)
		{
			list.Nodes.Clear();

			Func<DarkTreeNode, List<object>, bool> decode = null;
			decode = (n, e) =>
			{
				foreach (var cmd in e.Cast<List<object>>())
				{
					var newNode = new DarkTreeNode();
					n.Nodes.Add(newNode);
					var form = cmd[0] as Symbol;
					if (form == "moverel")
					{
						newNode.Text = string.Format("(moverel \"{0}\" \"{1}\" {2} {3})", cmd[1], cmd[2], cmd[3], cmd[4]);
					}
					else if (form == "timer")
					{
						newNode.Text = string.Format("(timer {0} {1}{2})", cmd[1], (cmd[2] is List<object>) ? "⭝" : (cmd[2] is string) ? string.Format("\"{0}\"", cmd[2]) : cmd[2], (cmd.Count == 4 && cmd[3] is Symbol && cmd[3].ToString() == "repeat") ? " repeat" : "");
						if (cmd[2] is List<object>)
							decode(newNode, cmd[2] as List<object>);
					}
					else if (form == "map" || form == "unmap" || form == "altmap")
					{
						newNode.Text = string.Format("({0} {1})", form, (cmd[1] is string) ? string.Format("\"{0}\"", cmd[1]) : cmd[1]);
					}
					else
						newNode.Text = string.Format("(???: {0})", cmd[0]);
				}
				return true;
			};

			foreach (var e in Events)
			{
				var ev = e.Key.Split('|');
				var evNode = new DarkTreeNode();
				if (ev[0] == "collide" || ev[0] == "in")
					evNode.Text = string.Format("({0} \"{1}\" \"{2}\")", ev[0], ev[1], ev[2]);
				else if (ev[0] == "click") //one string param
				{
					evNode.Text = string.Format("({0} \"{1}\")", ev[0], ev[1]);
				}
				else if (ev[0] == "alarm") //one int or string param
				{
					var evHash = ev[1].GetHashCode();
					int.TryParse(ev[1].ToString(), out evHash);
					evNode.Text = string.Format("({0} {1})", ev[0], Viewer.Scene.HashCodes[evHash]);
				}
				else if (ev[0] == "initialize") //no param
					evNode.Text = string.Format("({0})", ev[0]);
				else
					evNode.Text = string.Format("(???: {0})", ev[0]);
				evNode.Tag = e.Value;
				decode(evNode, e.Value);
				list.Nodes.Add(evNode);
			}
		}
	}
}
