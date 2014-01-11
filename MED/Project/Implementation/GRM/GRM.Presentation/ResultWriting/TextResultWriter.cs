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

                    stringBuilder.AppendLine(string.Format("Decision: '{0}':", rule.Decision));

                    stringBuilder.AppendLine(SUBSEPARATOR);

                    foreach (var generator in rule.Generators)
                    {
                        stringBuilder.AppendLine(FormatGenerator(generator));
                    }

                    stringBuilder.AppendLine();
                }

                writer.Write(stringBuilder.ToString());
            }
        }

        private string FormatGenerator(IEnumerable<Item> generator)
        {
            var items = generator.OrderBy(x => x.AttributeID);
            var maxAttributeId = items.Last().AttributeID;

            var attributeValues = new List<string>();

            for (int i = 0; i <= maxAttributeId; i++)
            {
                if (generator.Any(x => x.AttributeID == i))
                {
                    var item = generator.Single(x => x.AttributeID == i);
                    attributeValues.Add(item.Value);
                }
                else
                {
                    attributeValues.Add(string.Empty);
                }
            }

            return string.Join(",", attributeValues.ToArray());
        }
    }
}