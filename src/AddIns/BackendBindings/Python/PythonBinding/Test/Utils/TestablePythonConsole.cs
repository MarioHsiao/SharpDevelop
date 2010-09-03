﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using ICSharpCode.PythonBinding;
using ICSharpCode.Scripting;
using ICSharpCode.Scripting.Tests.Utils;
using IronPython.Hosting;

namespace PythonBinding.Tests.Utils
{
	public class TestablePythonConsole : PythonConsole
	{
		public MockConsoleTextEditor MockConsoleTextEditor;
		public FakeLock LockCreated;
		public bool IsLineReceivedEventFired;
		public int UnreadLineCountWhenLineReceivedEventFired = -1;
		
		public TestablePythonConsole()
			: this(new MockConsoleTextEditor(), new PythonCommandLine())
		{
		}
		
		TestablePythonConsole(IConsoleTextEditor consoleTextEditor, PythonCommandLine commandLine)
			: base(consoleTextEditor)
		{
			CommandLine = commandLine;
			MockConsoleTextEditor = (MockConsoleTextEditor)consoleTextEditor;
		}
		
		public List<string> GetUnreadLinesList()
		{
			return base.unreadLines;
		}
		
		public string[] GetUnreadLines()
		{
			return base.unreadLines.ToArray();
		}
		
		protected override ILock CreateLock(List<string> lines)
		{
			LockCreated = new FakeLock(lines);
			return LockCreated;
		}
		
		protected override void FireLineReceivedEvent()
		{
			IsLineReceivedEventFired = true;
			UnreadLineCountWhenLineReceivedEventFired = LockCreated.Lines.Count;
		}
	}
}
