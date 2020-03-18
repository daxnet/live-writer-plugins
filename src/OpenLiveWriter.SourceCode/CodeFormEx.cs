using System;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace OpenLiveWriter.SourceCode
{
	public class CodeFormEx : Form
	{
		private const string prePrefix = "<pre class=\"";

		private const string preSuffix = "\">";

		private const string preClose = "</pre>";

		private PluginConfigurationRepository _configDb = new PluginConfigurationRepository();

		private string _code;

		private Button buttonOK;

		private Button buttonCancel;

		private Button buttonAbout;

		private Button buttonOptions;

		private ComboBox comboBrush;

		private Label label1;

		private Label label2;

		private NumericUpDown numericFirstLine;

		private Label label3;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private CodeEditor editor;
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

		public CodeFormEx(string code)
		{
			this._configDb.LoadPluginConfigurationData();
			this.Code = code;
			this.InitializeComponent();
			this.LoadBrushes();
            // this.comboBrush.Text = this._configDb.Config.Brush;
            this.comboBrush.SelectedItem = BrushItem.BrushItems.First(b => b.Name.Equals(this._configDb.Config.Brush));
			this.numericFirstLine.Value = this._configDb.Config.FirstLine;
			this.textHighlight.Text = this._configDb.Config.Highlight;
            //this.textCode.Text = this._code;
            this.editor.Text = this._code;
            this.editor.LineDoubleClick += Editor_LineDoubleClick;
			//this.textCode.Font = new Font(this._configDb.Config.CodeFontFamily, this._configDb.Config.CodeFontSize);
			if (this._configDb.Config.MainFormHeight > 0 && this._configDb.Config.MainFormWidth > 0)
			{
				base.Left = this._configDb.Config.MainFormX;
				base.Top = this._configDb.Config.MainFormY;
				base.Width = this._configDb.Config.MainFormWidth;
				base.Height = this._configDb.Config.MainFormHeight;
				base.StartPosition = FormStartPosition.Manual;
			}

            this.editor.Focus();
            this.editor.SetFocus();
		}

        private void Editor_LineDoubleClick(object sender, LineDoubleClickedEventArgs e)
        {
            this.AddLineToHighlight(e.Line);
        }

        private void AddLineToHighlight(int line)
        {
            if (string.IsNullOrEmpty(textHighlight.Text))
            {
                textHighlight.Text = line.ToString();
            }
            else
            {
                var linesHighlighted = textHighlight.Text.Split(',').Select(x => Convert.ToInt32(x)).OrderBy(_ => _).ToList();
                if (!linesHighlighted.Any(x => x == line))
                {
                    var idx = 0;
                    while (idx < linesHighlighted.Count)
                    {
                        if (linesHighlighted[idx] < line)
                        {
                            idx++;
                        }
                        else
                        {
                            linesHighlighted.Insert(idx, line);
                            break;
                        }

                        if (idx == linesHighlighted.Count)
                        {
                            linesHighlighted.Add(line);
                            break;
                        }
                    }

                    textHighlight.Text = string.Join(",", linesHighlighted.ToArray());
                }
                else
                {
                    linesHighlighted.Remove(line);
                    textHighlight.Text = string.Join(",", linesHighlighted.ToArray());
                }
            }
        }

        private void LoadBrushes()
        {
            this.comboBrush.Items.Clear();
            foreach (var brushItem in BrushItem.BrushItems.OrderByDescending(i => i.Rank).ThenBy(i => i.DisplayName))
            {
                this.comboBrush.Items.Add(brushItem);
            }
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
					//this.textCode.Font = new Font(this._configDb.Config.CodeFontFamily, this._configDb.Config.CodeFontSize);
					//this.textCode.Update();
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
            var selectedBrush = (BrushItem)this.comboBrush.SelectedItem;
			this._configDb.Config.Brush = selectedBrush?.Name;
			this._configDb.Config.FirstLine = (int)this.numericFirstLine.Value;
			this._configDb.Config.Highlight = this.textHighlight.Text;
			this._configDb.Config.MainFormX = base.Left;
			this._configDb.Config.MainFormY = base.Top;
			this._configDb.Config.MainFormWidth = base.Width;
			this._configDb.Config.MainFormHeight = base.Height;
			this._configDb.SavePluginConfigurationData();
            //this._code = this.textCode.Text;
            this._code = this.editor.Text;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeFormEx));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.comboBrush = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericFirstLine = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.textHighlight = new System.Windows.Forms.TextBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.editor = new OpenLiveWriter.SourceCode.CodeEditor();
            ((System.ComponentModel.ISupportInitialize)(this.numericFirstLine)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(720, 583);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 28);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(815, 583);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 28);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbout.Location = new System.Drawing.Point(626, 583);
            this.buttonAbout.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(88, 28);
            this.buttonAbout.TabIndex = 5;
            this.buttonAbout.Text = "&About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonOptions
            // 
            this.buttonOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOptions.Location = new System.Drawing.Point(531, 583);
            this.buttonOptions.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(88, 28);
            this.buttonOptions.TabIndex = 4;
            this.buttonOptions.Text = "O&ptions";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // comboBrush
            // 
            this.comboBrush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBrush.FormattingEnabled = true;
            this.comboBrush.Location = new System.Drawing.Point(14, 585);
            this.comboBrush.Margin = new System.Windows.Forms.Padding(4);
            this.comboBrush.Name = "comboBrush";
            this.comboBrush.Size = new System.Drawing.Size(110, 24);
            this.comboBrush.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 566);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "C&ode language:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 566);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "&First line number:";
            // 
            // numericFirstLine
            // 
            this.numericFirstLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericFirstLine.Location = new System.Drawing.Point(151, 587);
            this.numericFirstLine.Margin = new System.Windows.Forms.Padding(4);
            this.numericFirstLine.Name = "numericFirstLine";
            this.numericFirstLine.Size = new System.Drawing.Size(97, 23);
            this.numericFirstLine.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 566);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "&Lines highlighted (comma separated):";
            // 
            // textHighlight
            // 
            this.textHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textHighlight.Location = new System.Drawing.Point(276, 585);
            this.textHighlight.Margin = new System.Windows.Forms.Padding(4);
            this.textHighlight.Name = "textHighlight";
            this.textHighlight.Size = new System.Drawing.Size(247, 23);
            this.textHighlight.TabIndex = 3;
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(18, 16);
            this.elementHost1.Margin = new System.Windows.Forms.Padding(4);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(885, 546);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.editor;
            // 
            // CodeFormEx
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(916, 626);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.textHighlight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericFirstLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBrush);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CodeFormEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert source code plug-in version 2.0";
            ((System.ComponentModel.ISupportInitialize)(this.numericFirstLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
