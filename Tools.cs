using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace KiSSLab
{
	public static class Tools
	{
		public static int Distance(Point a, Point b)
		{
			var x = (b.X - a.X);
			var y = (b.Y - a.Y);
			return (int)Math.Sqrt(x * x + y * y);
		}

		public static bool Overlap(Point pA, Point pB, Size sA, Size sB)
		{
			var pA2 = new Point(pA.X + sA.Width, pA.Y + sA.Height);
			var pB2 = new Point(pB.X + sB.Width, pB.Y + sB.Height);
			// If one rectangle is on left side of other  
			if (pA2.X < pB.X || pB2.X < pA.X)
				return false;

			// If one rectangle is above other  
			if (pA2.Y < pB.Y || pB2.Y < pA.Y)
				return false;

			return true;
		}

		public static bool PixelOverlap(Part oA, Part oB, Cel cA, Cel cB)
		{
			var pA = new Point(oA.Position.X + cA.Offset.X, oA.Position.Y + cA.Offset.Y);
			var pB = new Point(oB.Position.X + cB.Offset.X, oB.Position.Y + cB.Offset.Y);

			try
			{
				if (!Overlap(pA, pB, cA.Image.Size, cB.Image.Size))
					return false;
			}
			catch (Exception)
			{
				return false;
			}

			var hitbox1 = new Rectangle(pA, cA.Image.Size);
			var hitbox2 = new Rectangle(pB, cB.Image.Size);

			int top = Math.Max(hitbox1.Top, hitbox2.Top);
			int bottom = Math.Min(hitbox1.Bottom, hitbox2.Bottom);
			int right = Math.Max(hitbox1.Right, hitbox2.Right);
			int left = Math.Min(hitbox1.Left, hitbox2.Left);

			for (var y = top; y < bottom; y++)
			{
				for (var x = left; x < right; x++)
				{
					var xA = x - hitbox1.Left;
					var yA = y - hitbox1.Top;
					var xB = x - hitbox2.Left;
					var yB = y - hitbox2.Top;

					if (xA < 0) continue;
					if (xB < 0) continue;
					if (xA >= cA.Image.Width) continue;
					if (xB >= cB.Image.Width) continue;

					var color1 = cA.Image.GetPixel(xA, yA);
					var color2 = cB.Image.GetPixel(xB, yB);

					if (color1.A != 0 && color2.A != 0)
					{
						return true;
					}
				}
			}

			return false;
		}

		public static bool PixelOverlap(Part oA, Part oB)
		{
			foreach (var cA in oA.Cels)
			{
				foreach (var cB in oB.Cels)
				{
					if (PixelOverlap(oA, oB, cA, cB))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Grabs a Bitmap from the Mix system, *but* in a way that ensures no files are locked, thus allowing rapid doll development.
		/// </summary>
		/// <param name="file">The name of the file to grab.</param>
		/// <returns>Not exactly the *same* Bitmap.</returns>
		public static Bitmap GrabClonedBitmap(string file, Kawa.Mix.Mix mix)
		{
			using (var jango = mix.GetBitmap(file))
			{
				var boba = new Bitmap(jango.Width, jango.Height);
				var gfx = Graphics.FromImage(boba);
				var full = new Rectangle(0, 0, jango.Width, jango.Height);
				gfx.DrawImage(jango, full, full, GraphicsUnit.Pixel);
				return boba;
			}
		}

		public static bool SplatMatch(this string input, string pattern)
		{
			pattern = "^" + Regex.Escape(pattern).Replace("\\?", ".").Replace("\\*", ".*") + "$";
			return Regex.IsMatch(input, pattern);
		}
	}
}
