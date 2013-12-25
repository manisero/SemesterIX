using System.IO;
using System.Text;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Presentation
{
    public class GRMResultWriter
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
                        // TODO: Handle rules with not full sets of attributes
                        stringBuilder.AppendLine(string.Join(",", generator.OrderBy(x => x.AttributeID).Select(x => x.Value).ToArray()));
                    }

                    stringBuilder.AppendLine();
                }

                writer.Write(stringBuilder.ToString());
            }
        }
    }
}