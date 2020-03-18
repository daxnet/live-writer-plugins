using System;

namespace OpenLiveWriter.SourceCode
{
	[Serializable]
	public class PluginConfigurationData
	{
		public bool LoadFromClipboard
		{
			get;
			set;
		}

		public string CodeFontFamily
		{
			get;
			set;
		}

		public float CodeFontSize
		{
			get;
			set;
		}

		public int MainFormX
		{
			get;
			set;
		}

		public int MainFormY
		{
			get;
			set;
		}

		public int MainFormWidth
		{
			get;
			set;
		}

		public int MainFormHeight
		{
			get;
			set;
		}

		public bool UseServerDefaults
		{
			get;
			set;
		}

		public bool AutoLinks
		{
			get;
			set;
		}

		public string ClassName
		{
			get;
			set;
		}

		public bool Collapse
		{
			get;
			set;
		}

		public bool Gutter
		{
			get;
			set;
		}

		public bool HtmlScript
		{
			get;
			set;
		}

		public bool Light
		{
			get;
			set;
		}

		public bool Ruler
		{
			get;
			set;
		}

		public bool SmartTabs
		{
			get;
			set;
		}

		public int TabSize
		{
			get;
			set;
		}

		public bool Toolbar
		{
			get;
			set;
		}

		public string Brush
		{
			get;
			set;
		}

		public int FirstLine
		{
			get;
			set;
		}

		public string Highlight
		{
			get;
			set;
		}

		public PluginConfigurationData()
		{
			this.UseServerDefaults = true;
			this.CodeFontFamily = "Microsoft Sans Serif";
			this.CodeFontSize = 8.25f;
			this.MainFormX = 0;
			this.MainFormY = 0;
			this.MainFormWidth = 0;
			this.MainFormHeight = 0;
			this.AutoLinks = true;
			this.ClassName = "";
			this.Collapse = false;
			this.Gutter = true;
			this.HtmlScript = false;
			this.Light = false;
			this.Ruler = false;
			this.SmartTabs = true;
			this.TabSize = 4;
			this.Toolbar = true;
			this.LoadFromClipboard = true;
			this.Brush = "csharp";
			this.FirstLine = 1;
			this.Highlight = string.Empty;
		}
	}
}
