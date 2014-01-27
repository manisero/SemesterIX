using System.Collections.Generic;
using System.IO;
using System.Text;
using GRM.Logic;
using GRM.Logic.DataSetProcessing.Entities;
using System.Linq;

namespace GRM.Presentation.ResultWriting
{
    public class CSVResultWriter
    {
        private const string SEPARATOR = ";";

        public void WriteResult(GRMResult result, string outputFilePath)
        {
            using (var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fileStream))
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine(FormatHeaders(result));

                foreach (var rule in result.Rules)
                {
                    foreach (var generator in rule.Generators)
                    {
                        stringBuilder.AppendLine(FormatGenerator(generator, result.AttributesCount, result.DecisionAttributeIndex, rule.Decision));
                    }
                }

                writer.Write(stringBuilder.ToString());
            }
        }

        private string FormatHeaders(GRMResult result)
        {
            var headers = new List<string>();

            for (int i = 0; i < result.AttributesCount; i++)
            {
                if (i == result.DecisionAttributeIndex)
                {
                    continue;
                }

                if (result.AttributeNames != null)
                {
                    headers.Add(result.AttributeNames[i]);
                }
                else
                {
                    headers.Add(string.Format("Attribute {0}", i + 1));
                }
            }

            if (result.AttributeNames != null)
            {
                headers.Add(result.AttributeNames[result.DecisionAttributeIndex]);
            }
            else
            {
                headers.Add("Decision");
            }

            return string.Join(SEPARATOR, headers.ToArray());
        }

        private string FormatGenerator(IEnumerable<Item> generator, int attributesCount, int decisionAttributeIndex, string decision)
        {
            var attributeValues = new List<string>();

            for (int i = 0; i < attributesCount; i++)
            {
                if (i == decisionAttributeIndex)
                {
                    continue;
                }

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

            attributeValues.Add(decision);

            return string.Join(SEPARATOR, attributeValues.ToArray());
        }
    }
}