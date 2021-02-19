using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Bitmap = System.Drawing.Bitmap;

namespace Kawa.Mix
{
	public class Mix
	{
		private class MixFileEntry
		{
			public string Source, Filename;
			public int Offset, Length;
			public bool IsCompressed;
			public bool IsInFolder;
		}

		private Dictionary<string, MixFileEntry> fileList;

		public void Reset()
		{
			fileList = new Dictionary<string, MixFileEntry>();
		}

		public void Load(string source)
		{
			if (fileList == null)
				Reset();
			if (File.Exists(source))
			{
				using (var mStream = new BinaryReader(File.Open(source, FileMode.Open)))
				{
					//This is not the "proper" way to do it. Fuck that.
					while (true)
					{
						var header = mStream.ReadBytes(4);
						if (header[0] != 'P' || header[1] != 'K' || header[2] != 3 || header[3] != 4)
						{
							if (header[2] == 1 && header[3] == 2) //reached the Central Directory
								break;
							throw new FileLoadException(string.Format("Zip file '{0}' has an incorrect header.", source));
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
							Source = source,
							IsInFolder = false,
						};
						fileList[filename] = entry;
					}
				}
			}
			else if (Directory.Exists(source))
			{
				foreach (var filename in Directory.EnumerateFiles(source))
				{
					var fn = Path.GetFileName(filename);
					var entry = new MixFileEntry()
					{
						Offset = 0,
						Length = 0,
						IsCompressed = false,
						Filename = fn,
						Source = source,
						IsInFolder = true,
					};
					fileList[fn] = entry;
				}
			}
		}

		public bool FileExists(string fileName)
		{
			return (fileList.ContainsKey(fileName));
		}

		public Stream GetStream(string fileName)
		{
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException(string.Format("File {0} was not found.",  fileName));
			var entry = fileList[fileName];
			if (entry.IsInFolder)
			{
				return new MemoryStream(File.ReadAllBytes(Path.Combine(entry.Source, entry.Filename)));
			}
			MemoryStream ret;
			using (var mStream = new BinaryReader(File.Open(entry.Source, FileMode.Open)))
			{
				mStream.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
				ret = new MemoryStream(mStream.ReadBytes(entry.Length));
			}
			return ret;
		}

		public string GetString(string fileName)
		{
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException(string.Format("File {0} was not found.", fileName));
			return Encoding.UTF8.GetString(GetBytes(fileName));
		}

		public Bitmap GetBitmap(string fileName)
		{
			var raw = GetBytes(fileName);
			using (var str = new MemoryStream(raw))
			{
				return (Bitmap)Bitmap.FromStream(str);
			}
		}

		public byte[] GetBytes(string fileName)
		{
			if (!fileList.ContainsKey(fileName))
				throw new FileNotFoundException(string.Format("File {0} was not found.", fileName));
			var entry = fileList[fileName];
			if (entry.IsInFolder)
			{
				return File.ReadAllBytes(Path.Combine(entry.Source, entry.Filename));
			}
			byte[] ret;
			using (var mStream = new BinaryReader(File.Open(entry.Source, FileMode.Open)))
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

		public string[] GetFilesInPath(string path)
		{
			var ret = new List<string>();
			foreach (var entry in fileList.Values.Where(x => x.Filename.StartsWith(path)))
				ret.Add(entry.Filename);
			return ret.ToArray();
		}
		
		public void GetFileRange(string fileName, out int offset, out int length, out string mixFile)
		{
			offset = -1;
			length = -1;
			mixFile = string.Empty;
			if (!fileList.ContainsKey(fileName))
				return;
			var entry = fileList[fileName];
			offset = entry.Offset;
			length = entry.Length;
			mixFile = entry.Source;
		}

		public string[] GetFilesWithPattern(string pattern)
		{
			var ret = new List<string>();
			var regex = new System.Text.RegularExpressions.Regex(pattern.Replace("*", "(.*)").Replace("\\", "\\\\"));
			foreach (var entry in fileList.Values.Where(x => regex.IsMatch(x.Filename)))
				ret.Add(entry.Filename);
			return ret.ToArray();
		}
	}
}