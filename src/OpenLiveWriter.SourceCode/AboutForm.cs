using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OpenLiveWriter.SourceCode
{
	public class AboutForm : Form
	{
		private GroupBox groupBoxPlugin;

		private Button buttonOK;

		private LinkLabel linkDavidPokluda;

		private TextBox textBox1;

		private GroupBox groupBox1;

		private LinkLabel linkHighlighter;

		private TextBox textBox2;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			// ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AboutForm));
			this.groupBoxPlugin = new GroupBox();
			this.linkDavidPokluda = new LinkLabel();
			this.textBox1 = new TextBox();
			this.buttonOK = new Button();
			this.groupBox1 = new GroupBox();
			this.linkHighlighter = new LinkLabel();
			this.textBox2 = new TextBox();
			this.groupBoxPlugin.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxPlugin.Controls.Add(this.linkDavidPokluda);
			this.groupBoxPlugin.Controls.Add(this.textBox1);
			this.groupBoxPlugin.Location = new Point(13, 13);
			this.groupBoxPlugin.Name = "groupBoxPlugin";
			this.groupBoxPlugin.Size = new Size(351, 82);
			this.groupBoxPlugin.TabIndex = 1;
			this.groupBoxPlugin.TabStop = false;
			this.groupBoxPlugin.Text = "Plug-in author";
			this.linkDavidPokluda.AutoSize = true;
			this.linkDavidPokluda.Location = new Point(15, 59);
			this.linkDavidPokluda.Name = "linkDavidPokluda";
			this.linkDavidPokluda.Size = new Size(122, 13);
			this.linkDavidPokluda.TabIndex = 1;
			this.linkDavidPokluda.TabStop = true;
			this.linkDavidPokluda.Text = "http://blog.pokluda.com";
			this.linkDavidPokluda.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkDavidPokluda_LinkClicked);
			this.textBox1.BorderStyle = BorderStyle.None;
			this.textBox1.Location = new Point(15, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new Size(330, 33);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "This plug-in for Windows Live Writer was created by David Pokluda. More projects and information are available at:";
			this.buttonOK.Location = new Point(383, 13);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.groupBox1.Controls.Add(this.linkHighlighter);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Location = new Point(13, 113);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(351, 82);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Syntax highlighter author";
			this.linkHighlighter.AutoSize = true;
			this.linkHighlighter.Location = new Point(15, 59);
			this.linkHighlighter.Name = "linkHighlighter";
			this.linkHighlighter.Size = new Size(244, 13);
			this.linkHighlighter.TabIndex = 1;
			this.linkHighlighter.TabStop = true;
			this.linkHighlighter.Text = "http://alexgorbatchev.com/wiki/SyntaxHighlighter";
			this.linkHighlighter.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkHighlighter_LinkClicked);
			this.textBox2.BorderStyle = BorderStyle.None;
			this.textBox2.Location = new Point(15, 19);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new Size(330, 33);
			this.textBox2.TabIndex = 0;
			this.textBox2.Text = "This plug-in expects syntax highlighter from Alex Gorbatchev. More information available at:";
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(470, 209);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.groupBoxPlugin);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			// base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "AboutForm";
			this.Text = "About information";
			this.groupBoxPlugin.ResumeLayout(false);
			this.groupBoxPlugin.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		public AboutForm()
		{
			this.InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		private void linkDavidPokluda_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.linkDavidPokluda.Text);
		}

		private void linkHighlighter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.linkHighlighter.Text);
		}
	}
}
