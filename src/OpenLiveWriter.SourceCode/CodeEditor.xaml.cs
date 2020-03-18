using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace OpenLiveWriter.SourceCode
{
    /// <summary>
    /// Interaction logic for CodeEditor.xaml
    /// </summary>
    public partial class CodeEditor : UserControl
    {
        public CodeEditor()
        {
            InitializeComponent();
        }

        public event EventHandler<LineDoubleClickedEventArgs> LineDoubleClick;

        public bool IsReadOnly
        {
            get => avalonTextEditor.IsReadOnly;
            set => avalonTextEditor.IsReadOnly = value;
        }

        public string Text
        {
            get => avalonTextEditor.Text;
            set => avalonTextEditor.Text = value;
        }

        public void SetFocus()
        {
            
            this.avalonTextEditor.SelectionStart = 0;
            this.avalonTextEditor.SelectionLength = 0;
            this.avalonTextEditor.Focus();
        }

        private string syntaxHighlightingDefinitionResourceName;

        public string SyntaxHighlightingDefinitionResourceName
        {
            get => syntaxHighlightingDefinitionResourceName;
            set
            {
                if (!string.Equals(syntaxHighlightingDefinitionResourceName, value))
                {
                    syntaxHighlightingDefinitionResourceName = value;
                    using (var stream = Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(value))
                    {
                        using (var xmlReader = new XmlTextReader(stream))
                        {
                            avalonTextEditor.SyntaxHighlighting = HighlightingLoader.Load(xmlReader, HighlightingManager.Instance);
                        }
                    }
                }
            }
        }

        private void AvalonTextEditor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.OnLineDoubleClicked(this.avalonTextEditor.TextArea.Caret.Line);
        }

        private void OnLineDoubleClicked(int line)
        {
            this.LineDoubleClick?.Invoke(this, new LineDoubleClickedEventArgs(line));
        }
    }
}
