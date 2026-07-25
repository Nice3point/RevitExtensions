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
        [Pure]
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

    /// <param name="rows">The report rows in presentation order.</param>
    extension(IEnumerable<ApiCollectionRow> rows)
    {
        /// <summary>
        ///     Renders the rows as a Markdown table.
        /// </summary>
        [Pure]
        public string ToMarkdownTable()
        {
            var builder = new StringBuilder();
            builder.AppendLine("| Kind | Collection | Element | Iterator | foreach yields | Issues | Implementation |");
            builder.AppendLine("| ---- | ---------- | ------- | -------- | -------------- | ------ | -------------- |");

            foreach (var row in rows)
            {
                builder
                    .Append("| ").Append(row.Kind)
                    .Append(" | ").Append(row.TypeName)
                    .Append(" | ").Append(row.ElementType)
                    .Append(" | ").Append(row.IteratorType)
                    .Append(" | ").Append(row.EnumeratedType)
                    .Append(" | ").Append(string.Join(", ", row.Issues))
                    .Append(" | ").Append(string.Join(", ", row.ImplementationFiles))
                    .AppendLine(" |");
            }

            return builder.ToString();
        }
    }

    /// <param name="rows">The report rows in presentation order.</param>
    extension(IEnumerable<ApiMapRow> rows)
    {
        /// <summary>
        ///     Renders the rows as a Markdown table.
        /// </summary>
        [Pure]
        public string ToMarkdownTable()
        {
            var builder = new StringBuilder();
            builder.AppendLine("| Map | Key | Value | Iterator | foreach yields | Issues | Implementation |");
            builder.AppendLine("| --- | --- | ----- | -------- | -------------- | ------ | -------------- |");

            foreach (var row in rows)
            {
                builder
                    .Append("| ").Append(row.TypeName)
                    .Append(" | ").Append(row.KeyType)
                    .Append(" | ").Append(row.ValueType)
                    .Append(" | ").Append(row.IteratorType)
                    .Append(" | ").Append(row.EnumeratedType)
                    .Append(" | ").Append(string.Join(", ", row.Issues))
                    .Append(" | ").Append(string.Join(", ", row.ImplementationFiles))
                    .AppendLine(" |");
            }

            return builder.ToString();
        }
    }
}
