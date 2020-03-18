using System;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenLiveWriter.SourceCode
{
	internal class PluginConfigurationRepository
	{
		private const string ConfigFile = "OpenLiveWriter.SourceCode.config";

		private readonly string _configurationFilePath;

		private PluginConfigurationData _config;

		public PluginConfigurationData Config
		{
			get
			{
				return this._config;
			}
			set
			{
				this._config = value;
			}
		}

		public PluginConfigurationRepository() : this(new PluginConfigurationData())
		{
		}

		public PluginConfigurationRepository(PluginConfigurationData config)
		{
			string text = string.Format("{0}\\Windows Live Writer", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			this._configurationFilePath = string.Format("{0}\\{1}", text, "OpenLiveWriter.SourceCode.config");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			this._config = config;
		}

		public void LoadPluginConfigurationData()
		{
			if (!File.Exists(this._configurationFilePath))
			{
				this._config = new PluginConfigurationData();
			}
			FileStream fileStream = null;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PluginConfigurationData));
				fileStream = new FileStream(this._configurationFilePath, FileMode.Open);
				this._config = (PluginConfigurationData)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
				fileStream = null;
			}
			catch (Exception ex)
			{
				if (!(ex is DirectoryNotFoundException) && !(ex is PathTooLongException) && !(ex is FileNotFoundException) && !(ex is IOException) && !(ex is SecurityException) && !(ex is NotSupportedException) && !(ex is ArgumentOutOfRangeException))
				{
					throw;
				}
				string text = string.Format("Can't load configuration from '{0}' file.\n\nDetails: {1}", this._configurationFilePath, ex.Message);
				MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		public void SavePluginConfigurationData()
		{
			TextWriter textWriter = null;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PluginConfigurationData));
				textWriter = new StreamWriter(this._configurationFilePath);
				xmlSerializer.Serialize(textWriter, this._config);
				textWriter.Close();
				textWriter = null;
			}
			catch (Exception ex)
			{
				if (!(ex is DirectoryNotFoundException) && !(ex is PathTooLongException) && !(ex is IOException) && !(ex is UnauthorizedAccessException) && !(ex is SecurityException))
				{
					throw;
				}
				string text = string.Format("Can't save configuration into '{0}' file.\n\nDetails: {1}", this._configurationFilePath, ex.Message);
				MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			finally
			{
				if (textWriter != null)
				{
					textWriter.Close();
				}
			}
		}

		public void LoadFromParameters(string parameters)
		{
			string value = ":";
			string value2 = ";";
			string text = parameters.Trim();
			if (!text.EndsWith(";"))
			{
				text += ";";
			}
			bool flag = false;
			while (!flag)
			{
				int num = text.IndexOf(value);
				int num2 = text.IndexOf(value2);
				if (0 < num && num < num2)
				{
					string variable = text.Substring(0, num).Trim();
					string value3 = text.Substring(num + 1, num2 - num - 1).Trim();
					this.UpdateConfig(this._config, variable, value3);
					text = text.Substring(num2 + 1).TrimStart(new char[0]);
				}
				else
				{
					flag = true;
				}
			}
		}

		public string SaveToParameters()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}: {1}; ", "brush", this._config.Brush);
			if (!string.IsNullOrEmpty(this._config.Highlight))
			{
				stringBuilder.AppendFormat("{0}: [{1}]; ", "highlight", this._config.Highlight);
			}
			if (!this._config.UseServerDefaults)
			{
				stringBuilder.AppendFormat("{0}: {1}; ", "auto-links", this._config.AutoLinks.ToString());
				if (!string.IsNullOrEmpty(this._config.ClassName))
				{
					stringBuilder.AppendFormat("{0}: {1}; ", "class-name", this._config.ClassName);
				}
				stringBuilder.AppendFormat("{0}: {1}; ", "collapse", this._config.Collapse.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "first-line", this._config.FirstLine);
				stringBuilder.AppendFormat("{0}: {1}; ", "gutter", this._config.Gutter);
				stringBuilder.AppendFormat("{0}: {1}; ", "html-script", this._config.HtmlScript.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "light", this._config.Light.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "ruler", this._config.Ruler.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "smart-tabs", this._config.SmartTabs.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "tab-size", this._config.TabSize.ToString());
				stringBuilder.AppendFormat("{0}: {1}; ", "toolbar", this._config.Toolbar.ToString());
			}
			return stringBuilder.ToString().ToLower().Trim();
		}

		private void UpdateConfig(PluginConfigurationData config, string variable, string value)
		{
			switch (variable)
			{
			case "brush":
				config.Brush = this.GetString(value);
				return;
			case "auto-links":
				config.AutoLinks = this.GetBoolean(value, config.AutoLinks);
				return;
			case "class-name":
				config.ClassName = this.GetString(value);
				return;
			case "collapse":
				config.Collapse = this.GetBoolean(value, config.Collapse);
				return;
			case "first-line":
				config.FirstLine = this.GetInt(value, config.FirstLine);
				return;
			case "gutter":
				config.Gutter = this.GetBoolean(value, config.Gutter);
				return;
			case "highlight":
			{
				int num2 = value.IndexOf("[");
				int num3 = value.LastIndexOf("]");
				if (0 <= num2 && num2 < num3)
				{
					config.Highlight = this.GetString(value.Substring(num2 + 1, num3 - num2 - 1));
					return;
				}
				break;
			}
			case "html-script":
				config.HtmlScript = this.GetBoolean(value, config.HtmlScript);
				return;
			case "light":
				config.Light = this.GetBoolean(value, config.Light);
				return;
			case "ruler":
				config.Ruler = this.GetBoolean(value, config.Ruler);
				return;
			case "smart-tabs":
				config.SmartTabs = this.GetBoolean(value, config.SmartTabs);
				return;
			case "tab-size":
				config.TabSize = this.GetInt(value, config.TabSize);
				return;
			case "toolbar":
				config.Toolbar = this.GetBoolean(value, config.Toolbar);
				break;
			}
		}

		private bool GetBoolean(string text, bool defaultValue)
		{
			bool result = false;
			if (!bool.TryParse(text, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		private int GetInt(string text, int defaultValue)
		{
			int result = 0;
			if (!int.TryParse(text, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		private string GetString(string text)
		{
			return text;
		}
	}
}
