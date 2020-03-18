using System;
using System.Collections.Generic;

namespace OpenLiveWriter.SourceCode
{
    internal sealed class BrushItem : IEquatable<BrushItem>
    {
        public static readonly BrushItem[] BrushItems
            = new[] {
                new BrushItem("csharp", "C#", 100),
                new BrushItem("js", "Javascript", 100),
                new BrushItem("sql", "SQL", 100),
                new BrushItem("py", "Python", 80),
                new BrushItem("cpp", "C++", 90),
                new BrushItem("css", "Css", 80),
                new BrushItem("scala", "Scala", 80),
                new BrushItem("bash", "Bash", 100),
                new BrushItem("ps", "Powershell", 80),
                new BrushItem("vb", "Visual Basic", 80),
                new BrushItem("text", "Text", 100),
                new BrushItem("xml", "XML/HTML", 100),
                new BrushItem("pas", "Delphi/Pascal", 80),
                new BrushItem("java", "Java", 90),
                new BrushItem("typescript", "Typescript", 100),
                new BrushItem("yaml", "YAML", 100),
            };

        public BrushItem(string name, string displayName, int rank = 0)
        {
            Name = name;
            DisplayName = displayName;
            Rank = rank;
        }

        public string DisplayName { get; }

        public string Name { get; }

        public int Rank { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BrushItem);
        }

        public bool Equals(BrushItem other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString() => DisplayName;
    }
}