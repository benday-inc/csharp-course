using System.Diagnostics;
using System.Reflection;
using System.Text;
using Benday.CommandsFramework;
using LinqLab.Api.Commands;

namespace LinqLab.ConsoleUi;

class Program
{
    static void Main(string[] args)
    {
        var assembly = typeof(SampleCommand).Assembly;

        var versionInfo =
            FileVersionInfo.GetVersionInfo(
                Assembly.GetExecutingAssembly().Location);

        var options = new DefaultProgramOptions();

        options.Version = $"v{versionInfo.FileVersion}";
        options.ApplicationName = "Movie Database";
        options.Website = "[add your website url here]";
        options.UsesConfiguration = false;

        var program = new DefaultProgram(options, assembly);

        program.Run(args);
    }
}