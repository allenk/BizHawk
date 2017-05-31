﻿using BizHawk.Common.BizInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BizHawk.Emulation.Cores.Consoles.SNK
{
	public abstract class LibNeoGeoPort
	{
		private const CallingConvention CC = CallingConvention.Cdecl;

		[UnmanagedFunctionPointer(CC)]
		public delegate void InputCallback();
		[StructLayout(LayoutKind.Sequential)]
		public class EmulateSpec
		{
			public IntPtr Pixels;
			public IntPtr SoundBuff;
			public long MasterCycles;
			public long FrontendTime;
			public int SoundBufMaxSize;
			public int SoundBufSize;
			public int SkipRendering;
			public int Buttons;
			public int Lagged;
		}
		[BizImport(CC)]
		public abstract bool LoadSystem(byte[] rom, int romlength, int language);
		[BizImport(CC)]
		public abstract void SetLayers(int enable); // 1, 2, 4  bg,fg,sprites
		[BizImport(CC)]
		public abstract void FrameAdvance([In, Out]EmulateSpec espec);
		[BizImport(CC)]
		public abstract void HardReset();
		[BizImport(CC)]
		public abstract void SetInputCallback(InputCallback callback);
		[BizImport(CC)]
		public abstract void GetMemoryArea(int which, ref IntPtr ptr, ref int size, ref bool writable);
	}
}