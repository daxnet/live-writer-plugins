using OpenLiveWriter.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlDoc = HtmlAgilityPack.HtmlDocument;

namespace OpenLiveWriter.AutoImageHeight
{
    [InsertableContentSource("Auto Image Height", SidebarText = "Auto Image Height"),
     WriterPlugin("D6C9E7FF-914B-42C1-B7F4-01D69F2EE373",
        "Auto Image Height",
        ImagePath = "images.png",
        Description = "Change the height of all images to 'auto'.")]
    public class Plugin : ContentSource
    {
        public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show(dialogOwner, "The content is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return DialogResult.Cancel;
            }
            var htmlDoc = new HtmlDoc();
            htmlDoc.LoadHtml(content);
            var total = 0;
            htmlDoc.DocumentNode.Descendants("img")
                .ToList()
                .ForEach(node =>
                {
                    total++;
                    node.SetAttributeValue("height", "auto");
                });

            content = htmlDoc.DocumentNode.OuterHtml;

            MessageBox.Show(dialogOwner, $"{total} image(s) processed successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return DialogResult.OK;
        }
    }
}
