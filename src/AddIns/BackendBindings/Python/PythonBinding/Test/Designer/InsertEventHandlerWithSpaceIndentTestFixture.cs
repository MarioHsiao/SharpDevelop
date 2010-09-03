﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using ICSharpCode.FormsDesigner;
using ICSharpCode.PythonBinding;
using ICSharpCode.Scripting.Tests.Utils;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using PythonBinding.Tests.Utils;

namespace PythonBinding.Tests.Designer
{
	/// <summary>
	/// Tests the PythonDesignerGenerator uses the text editor properties for indentation when
	/// inserting an event handler into the document.
	/// </summary>
	[TestFixture]
	public class InsertEventHandlerWithSpaceIndentTestFixture : InsertEventHandlerTestFixtureBase
	{
		public override void AfterSetUpFixture()
		{
			base.textEditorOptions.ConvertTabsToSpaces = true;
			base.textEditorOptions.IndentationSize = 4;
			MockEventDescriptor mockEventDescriptor = new MockEventDescriptor("Click");
			insertedEventHandler = generator.InsertComponentEvent(null, mockEventDescriptor, "button1_click", String.Empty, out file, out position);
		}
		
		[Test]
		public void ExpectedCodeAfterEventHandlerInserted()
		{
			string expectedCode = GetTextEditorCode();			
			string eventHandler = 
				"    def button1_click(self, sender, e):\r\n" +
				"        pass";
			expectedCode = expectedCode + "\r\n" + eventHandler;
			
			Assert.AreEqual(expectedCode, viewContent.DesignerCodeFileContent);
		}
		
		/// <summary>
		/// Note that the text editor code already has the
		/// statement: 
		/// 
		/// "self._button1.Click += button1_click"
		/// 
		/// This is generated by the form designer framework and not
		/// by the designer generator.
		/// </summary>
		protected override string GetTextEditorCode()
		{
			return "from System.Windows.Forms import Form\r\n" +
					"\r\n" +
					"class MainForm(Form):\r\n" +
					"    def __init__(self):\r\n" +
					"        self.InitializeComponents()\r\n" +
					"    \r\n" +
					"    def InitializeComponents(self):\r\n" +
					"        self._button1 = System.Windows.Forms.Button()\r\n" +
					"        self.Controls.Add(self._button1)\r\n";
		}
	}
}
