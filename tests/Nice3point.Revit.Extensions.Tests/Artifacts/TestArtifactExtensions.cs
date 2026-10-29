namespace Nice3point.Revit.Extensions.Tests.Artifacts;

/// <summary>
///     Attaches files to test output.
/// </summary>
public static class TestArtifactExtensions
{
    /// <param name="content">The artifact content.</param>
    extension(string content)
    {
        /// <summary>
        ///     Write the content to the temporary file.
        /// </summary>
        /// <param name="name">The artifact name.</param>
        public async Task CreateArtifactAsync(string name)
        {
            await CreateArtifactEntryAsync(name, content, extension: null);
        }

        /// <summary>
        ///     Write the content to the temporary file in a Markdown format.
        /// </summary>
        /// <param name="name">The artifact name.</param>
        public async Task CreateMarkdownArtifactAsync(string name)
        {
            await CreateArtifactEntryAsync(name, content, ".md");
        }
    }

    /// <exception cref="InvalidOperationException">The call happens outside a running test.</exception>
    private static async Task CreateArtifactEntryAsync(string name, string content, string? extension)
    {
        var context = TestContext.Current ?? throw new InvalidOperationException($"The '{name}' artifact cannot be attached outside a running test.");

        var fileName = Path.GetRandomFileName();
        if (extension is not null)
        {
            fileName = Path.ChangeExtension(fileName, extension);
        }

        var artifactPath = Path.Combine(Path.GetTempPath(), fileName);
        await File.WriteAllTextAsync(artifactPath, content, context.Execution.CancellationToken);

        context.Output.WriteLine($"Artifact: {artifactPath}");
        context.Output.AttachArtifact(new Artifact
        {
            File = new FileInfo(artifactPath),
            DisplayName = name
        });
    }
}
