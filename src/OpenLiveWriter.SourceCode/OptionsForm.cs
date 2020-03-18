using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OpenLiveWriter.SourceCode
{
	public class OptionsForm : Form
	{
		private Button buttonOK;

		private Button buttonCancel;

		private CheckBox checkBoxAutoLinks;

		private CheckBox checkBoxCollapse;

		private CheckBox checkBoxGutter;

		private CheckBox checkBoxHtmlScript;

		private CheckBox checkBoxLight;

		private CheckBox checkBoxRuler;

		private CheckBox checkBoxSmartTabs;

		private CheckBox checkBoxToolbar;

		private NumericUpDown numericTabSize;

		private Label labelTabSize;

		private Label labelClassName;

		private TextBox textBoxClassName;

		private CheckBox checkLoadFromClipboard;

		private GroupBox groupBox1;

		private CheckBox checkServerDefaults;

		private GroupBox groupBox2;

		private Label labelCodeFont;

		private Button buttonChangeCodeFont;

		private Label labelSize;

		private Label labelFamily;

		public PluginConfigurationData PluginConfigurationData
		{
			get;
			set;
		}

		public OptionsForm(PluginConfigurationData config)
		{
			this.InitializeComponent();
			this.PluginConfigurationData = config;
			this.checkServerDefaults.Checked = this.PluginConfigurationData.UseServerDefaults;
			this.checkBoxAutoLinks.Checked = this.PluginConfigurationData.AutoLinks;
			this.checkBoxCollapse.Checked = this.PluginConfigurationData.Collapse;
			this.checkBoxGutter.Checked = this.PluginConfigurationData.Gutter;
			this.checkBoxHtmlScript.Checked = this.PluginConfigurationData.HtmlScript;
			this.checkBoxLight.Checked = this.PluginConfigurationData.Light;
			this.checkBoxRuler.Checked = this.PluginConfigurationData.Ruler;
			this.checkBoxSmartTabs.Checked = this.PluginConfigurationData.SmartTabs;
			this.checkBoxToolbar.Checked = this.PluginConfigurationData.Toolbar;
			this.checkLoadFromClipboard.Checked = this.PluginConfigurationData.LoadFromClipboard;
			this.numericTabSize.Value = this.PluginConfigurationData.TabSize;
			this.textBoxClassName.Text = (this.PluginConfigurationData.ClassName ?? string.Empty);
			this.labelFamily.Text = this.PluginConfigurationData.CodeFontFamily;
			this.labelSize.Text = this.PluginConfigurationData.CodeFontSize.ToString();
			this.UpdateCodeFont();
		}

		private void UpdateCodeFont()
		{
			this.labelCodeFont.Text = string.Format("{0}, {1}pt", this.labelFamily.Text, this.labelSize.Text);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.PluginConfigurationData.UseServerDefaults = this.checkServerDefaults.Checked;
			this.PluginConfigurationData.AutoLinks = this.checkBoxAutoLinks.Checked;
			this.PluginConfigurationData.Collapse = this.checkBoxCollapse.Checked;
			this.PluginConfigurationData.Gutter = this.checkBoxGutter.Checked;
			this.PluginConfigurationData.HtmlScript = this.checkBoxHtmlScript.Checked;
			this.PluginConfigurationData.Light = this.checkBoxLight.Checked;
			this.PluginConfigurationData.Ruler = this.checkBoxRuler.Checked;
			this.PluginConfigurationData.SmartTabs = this.checkBoxSmartTabs.Checked;
			this.PluginConfigurationData.Toolbar = this.checkBoxToolbar.Checked;
			this.PluginConfigurationData.LoadFromClipboard = this.checkLoadFromClipboard.Checked;
			this.PluginConfigurationData.TabSize = (int)this.numericTabSize.Value;
			this.PluginConfigurationData.ClassName = this.textBoxClassName.Text;
			this.PluginConfigurationData.CodeFontFamily = this.labelFamily.Text;
			this.PluginConfigurationData.CodeFontSize = float.Parse(this.labelSize.Text);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void checkServerDefaults_CheckedChanged(object sender, EventArgs e)
		{
			this.EnableOverrides(!this.checkServerDefaults.Checked);
		}

		private void EnableOverrides(bool enable)
		{
			this.checkBoxAutoLinks.Enabled = enable;
			this.checkBoxCollapse.Enabled = enable;
			this.checkBoxGutter.Enabled = enable;
			this.checkBoxHtmlScript.Enabled = enable;
			this.checkBoxLight.Enabled = enable;
			this.checkBoxRuler.Enabled = enable;
			this.checkBoxSmartTabs.Enabled = enable;
			this.checkBoxToolbar.Enabled = enable;
			this.numericTabSize.Enabled = enable;
			this.textBoxClassName.Enabled = enable;
		}

		private void buttonChangeCodeFont_Click(object sender, EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();
			Font font = new Font(this.labelFamily.Text, float.Parse(this.labelSize.Text));
			fontDialog.Font = font;
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{
				this.labelFamily.Text = fontDialog.Font.FontFamily.Name;
				this.labelSize.Text = fontDialog.Font.SizeInPoints.ToString();
				this.UpdateCodeFont();
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			// ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OptionsForm));
			this.buttonOK = new Button();
			this.buttonCancel = new Button();
			this.checkBoxAutoLinks = new CheckBox();
			this.checkBoxCollapse = new CheckBox();
			this.checkBoxGutter = new CheckBox();
			this.checkBoxHtmlScript = new CheckBox();
			this.checkBoxLight = new CheckBox();
			this.checkBoxRuler = new CheckBox();
			this.checkBoxSmartTabs = new CheckBox();
			this.checkBoxToolbar = new CheckBox();
			this.numericTabSize = new NumericUpDown();
			this.labelTabSize = new Label();
			this.labelClassName = new Label();
			this.textBoxClassName = new TextBox();
			this.checkLoadFromClipboard = new CheckBox();
			this.groupBox1 = new GroupBox();
			this.checkServerDefaults = new CheckBox();
			this.groupBox2 = new GroupBox();
			this.labelSize = new Label();
			this.labelFamily = new Label();
			this.labelCodeFont = new Label();
			this.buttonChangeCodeFont = new Button();
			((ISupportInitialize)this.numericTabSize).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.buttonOK.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.buttonOK.Location = new Point(435, 13);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 13;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonCancel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.buttonCancel.DialogResult = DialogResult.Cancel;
			this.buttonCancel.Location = new Point(434, 43);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new Size(75, 23);
			this.buttonCancel.TabIndex = 14;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
			this.checkBoxAutoLinks.AutoSize = true;
			this.checkBoxAutoLinks.Location = new Point(19, 30);
			this.checkBoxAutoLinks.Name = "checkBoxAutoLinks";
			this.checkBoxAutoLinks.Size = new Size(269, 17);
			this.checkBoxAutoLinks.TabIndex = 0;
			this.checkBoxAutoLinks.Text = "Turn on detection of &links in the highlighted element";
			this.checkBoxAutoLinks.UseVisualStyleBackColor = true;
			this.checkBoxCollapse.AutoSize = true;
			this.checkBoxCollapse.Location = new Point(19, 54);
			this.checkBoxCollapse.Name = "checkBoxCollapse";
			this.checkBoxCollapse.Size = new Size(336, 17);
			this.checkBoxCollapse.TabIndex = 1;
			this.checkBoxCollapse.Text = "Force &highlighted elements on the page to be collapsed by default";
			this.checkBoxCollapse.UseVisualStyleBackColor = true;
			this.checkBoxGutter.AutoSize = true;
			this.checkBoxGutter.Location = new Point(19, 78);
			this.checkBoxGutter.Name = "checkBoxGutter";
			this.checkBoxGutter.Size = new Size(177, 17);
			this.checkBoxGutter.TabIndex = 2;
			this.checkBoxGutter.Text = "Turn on &gutter with line numbers";
			this.checkBoxGutter.UseVisualStyleBackColor = true;
			this.checkBoxHtmlScript.AutoSize = true;
			this.checkBoxHtmlScript.Location = new Point(19, 102);
			this.checkBoxHtmlScript.Name = "checkBoxHtmlScript";
			this.checkBoxHtmlScript.Size = new Size(265, 17);
			this.checkBoxHtmlScript.TabIndex = 3;
			this.checkBoxHtmlScript.Text = "Highlight a mixture of HTML/&XML code and scripts";
			this.checkBoxHtmlScript.UseVisualStyleBackColor = true;
			this.checkBoxLight.AutoSize = true;
			this.checkBoxLight.Location = new Point(19, 126);
			this.checkBoxLight.Name = "checkBoxLight";
			this.checkBoxLight.Size = new Size(236, 17);
			this.checkBoxLight.TabIndex = 4;
			this.checkBoxLight.Text = "&Light version with disabled toolbar and gutter";
			this.checkBoxLight.UseVisualStyleBackColor = true;
			this.checkBoxRuler.AutoSize = true;
			this.checkBoxRuler.Location = new Point(19, 150);
			this.checkBoxRuler.Name = "checkBoxRuler";
			this.checkBoxRuler.Size = new Size(146, 17);
			this.checkBoxRuler.TabIndex = 5;
			this.checkBoxRuler.Text = "Show column &ruler on top";
			this.checkBoxRuler.UseVisualStyleBackColor = true;
			this.checkBoxSmartTabs.AutoSize = true;
			this.checkBoxSmartTabs.Location = new Point(19, 174);
			this.checkBoxSmartTabs.Name = "checkBoxSmartTabs";
			this.checkBoxSmartTabs.Size = new Size(150, 17);
			this.checkBoxSmartTabs.TabIndex = 6;
			this.checkBoxSmartTabs.Text = "Turn on smart ta&bs feature";
			this.checkBoxSmartTabs.UseVisualStyleBackColor = true;
			this.checkBoxToolbar.AutoSize = true;
			this.checkBoxToolbar.Location = new Point(19, 198);
			this.checkBoxToolbar.Name = "checkBoxToolbar";
			this.checkBoxToolbar.Size = new Size(307, 17);
			this.checkBoxToolbar.TabIndex = 7;
			this.checkBoxToolbar.Text = "Shows &toolbar in the upper/right corner of the code element";
			this.checkBoxToolbar.UseVisualStyleBackColor = true;
			this.numericTabSize.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.numericTabSize.Location = new Point(16, 256);
			this.numericTabSize.Name = "numericTabSize";
			this.numericTabSize.Size = new Size(50, 20);
			this.numericTabSize.TabIndex = 10;
			this.labelTabSize.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.labelTabSize.AutoSize = true;
			this.labelTabSize.Location = new Point(16, 237);
			this.labelTabSize.Name = "labelTabSize";
			this.labelTabSize.Size = new Size(50, 13);
			this.labelTabSize.TabIndex = 9;
			this.labelTabSize.Text = "&Tab size:";
			this.labelClassName.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.labelClassName.AutoSize = true;
			this.labelClassName.Location = new Point(90, 238);
			this.labelClassName.Name = "labelClassName";
			this.labelClassName.Size = new Size(101, 13);
			this.labelClassName.TabIndex = 11;
			this.labelClassName.Text = "Custom class &name:";
			this.textBoxClassName.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.textBoxClassName.Location = new Point(93, 256);
			this.textBoxClassName.Name = "textBoxClassName";
			this.textBoxClassName.Size = new Size(292, 20);
			this.textBoxClassName.TabIndex = 12;
			this.checkLoadFromClipboard.AutoSize = true;
			this.checkLoadFromClipboard.Location = new Point(21, 16);
			this.checkLoadFromClipboard.Name = "checkLoadFromClipboard";
			this.checkLoadFromClipboard.Size = new Size(255, 17);
			this.checkLoadFromClipboard.TabIndex = 8;
			this.checkLoadFromClipboard.Text = "T&ake text from clipboard when selection is empty";
			this.checkLoadFromClipboard.UseVisualStyleBackColor = true;
			this.groupBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.groupBox1.Controls.Add(this.checkServerDefaults);
			this.groupBox1.Controls.Add(this.textBoxClassName);
			this.groupBox1.Controls.Add(this.checkBoxAutoLinks);
			this.groupBox1.Controls.Add(this.labelClassName);
			this.groupBox1.Controls.Add(this.checkBoxCollapse);
			this.groupBox1.Controls.Add(this.labelTabSize);
			this.groupBox1.Controls.Add(this.checkBoxGutter);
			this.groupBox1.Controls.Add(this.numericTabSize);
			this.groupBox1.Controls.Add(this.checkBoxHtmlScript);
			this.groupBox1.Controls.Add(this.checkBoxLight);
			this.groupBox1.Controls.Add(this.checkBoxToolbar);
			this.groupBox1.Controls.Add(this.checkBoxRuler);
			this.groupBox1.Controls.Add(this.checkBoxSmartTabs);
			this.groupBox1.Location = new Point(12, 108);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(401, 291);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.checkServerDefaults.AutoSize = true;
			this.checkServerDefaults.Location = new Point(8, 0);
			this.checkServerDefaults.Name = "checkServerDefaults";
			this.checkServerDefaults.Size = new Size(286, 17);
			this.checkServerDefaults.TabIndex = 13;
			this.checkServerDefaults.Text = "Use server &defaults for rendering code in your blog post";
			this.checkServerDefaults.UseVisualStyleBackColor = true;
			this.checkServerDefaults.CheckedChanged += new EventHandler(this.checkServerDefaults_CheckedChanged);
			this.groupBox2.Controls.Add(this.labelSize);
			this.groupBox2.Controls.Add(this.labelFamily);
			this.groupBox2.Controls.Add(this.labelCodeFont);
			this.groupBox2.Controls.Add(this.buttonChangeCodeFont);
			this.groupBox2.Location = new Point(12, 43);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(401, 56);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Font used for code editing";
			this.labelSize.AutoSize = true;
			this.labelSize.Location = new Point(137, 42);
			this.labelSize.Name = "labelSize";
			this.labelSize.Size = new Size(27, 13);
			this.labelSize.TabIndex = 3;
			this.labelSize.Text = "Size";
			this.labelSize.Visible = false;
			this.labelFamily.AutoSize = true;
			this.labelFamily.Location = new Point(60, 42);
			this.labelFamily.Name = "labelFamily";
			this.labelFamily.Size = new Size(36, 13);
			this.labelFamily.TabIndex = 2;
			this.labelFamily.Text = "Family";
			this.labelFamily.Visible = false;
			this.labelCodeFont.AutoSize = true;
			this.labelCodeFont.Location = new Point(20, 26);
			this.labelCodeFont.Name = "labelCodeFont";
			this.labelCodeFont.Size = new Size(62, 13);
			this.labelCodeFont.TabIndex = 1;
			this.labelCodeFont.Text = "CurrentFont";
			this.buttonChangeCodeFont.Location = new Point(310, 19);
			this.buttonChangeCodeFont.Name = "buttonChangeCodeFont";
			this.buttonChangeCodeFont.Size = new Size(75, 23);
			this.buttonChangeCodeFont.TabIndex = 0;
			this.buttonChangeCodeFont.Text = "Chan&ge";
			this.buttonChangeCodeFont.UseVisualStyleBackColor = true;
			this.buttonChangeCodeFont.Click += new EventHandler(this.buttonChangeCodeFont_Click);
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new Size(522, 411);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.checkLoadFromClipboard);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			// base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "OptionsForm";
			this.Text = "Code Options";
			((ISupportInitialize)this.numericTabSize).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
