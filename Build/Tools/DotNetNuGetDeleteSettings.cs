using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

namespace Tools;

[PublicAPI]
[Serializable]
public class DotNetNuGetDeleteSettings : ToolSettings
{
    public override string ProcessToolPath => base.ProcessToolPath ?? DotNetTasks.DotNetPath;
    public override Action<OutputType, string> ProcessCustomLogger => DotNetTasks.DotNetLogger;
    public virtual string Package { get; internal set; }
    public virtual string Version { get; internal set; }
    public virtual string Source { get; internal set; }
    public virtual string SymbolSource { get; internal set; }
    public virtual int? Timeout { get; internal set; }
    public virtual string ApiKey { get; internal set; }
    public virtual string SymbolApiKey { get; internal set; }
    public virtual bool? DisableBuffering { get; internal set; }
    public virtual bool? NoSymbols { get; internal set; }
    public virtual bool? ForceEnglishOutput { get; internal set; }
    public virtual bool? SkipDuplicate { get; internal set; }
    public virtual bool? NoServiceEndpoint { get; internal set; }

    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
            .Add("nuget delete")
            .Add("{value}", Package)
            .Add("{value}", Version)
            .Add("--source {value}", Source)
            .Add("--symbol-source {value}", SymbolSource)
            .Add("--timeout {value}", Timeout)
            .Add("--api-key {value}", ApiKey, secret: true)
            .Add("--symbol-api-key {value}", SymbolApiKey, secret: true)
            .Add("--disable-buffering", DisableBuffering)
            .Add("--no-symbols", NoSymbols)
            .Add("--force-english-output", ForceEnglishOutput)
            .Add("--skip-duplicate", SkipDuplicate)
            .Add("--no-service-endpoint", NoServiceEndpoint);
        return base.ConfigureProcessArguments(arguments);
    }
}