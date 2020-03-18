using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLiveWriter.SourceCode
{
    public class LineDoubleClickedEventArgs : EventArgs
    {
        public LineDoubleClickedEventArgs(int line) => Line = line;

        public int Line { get; }
    } 
}
