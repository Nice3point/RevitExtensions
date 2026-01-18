using ModularPipelines.Attributes;
using ModularPipelines.Configuration;
using ModularPipelines.Context;
using ModularPipelines.Git.Extensions;
using ModularPipelines.Modules;

namespace Build.Modules;

[DependsOn<PackProjectsModule>]
[DependsOn<UpdateReadmeModule>]
public sealed class RestoreReadmeModule : Module
{
    protected override ModuleConfiguration Configure() => ModuleConfiguration.Create()
        .WithAlwaysRun()
        .WithIgnoreFailuresWhen(async (context, _) =>
        {
            var nugetReadmeModule = await context.GetModule<UpdateReadmeModule>();
            return nugetReadmeModule.IsSuccess;
        })
        .Build();

    protected override async Task ExecuteModuleAsync(IModuleContext context, CancellationToken cancellationToken)
    {
        var nugetReadmeResult = await context.GetModule<UpdateReadmeModule>();
        if (!nugetReadmeResult.IsSuccess)
        {
            return;
        }

        var nugetReadme = nugetReadmeResult.ValueOrDefault!;
        var readmePath = context.Git().RootDirectory.GetFile("Readme.md");
        await readmePath.WriteAsync(nugetReadme, cancellationToken);
    }
}