﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

using ICSharpCode.RubyBinding;
using ICSharpCode.Scripting.Tests.Utils;
using NUnit.Framework;
using RubyBinding.Tests.Utils;

namespace RubyBinding.Tests.Designer
{
	/// <summary>
	/// Tests the GetInstance method of the RubyDesignerLoader.
	/// </summary>
	[TestFixture]
	public class GetInstanceFromDesignerLoaderTestFixture
	{
		RubyDesignerLoader loader;
		MockDesignerLoaderHost host;
		ListViewItem listViewItem1;
		object instance;
		Type type;
		string typeName;
		
		[SetUp]
		public void Init()
		{
			host = new MockDesignerLoaderHost();
			loader = new RubyDesignerLoader(new MockDesignerGenerator());
			loader.BeginLoad(host);
			
			DesignerSerializationManager designerSerializationManager = (DesignerSerializationManager)host.GetService(typeof(IDesignerSerializationManager));
			using (designerSerializationManager.CreateSession()) {	
				listViewItem1 = (ListViewItem)loader.CreateInstance(typeof(ListViewItem), new object[0], "listViewItem1", false);
				instance = loader.GetInstance("listViewItem1");
				typeName = typeof(Int32).FullName;
				type = loader.GetType(typeName);
			}
		}
		
		[Test]
		public void GetListViewInstance()
		{
			Assert.AreEqual(listViewItem1, instance);
		}
		
		[Test]
		public void GetTypeFromLoader()
		{
			Assert.AreEqual(typeof(Int32), type);
		}
		
		[Test]
		public void TypeNameUsed()
		{
			Assert.AreEqual(typeName, host.TypeResolutionService.LastTypeNameResolved);
		}
	}
}
