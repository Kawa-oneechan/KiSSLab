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
		/// <summary>
		/// A timer reaches zero.
		/// </summary>
		[ScriptEvent]
		public string Alarm(params object[] ev)
		{
			if (!(ev[1] is int || ev[1] is string || ev[1] is Symbol))
			{
				DarkUI.Forms.DarkMessageBox.ShowError(string.Format("Malformed \"alarm\" event. An integer or string ID is expected, but got a {0}.", ev[1].GetType().Name), Application.ProductName);
				return null;
			}
			var timerID = ev[1].GetHashCode();
			HashCodes[timerID] = ev[1].ToString();
			if (ev[1] is string)
				HashCodes[timerID] = string.Format("\"{0}\"", ev[1]);
			return string.Format("alarm|{0}", timerID);
		}

		/// <summary>
		/// The two cells or objects do not overlap, taking transparent pixels into account.
		/// Triggers only if the cells did overlap before one of them was moved by the user.
		/// </summary>
		[ScriptEvent]
		public string Apart(params object[] ev)
		{
			return string.Format("apart|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// This event is triggered after the initialize event and before the version event.
		/// </summary>
		[ScriptEvent]
		public string Begin(params object[] ev)
		{
			return "begin";
		}

		/// <summary>
		/// The user clicks on the object or cell.
		/// Applies to all cells & objects except those with a maximal fix value.
		/// </summary>
		[ScriptEvent]
		public string Catch(params object[] ev)
		{
			return string.Format("catch|{0}", ev[1]);
		}

		/// <summary>
		/// The two cells or objects touch, taking transparent pixels into account.
		/// Triggers only if the cells did not overlap before one of them was moved by the user.
		/// </summary>
		[ScriptEvent]
		public string Collide(params object[] ev)
		{
			return string.Format("collide|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// The user releases the mouse on the object or cell.
		/// Applies only to all cells & objects except those with a maximal fix value.
		/// </summary>
		[ScriptEvent]
		public string Drop(params object[] ev)
		{
			return string.Format("drop|{0}", ev[1]);
		}

		/// <summary>
		/// The user quits the player or closes the doll.
		/// </summary>
		[ScriptEvent]
		public string End(params object[] ev)
		{
			return "end";
		}

		/// <summary>
		/// The user clicks on the object or cell.
		/// Applies only to fixed cells & objects.
		/// </summary>
		[ScriptEvent]
		public string FixCatch(params object[] ev)
		{
			return string.Format("fixcatch|{0}", ev[1]);
		}

		/// <summary>
		/// The user releases the object or cell.
		/// Applies only to fixed cells & objects.
		/// </summary>
		[ScriptEvent]
		public string FixDrop(params object[] ev)
		{
			return string.Format("fixdrop|{0}", ev[1]);
		}

		/// <summary>
		/// The two objects or cells overlap, ignoring transparency.
		/// Triggers only if the objects did not overlap before one of them was moved by the user.
		/// </summary>
		[ScriptEvent]
		public string In(params object[] ev)
		{
			return string.Format("in|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// Before the doll is displayed after loading.
		/// </summary>
		[ScriptEvent]
		public string Initialize(params object[] ev)
		{
			return "initialize";
		}

		/// <summary>
		/// The user has pressed the specified key. This event is triggered once when the key is pressed - there is no autorepeat.
		/// </summary>
		[ScriptEvent]
		public string KeyPress(params object[] ev)
		{
			return string.Format("keypress|{0}", ev[1]);
		}

		/// <summary>
		/// The user has released the specified key (i.e. is no longer pressing it).
		/// </summary>
		[ScriptEvent]
		public string KeyRelease(params object[] ev)
		{
			return string.Format("keyrelease|{0}", ev[1]);
		}

		/// <summary>
		/// Triggered when the mouse pointer moves over the cell or object.
		/// The event is only triggered if the cell or object is not occluded by other cells (i.e. if a mouse click would invoke a "press" event)
		/// </summary>
		[ScriptEvent]
		public string MouseIn(params object[] ev)
		{
			return string.Format("mousein|{0}", ev[1]);
		}

		/// <summary>
		/// Triggered when the mouse pointer moves over the cell or object.
		/// The event is only triggered if the cell or object is not occluded by other cells (i.e. if a mouse click would invoke a "press" event)
		/// </summary>
		[ScriptEvent]
		public string MouseOut(params object[] ev)
		{
			return string.Format("mouseout|{0}", ev[1]);
		}

		/// <summary>
		/// The two cells or objects do not overlap, ignoring transparency.
		/// Triggers only if the objects did overlap before one of them was moved by the user.
		/// </summary>
		[ScriptEvent]
		public string Out(params object[] ev)
		{
			return string.Format("out|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// The user clicks on the object or cell.
		/// Applies to all cells & objects.
		/// </summary>
		[ScriptEvent]
		public string Press(params object[] ev)
		{
			return string.Format("press|{0}", ev[1]);
		}

		/// <summary>
		/// The user releases the object or cell.
		/// Applies to all cells & objects.
		/// </summary>
		[ScriptEvent]
		public string Release(params object[] ev)
		{
			return string.Format("release|{0}", ev[1]);
		}

		/// <summary>
		/// The user changes the set to that specified.
		/// </summary>
		[ScriptEvent("set")]
		public string ChangedSet(params object[] ev)
		{
			return string.Format("set|{0}", ev[1]);
		}

		/// <summary>
		/// The two objects or cells overlap, ignoring transparency.
		/// Triggers irrespective of the state of the two objects before movement.
		/// </summary>
		[ScriptEvent]
		public string StillIn(params object[] ev)
		{
			return string.Format("stillin|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// The two cells or objects do not overlap, ignoring transparency.
		/// Triggers irrespective of the state of the two objects before movement.
		/// </summary>
		[ScriptEvent]
		public string StillOut(params object[] ev)
		{
			return string.Format("stillout|{0}|{1}", ev[1], ev[2]);
		}

		/// <summary>
		/// A previously-fixed cell or object becomes free to move.
		/// </summary>
		[ScriptEvent("unfix")]
		public string UnFix(params object[] ev)
		{
			return string.Format("unfix|{0}", ev[1]);
		}
	}
}
