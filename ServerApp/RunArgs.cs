using CommandLine;

namespace ServerApp;

public class RunArgs
{
    [Option("addr", Required = true, HelpText = "Listening adress")]
    public string ServerIP { get; set; }

    [Option("loggerCfg", Required = true, HelpText = "Logger config file")]
    public string LoggerCfg { get; set; }

}
