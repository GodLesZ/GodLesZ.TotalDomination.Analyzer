using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ImageMagick;

namespace GodLesZ.TotalDomination.Tools.AtlasSplitter {

    class Program {

        static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("<atlas_image> <atlas_xml>");
                return;
            }

            var atlasImage = args[0];
            var atlasXml = args[1];
            var atlasImageName = Path.GetFileNameWithoutExtension(atlasImage);
            var targetDirectory = Path.Combine(Path.GetDirectoryName(atlasImage), atlasImageName);
            if (Directory.Exists(targetDirectory) == false) {
                Directory.CreateDirectory(targetDirectory);
            }

            // <?xml version="1.0"?>
            // <content version="3.9.2">
            // 	<atlas id='21855248' name='ui_common' width='4096' height='4096' format='webp' sig='X4tEj/+6dNNMKmEVrJHQvQ=='>
            var xmlDocument = XDocument.Load(atlasXml);
            foreach (var node in xmlDocument.Root.Elements("atlas")) {
                if (node.Attribute("name").Value != atlasImageName) {
                    continue;
                }

                foreach (var atlasFrame in node.Elements("frame")) {
                    SplitImageFromAtlas(atlasFrame, atlasImage, targetDirectory);
                }
            }
            
            Console.WriteLine("Done");
            Console.Read();
        }

        private static void SplitImageFromAtlas(XElement atlasFrame, string atlasImagePath, string targetDirectory) {
            // <frame id='29650108' name='btn_close_popup' width='146' height='88' frameX='1660' frameY='2414' frameW='146' frameH='88' offsetX='0' offsetY='0' />

            var attributes = atlasFrame.Attributes().ToDictionary(attr => attr.Name.ToString(), attr => attr.Value);
            
            Console.WriteLine("- create frame #{0}: {1}",attributes["id"], attributes["name"]);

            var frameFilename = attributes["name"] + ".png";
            var srcRect = new Rectangle(int.Parse(attributes["frameX"]), int.Parse(attributes["frameY"]), int.Parse(attributes["width"]), int.Parse(attributes["height"]));

            var img = new MagickImage(atlasImagePath);
            img.Crop(new MagickGeometry(srcRect));
            img.Write(Path.Combine(targetDirectory, frameFilename));
            img = null;
            GC.Collect();
        }

    }

}
