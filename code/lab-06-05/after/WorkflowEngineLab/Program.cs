using System;
using System.Threading.Tasks;

namespace WorkflowEngineLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var engine = new WorkflowEngine();

            engine.StepStarted += (s, name) => Console.WriteLine($"[START] {name}");
            engine.StepCompleted += (s, name) => Console.WriteLine($"[DONE]  {name}");
            engine.StepFailed += (s, name) => Console.WriteLine($"[FAIL]  {name}");

            bool debug = true;

            engine.AddStep("DownloadFile", async () => {
                await Task.Delay(500);
                Console.WriteLine("Downloading file...");
            });

            engine.AddStep("ProcessData", async () => {
                await Task.Delay(500);
                Console.WriteLine("Processing data...");
            });

            engine.AddStep("UploadResults", async () => {
                await Task.Delay(500);
                Console.WriteLine("Uploading results...");
            });

            engine.AddConditionalStep("DebugLog", async () => {
                Console.WriteLine("[DEBUG] Running debug step");
            }, () => debug);

            await engine.ExecuteAsync();
        }
    }
}
