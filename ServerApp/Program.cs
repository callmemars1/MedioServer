using AutoMapper;
using CommandLine;
using Medio.Network;
using Medio.Sessions.PvP;
using NLog;
using NLog.Config;
using System.Net;

namespace ServerApp;

class Server
{
    static async Task Main(string[] args)
    {
        _ = await Parser.Default.ParseArguments<RunArgs>(args).WithParsedAsync(StartUp);

    }
    static async Task StartUp(RunArgs args)
    {
        // Зависимости логгера пока прокидываются через синглтон, в будущем возможен
        // переход на DI
        LogManager.Configuration = new XmlLoggingConfiguration(args.LoggerCfg);

        var local = IPEndPoint.Parse(args.ServerIP);
        var sessionCreator = new SessionPvPCreator(local);
        var session = sessionCreator.Create();
        await session.StartAsync();
    }

}

