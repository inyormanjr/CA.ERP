using jsreport.Binary;
using jsreport.Local;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CA.ERP.JSReport.Studio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting jsreport studio");
            var rs = new LocalReporting()
                .Configure(cfg => cfg.AllowedLocalFilesAccess())
             .UseBinary(JsReportBinary.GetBinary())
             .KillRunningJsReportProcesses()
             .RunInDirectory(Path.Combine(Directory.GetCurrentDirectory(), "../../../../../CA.ERP.WebApp/jsreport"))
             .Configure(cfg => cfg.CreateSamples()
                .FileSystemStore()
                .BaseUrlAsWorkingDirectory())
             .AsWebServer()
             .RedirectOutputToConsole()
             .Create();

            rs.StartAsync().Wait();

            Process.Start(new ProcessStartInfo("cmd", $"/c start http://localhost:5488"));

            Console.ReadKey();

            rs.KillAsync().Wait();
        }
    }
}
