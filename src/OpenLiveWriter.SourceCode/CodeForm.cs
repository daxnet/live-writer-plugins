using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace OpenLiveWriter.SourceCode
{
	public class CodeForm : Form
	{
		private const string prePrefix = "<pre class=\"";

		private const string preSuffix = "\">";

		private const string preClose = "</pre>";

		private PluginConfigurationRepository _configDb = new PluginConfigurationRepository();

		private string _code;

		private Button buttonOK;

		private Button buttonCancel;

		private TextBox textCode;

		private Button buttonAbout;

		private Button buttonOptions;

		private ComboBox comboBrush;

		private Label label1;

		private Label label2;

		private NumericUpDown numericFirstLine;

		private Label label3;

		private TextBox textHighlight;

		public string Code
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}{1}{2}", "<pre class=\"", this._configDb.SaveToParameters(), "\">");
				stringBuilder.Append(HttpUtility.HtmlEncode(this._code));
				stringBuilder.Append("</pre>");
				return stringBuilder.ToString();
			}
			set
			{
				int num = value.IndexOf("<pre class=\"");
				int num2 = value.IndexOf("\">");
				int num3 = value.LastIndexOf("</pre>");
				if (0 <= num && num < num2 && num2 < num3)
				{
					num += "<pre class=\"".Length;
					string parameters = value.Substring(num, num2 - num - 1);
					this._configDb.LoadFromParameters(parameters);
					num2 += "\">".Length;
					this._code = value.Substring(num2, num3 - num2);
					return;
				}
				if (!string.IsNullOrEmpty(value))
				{
					this._code = value;
					return;
				}
				if (this._configDb.Config.LoadFromClipboard)
				{
					this._code = Clipboard.GetText();
				}
			}
		}

		public CodeForm(string code)
		{
			this._configDb.LoadPluginConfigurationData();
			this.Code = code;
			this.InitializeComponent();
			this.LoadBrushes();
			this.comboBrush.Text = this._configDb.Config.Brush;
			this.numericFirstLine.Value = this._configDb.Config.FirstLine;
			this.textHighlight.Text = this._configDb.Config.Highlight;
			this.textCode.Text = this._code;
			this.textCode.Font = new Font(this._configDb.Config.CodeFontFamily, this._configDb.Config.CodeFontSize);
			if (this._configDb.Config.MainFormHeight > 0 && this._configDb.Config.MainFormWidth > 0)
			{
				base.Left = this._configDb.Config.MainFormX;
				base.Top = this._configDb.Config.MainFormY;
				base.Width = this._configDb.Config.MainFormWidth;
				base.Height = this._configDb.Config.MainFormHeight;
				base.StartPosition = FormStartPosition.Manual;
			}
		}

		private void LoadBrushes()
		{
			this.comboBrush.Items.Clear();
			this.comboBrush.Items.Add("csharp");
			this.comboBrush.Items.Add("bash");
			this.comboBrush.Items.Add("shell");
			this.comboBrush.Items.Add("cpp");
			this.comboBrush.Items.Add("css");
			this.comboBrush.Items.Add("pas");
			this.comboBrush.Items.Add("patch");
			this.comboBrush.Items.Add("groovy");
			this.comboBrush.Items.Add("xml");
			this.comboBrush.Items.Add("js");
			this.comboBrush.Items.Add("java");
			this.comboBrush.Items.Add("php");
			this.comboBrush.Items.Add("text");
			this.comboBrush.Items.Add("py");
			this.comboBrush.Items.Add("rails");
			this.comboBrush.Items.Add("bash");
			this.comboBrush.Items.Add("sql");
			this.comboBrush.Items.Add("vb");
			this.comboBrush.Items.Add("powershell");
			this.comboBrush.Items.Add("fsharp");
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			using (AboutForm aboutForm = new AboutForm())
			{
				aboutForm.ShowDialog();
			}
		}

		private void buttonOptions_Click(object sender, EventArgs e)
		{
			using (OptionsForm optionsForm = new OptionsForm(this._configDb.Config))
			{
				if (optionsForm.ShowDialog() == DialogResult.OK)
				{
					this._configDb.Config = optionsForm.PluginConfigurationData;
					this._configDb.SavePluginConfigurationData();
					this.textCode.Font = new Font(this._configDb.Config.CodeFontFamily, this._configDb.Config.CodeFontSize);
					this.textCode.Update();
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this._configDb.Config.Brush = this.comboBrush.Text;
			this._configDb.Config.FirstLine = (int)this.numericFirstLine.Value;
			this._configDb.Config.Highlight = this.textHighlight.Text;
			this._configDb.Config.MainFormX = base.Left;
			this._configDb.Config.MainFormY = base.Top;
			this._configDb.Config.MainFormWidth = base.Width;
			this._configDb.Config.MainFormHeight = base.Height;
			this._configDb.SavePluginConfigurationData();
			this._code = this.textCode.Text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CodeForm));
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.textCode = new TextBox();
			this.buttonAbout = new Button();
			this.buttonOptions = new Button();
			this.comboBrush = new ComboBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.numericFirstLine = new NumericUpDown();
			this.label3 = new Label();
			this.textHighlight = new TextBox();
			((ISupportInitialize)this.numericFirstLine).BeginInit();
			base.SuspendLayout();
			this.buttonOK.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.buttonOK.Location = new Point(531, 13);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 7;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.Location = new Point(530, 43);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 8;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.textCode.AcceptsReturn = true;
			this.textCode.AcceptsTab = true;
			this.textCode.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.textCode.Location = new Point(13, 13);
			this.textCode.Multiline = true;
			this.textCode.Name = "textCode";
			this.textCode.ScrollBars = ScrollBars.Both;
			this.textCode.Size = new Size(511, 371);
			this.textCode.TabIndex = 0;
			this.buttonAbout.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.buttonAbout.Location = new Point(531, 417);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new Size(75, 23);
			this.buttonAbout.TabIndex = 10;
			this.buttonAbout.Text = "&About";
			this.buttonAbout.UseVisualStyleBackColor = true;
			this.buttonAbout.Click += new EventHandler(this.buttonAbout_Click);
			this.buttonOptions.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.buttonOptions.Location = new Point(530, 388);
			this.buttonOptions.Name = "buttonOptions";
			this.buttonOptions.Size = new Size(75, 23);
			this.buttonOptions.TabIndex = 9;
			this.buttonOptions.Text = "O&ptions";
			this.buttonOptions.UseVisualStyleBackColor = true;
			this.buttonOptions.Click += new EventHandler(this.buttonOptions_Click);
			this.comboBrush.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.comboBrush.FormattingEnabled = true;
			this.comboBrush.Location = new Point(12, 419);
			this.comboBrush.Name = "comboBrush";
			this.comboBrush.Size = new Size(95, 21);
			this.comboBrush.TabIndex = 2;
			this.label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 403);
			this.label1.Name = "label1";
			this.label1.Size = new Size(82, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "C&ode language:";
			this.label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(127, 403);
			this.label2.Name = "label2";
			this.label2.Size = new Size(86, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "&First line number:";
			this.numericFirstLine.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.numericFirstLine.Location = new Point(130, 420);
			this.numericFirstLine.Name = "numericFirstLine";
			this.numericFirstLine.Size = new Size(83, 20);
			this.numericFirstLine.TabIndex = 4;
			this.label3.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(234, 403);
			this.label3.Name = "label3";
			this.label3.Size = new Size(182, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "&Lines highlighted (comma separated):";
			this.textHighlight.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.textHighlight.Location = new Point(237, 419);
			this.textHighlight.Name = "textHighlight";
			this.textHighlight.Size = new Size(287, 20);
			this.textHighlight.TabIndex = 6;
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new Size(618, 452);
			base.Controls.Add(this.textHighlight);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.numericFirstLine);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBrush);
			base.Controls.Add(this.buttonOptions);
			base.Controls.Add(this.buttonAbout);
			base.Controls.Add(this.textCode);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Name = "CodeForm";
			this.Text = "Insert source code plug-in version 2.0";
			((ISupportInitialize)this.numericFirstLine).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
