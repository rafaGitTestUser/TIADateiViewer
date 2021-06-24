using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TIADateiViewer
{
    public class XmlModel
    {
        public static List<NodeType> GetNodeTypes(string fileName)
        {
            List<NodeType> NodeTypes = new List<NodeType>();

            List<string> types = XDocument.Load(fileName)
                                         .Descendants("node")
                                         .Attributes("Type")
                                         .Select(attr => attr.Value)
                                         .ToList();

            var numberGroups = types.GroupBy(i => i);
            foreach (var grp in numberGroups)
            {
                NodeTypes.Add(new NodeType(grp.Key, grp.Count().ToString()));
            }

            return NodeTypes;
        }
    }

    public class NodeType
    {
        public string Type { get; set; }
        public string NumberOfTypes { get; set; }

        public string Property { get; set; }
        public string NumberOfProperty { get; set; }

        public NodeType(string type, string numberOfTypes)
        {
            Type = type;
            NumberOfTypes = numberOfTypes;
        }
    }
}
