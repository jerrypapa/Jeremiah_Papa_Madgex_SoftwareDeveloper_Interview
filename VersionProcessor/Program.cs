using Application.Services;
using ApplicationVersionProcessor.Core.Interfaces;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        args = new[] { "bugfix","Feature","blahblah" };


        try
        {
            var versionIncrementer = serviceProvider.GetRequiredService<IVersionIncrementer>();
            var newVersion =await versionIncrementer.IncrementVersion(args[0]);

            Console.WriteLine($"Updated Release Version Is: {newVersion.FirstNumber}.{newVersion.SecondNumber}.{newVersion.Major}.{newVersion.Minor}");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Log.Logger = new LoggerConfiguration().WriteTo.File("D:\\Test\\Logs\\log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        Log.CloseAndFlush(); 
    
}

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddTransient<IVersionIncrementer, VersionIncrementer>()
            .AddTransient<IVersionProvider, VersionProvider>()
            .AddTransient<IVersionRepository, FileVersionRepository>()
            .BuildServiceProvider();
    }
}
