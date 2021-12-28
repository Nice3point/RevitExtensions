using Nuke.Common;
using static Nuke.Common.IO.FileSystemTasks;

partial class Build
{
    Target Cleaning => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(ArtifactsDirectory);
        });
}