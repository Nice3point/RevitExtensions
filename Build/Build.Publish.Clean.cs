using Nuke.Common.Tools.Git;

sealed partial class Build
{
    Target CleanFailedRelease => _ => _
        .AssuredAfterFailure()
        .OnlyWhenDynamic(() => FailedTargets.Contains(PublishGitHub))
        .TriggeredBy(PublishGitHub)
        .Executes(() =>
        {
            Log.Information("Cleaning failed GitHub release");
            GitTasks.Git($"push --delete origin {Version}", logInvocation: false);
        });
}