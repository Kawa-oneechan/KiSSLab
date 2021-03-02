using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kawa.Mix;

namespace KiSSLab
{
	public class SoundSystem
	{
		public class Sound
		{
			private FMOD.System system;
			public FMOD.Sound InnerSound { get; private set; }
			public FMOD.Channel Channel { get; private set; }
			public Sound(string file, Mix mix, FMOD.System system)
			{
				this.system = system;
				FMOD.Sound ns = null;
				var data = mix.GetBytes(file);
				var fCSex = new FMOD.CreateSoundExInfo()
				{
					Size = 216,
					Length = (uint)data.Length
				};
				SoundSystem.CheckError(system.CreateSound(data, FMOD.SoundMode.Default | FMOD.SoundMode.OpenMemory, ref fCSex, ref ns));
				InnerSound = ns;
			}
			public void Play()
			{
				if (InnerSound == null)
					return;
				var chan = Channel;
				SoundSystem.CheckError(system.PlaySound(InnerSound, false, ref chan));
				Channel = chan;
			}
			public void Stop()
			{
				Channel.Stop();
			}
			~Sound()
			{
				Channel.Stop();
				InnerSound.Release();
			}
		}

		private FMOD.System system;
		private Dictionary<string, Sound> sounds;
		private FMOD.Sound music;
		private FMOD.Channel musicChannel;

		public bool Enabled
		{
			get
			{
				return system != null;
			}
		}

		public SoundSystem()
		{
			var ret = FMOD.Result.OK;
			system = null;

			try
			{
				FMOD.Factory.CreateSystem(ref system);
			}
			catch (DllNotFoundException)
			{
				return;
			}
			if (ret != FMOD.Result.OK)
				return;
			ret = system.Initialize(8, FMOD.InitFlags.Normal, IntPtr.Zero);
			if (ret != FMOD.Result.OK)
			{
				system = null;
				return;
			}
			sounds = new Dictionary<string, Sound>();
			music = null;
			musicChannel = null;
		}

		public void PlayMusic(Mix mix, string name)
		{
			if (!Enabled)
				return;

			if (mix.FileExists(name))
			{
				StopMusic();
				var data = mix.GetBytes(name);
				var fCSex = new FMOD.CreateSoundExInfo()
				{
					Size = 216,
					Length = (uint)data.Length
				};
				CheckError(system.CreateSound(data, FMOD.SoundMode.LoopNormal | FMOD.SoundMode.OpenMemory, ref fCSex, ref music));
				system.PlaySound(music, false, ref musicChannel);
				musicChannel.SetVolume(0.5f);
			}
		}

		public Sound PlaySound(Mix mix, string name)
		{
			if (!Enabled)
				return null;

			if (sounds.ContainsKey(name) && sounds[name] == null)
				return null;
			if (!sounds.ContainsKey(name))
			{
				if (mix.FileExists(name))
				{
					sounds.Add(name, new Sound(name, mix, system));
				}
				else
				{
					sounds.Add(name, null);
					return null;
				}
			}
			sounds[name].Play();
			return sounds[name];
		}

		public void Update()
		{
			if (!Enabled)
				return;
			system.Update();
		}

		public void StopMusic()
		{
			if (!Enabled)
				return;

			if (musicChannel != null)
				musicChannel.Stop();
			if (music != null)
				music.Release();
		}

		public void StopEverything()
		{
			if (!Enabled)
				return;
			if (musicChannel != null)
				musicChannel.Stop();
			if (music != null)
				music.Release();
			if (sounds != null)
			{
				foreach (var s in sounds.Values)
				{
					s.Channel.Stop();
					s.InnerSound.Release();
				}
				sounds.Clear();
			}
		}

		public void ShutDown()
		{
			if (!Enabled)
				return;
			StopEverything();
			system.Close();
			system = null;
		}

		public static void CheckError(FMOD.Result result)
		{
			if (result != FMOD.Result.OK)
				throw new Exception(string.Format("FMOD error: {0}", result));
		}
	}
}
