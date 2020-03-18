using System;
using System.Windows.Forms;
using OpenLiveWriter.Api;

namespace OpenLiveWriter.SourceCode
{
	[InsertableContentSource("Source Code", SidebarText = "Source Code"), 
     WriterPlugin("2506BDC0-6146-4f4d-8A9E-FE6A3965A07A",
        "Source Code (Alex Gorbatchev)", 
        ImagePath = "Resources.code.png", 
        Description = "SourceCodePlugin is a plug-in to Windows Live Writer to enable code syntax highlighting using Alex Gorbatchev syntax highlighter.", 
        PublisherUrl = "http://blog.pokluda.com/?tag=/syntaxhighlighter")]
	public class SourceCodePlugin : ContentSource
	{
		public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content)
		{
			DialogResult dialogResult = DialogResult.Cancel;
			using (CodeFormEx codeForm = new CodeFormEx(content))
			{
				dialogResult = codeForm.ShowDialog(dialogOwner);
				if (dialogResult == DialogResult.OK)
				{
					content = codeForm.Code.Replace("\t", "    ");
				}
			}
			return dialogResult;
		}
	}
}
