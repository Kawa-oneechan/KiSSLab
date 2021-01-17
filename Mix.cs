using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Bitmap = System.Drawing.Bitmap;

namespace Kawa.Mix
{
	public static class Mix
	{
		private class MixFileEntry
		{
			public string MixFile, Filename;
			public int Offset, Length;
			public bool IsCompressed;
		}

		private static string dataFolder;

		private static Dictionary<string, MixFileEntry> fileList;

		private static Dictionary<string, string> stringCache;

		public static void Initialize(string mixfile)
		{
			fileList = new Dictionary<string, MixFileEntry>();
			stringCache = new Dictionary<string, string>();
			//var mixfiles = new List<string>() { mainFile + "." + ext };
			//mixfiles.AddRange(Directory.GetFiles(".", "*." + ext).Select(x => x.Substring(2)).Where(x => !x.Equals(mainFile + "." + ext, StringComparison.OrdinalIgnoreCase)));
			//foreach (var mixfile in mixfiles)
						
			if (File.Exists(mixfile))
			{
				//if (!File.Exists(mixfile))
				//	continue;

				using (var mStream = new BinaryReader(File.Open(mixfile, FileMode.Open)))
				{
					//This is not the "proper" way to do it. Fuck that.
					while (true)
					{
						var header = mStream.ReadBytes(4);
						if (header[0] != 'P' || header[1] != 'K' || header[2] != 3 || header[3] != 4)
						{
							if (header[2] == 1 && header[3] == 2) //reached the Central Directory
								break;
							throw new FileLoadException(string.Format("Zip file '{0}' has an incorrect header.", mixfile));
						}
						mStream.BaseStream.Seek(4, SeekOrigin.Current);
						var method = mStream.ReadInt16();
						mStream.BaseStream.Seek(8, SeekOrigin.Current);
						var moto = mStream.ReadBytes(4);
						var compressedSize = (moto[3] << 24) | (moto[2] << 16) | (moto[1] << 8) | moto[0]; //0x000000F8
						moto = mStream.ReadBytes(4);
						var uncompressedSize = (moto[3] << 24) | (moto[2] << 16) | (moto[1] << 8) | moto[0]; //0x00000197
						moto = mStream.ReadBytes(2);
						var filenameLength = (moto[1] << 8) | moto[0];
						mStream.BaseStream.Seek(2, SeekOrigin.Current);
						var filename = new string(mStream.ReadChars(filenameLength)).Replace('/', '\\');
						var offset = (int)mStream.BaseStream.Position;
						mStream.BaseStream.Seek(compressedSize, SeekOrigin.Current);
						if (filename.EndsWith("\\"))
							continue;
						var entry = new MixFileEntry()
						{
							Offset = offset,
							Length = compressedSize,
							IsCompressed = method == 8,
							Filename = filename,
							MixFile = mixfile,
						};
						fileList[filename] = entry;
					}
				}
			}
			else if (Directory.Exists(mixfile))
			{
				dataFolder = mixfile;
			}
		}

		public static bool FileExists(string fileName)
		{
			if (dataFolder != null && File.Exists(Path.Combine(dataFolder, fileName)))
				return true;
			return (fileList.ContainsKey(fileName));
		}

		public static Stream GetStream(string fileName)
		{
			if (dataFolder != null && File.Exists(Path.Combine(dataFolder, fileName)))
				return new MemoryStream(File.ReadAllBytes(Path.Combine(dataFolder, fileName)));
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException("File " + fileName + " was not found in the MIX files.");
			MemoryStream ret;
			var entry = fileList[fileName];
			using (var mStream = new BinaryReader(File.Open(entry.MixFile, FileMode.Open)))
			{
				mStream.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
				ret = new MemoryStream(mStream.ReadBytes(entry.Length));
			}
			return ret;
		}

		public static string GetString(string fileName, bool cache = true)
		{
			if (cache && stringCache.ContainsKey(fileName))
				return stringCache[fileName];
			if (dataFolder != null && File.Exists(Path.Combine(dataFolder, fileName)))
				return File.ReadAllText(Path.Combine(dataFolder, fileName));
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException("File " + fileName + " was not found in the MIX files.");
			var bytes = GetBytes(fileName);
			var ret = Encoding.UTF8.GetString(bytes);
			if (cache)
				stringCache[fileName] = ret;
			return ret;
		}

		//TODO: cache the returns.
		public static Bitmap GetBitmap(string fileName)
		{
			if (dataFolder != null && File.Exists(Path.Combine(dataFolder, fileName)))
				return new Bitmap(Path.Combine(dataFolder, fileName));
			var raw = GetBytes(fileName);
			using (var str = new MemoryStream(raw))
			{
				return (Bitmap)Bitmap.FromStream(str);
			}
		}

		public static byte[] GetBytes(string fileName)
		{
			if (dataFolder != null && File.Exists(Path.Combine(dataFolder, fileName)))
				return File.ReadAllBytes(Path.Combine(dataFolder, fileName));
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException("File " + fileName + " was not found in the MIX files.");
			byte[] ret;
			var entry = fileList[fileName];
			using (var mStream = new BinaryReader(File.Open(entry.MixFile, FileMode.Open)))
			{
				mStream.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
				if (!entry.IsCompressed)
				{
					ret = mStream.ReadBytes(entry.Length);
				}
				else
				{
					var cStream = new MemoryStream(mStream.ReadBytes(entry.Length));
					var decompressor = new System.IO.Compression.DeflateStream(cStream, System.IO.Compression.CompressionMode.Decompress);
					var outStream = new MemoryStream();
					var buffer = new byte[1024];
					var recieved = 1;
					while (recieved > 0)
					{
						recieved = decompressor.Read(buffer, 0, buffer.Length);
						outStream.Write(buffer, 0, recieved);
					}
					ret = new byte[outStream.Length];
					outStream.Seek(0, SeekOrigin.Begin);
					outStream.Read(ret, 0, ret.Length);
				}
			}
			return ret;
		}

		public static string[] GetFilesInPath(string path)
		{
			var ret = new List<string>();
			foreach (var entry in fileList.Values.Where(x => x.Filename.StartsWith(path)))
				ret.Add(entry.Filename);
			if (Directory.Exists(Path.Combine(dataFolder, path)))
			{
				var getFiles = Directory.GetFiles(Path.Combine(dataFolder, path), "*", SearchOption.AllDirectories);
				ret.AddRange(getFiles.Select(x => x.Substring((dataFolder + "\\").Length)).Where(x => !ret.Contains(x)));
			}
			return ret.ToArray();
		}
		
		public static void GetFileRange(string fileName, out int offset, out int length, out string mixFile)
		{
			offset = -1;
			length = -1;
			mixFile = string.Empty;
			if (!fileList.ContainsKey(fileName))
				return;
			var entry = fileList[fileName];
			offset = entry.Offset;
			length = entry.Length;
			mixFile = entry.MixFile;
		}

		public static string[] GetFilesWithPattern(string pattern)
		{
			var ret = new List<string>();
			var regex = new System.Text.RegularExpressions.Regex(pattern.Replace("*", "(.*)").Replace("\\", "\\\\"));
			foreach (var entry in fileList.Values.Where(x => regex.IsMatch(x.Filename)))
				ret.Add(entry.Filename);
			if (dataFolder != null && Directory.Exists(dataFolder))
			{
				if (pattern.Contains('\\'))
				{
					if (!Directory.Exists(Path.Combine(dataFolder, Path.GetDirectoryName(pattern))))
						return ret.ToArray();
				}
				var getFiles = Directory.GetFiles(dataFolder, pattern, SearchOption.AllDirectories);
				ret.AddRange(getFiles.Select(f => f.Substring(dataFolder.Length + 1)).Where(f => !ret.Contains(f)));
			}
			return ret.ToArray();
		}
	}
}