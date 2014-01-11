using System.Collections.Generic;
using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Presentation.ResultWriting
{
    public class TextResultWriter
    {
        private const string SEPARATOR = "====================";
        private const string SUBSEPARATOR = "--------------------";

        public void WriteResult(GRMResult result, string outputFilePath)
        {
            using (var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fileStream))
            {
                var stringBuilder = new StringBuilder();

                foreach (var rule in result.Rules)
                {
                    stringBuilder.AppendLine(SEPARATOR);

                    stringBuilder.AppendLine(string.Format("{0}: '{1}':", GetDecisionAttributeName(result.DecisionAttributeIndex, result.AttributeNames), rule.Decision));

                    stringBuilder.AppendLine(SUBSEPARATOR);

                    foreach (var generator in rule.Generators)
                    {
                        stringBuilder.AppendLine(FormatGenerator(generator, result.AttributeNames));
                    }

                    stringBuilder.AppendLine();
                }

                writer.Write(stringBuilder.ToString());
            }
        }

        private string GetDecisionAttributeName(int decisionAttributeIndex, IDictionary<int, string> attributeNames)
        {
            return attributeNames != null
                       ? attributeNames[decisionAttributeIndex]
                       : "Decision";
        }

        private string FormatGenerator(IEnumerable<Item> generator, IDictionary<int, string> attributeNames)
        {
            var attributes = new List<string>();

            foreach (var item in generator.OrderBy(x => x.AttributeID))
            {
                var attribute = string.Format("{0} ({1})", GetAttributeName(item.AttributeID, attributeNames), item.Value);

                attributes.Add(attribute);
            }

            return string.Join(", ", attributes.ToArray());
        }

        private string GetAttributeName(int attributeId, IDictionary<int, string> attributeNames)
        {
            return attributeNames != null
                       ? attributeNames[attributeId]
                       : string.Format("Attribute {0}", attributeId + 1);
        }
    }
}