using System.Text;
using Nice3point.Revit.Extensions.Tests.Coverage.Models;

namespace Nice3point.Revit.Extensions.Tests.Coverage.Formatters;

/// <summary>
///     Renders API surface report rows as Markdown.
/// </summary>
internal static class MarkdownFormatter
{
    /// <param name="rows">The report rows in presentation order.</param>
    extension(IEnumerable<ApiMethodRow> rows)
    {
        /// <summary>
        ///     Renders the rows as a Markdown table.
        /// </summary>
        public string ToMarkdownTable()
        {
            var builder = new StringBuilder();
            builder.AppendLine("| Return type | Method | Parameters | Implementation |");
            builder.AppendLine("| ----------- | ------ | ---------- | -------------- |");

            foreach (var row in rows)
            {
                builder
                    .Append("| ").Append(row.ReturnType)
                    .Append(" | ").Append(row.QualifiedName)
                    .Append(" | ").Append(row.Parameters)
                    .Append(" | ").Append(string.Join(", ", row.ImplementationFiles))
                    .AppendLine(" |");
            }

            return builder.ToString();
        }
    }
}
