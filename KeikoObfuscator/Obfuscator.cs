using System;
using System.IO;
using KeikoObfuscator.JunkGeneration;
using KeikoObfuscator.Renaming;
using Mono.Cecil;
namespace KeikoObfuscator
{
    public static class Obfuscator
	{
		public static Opts current;
        public static void Obfuscate(AssemblyDefinition assembly, ILogOutput logOutput)
		{

			StartupSettings();
            logOutput.WriteMessage("Obfuscator module written by Marwix (2016).");
            logOutput.WriteMessage("---------------------------------------------------");

            var startTime = DateTime.Now;
            logOutput.WriteMessage("Started obfuscation process at " + startTime);

            var context = new ObfuscationContext(assembly, logOutput);

            var tasks = new ObfuscatorTask[]
            {
                new JunkCodeGenerationTask(),
                new SymbolRenamingTask(),
            };

            foreach (var task in tasks)
            {
                logOutput.WriteMessage("Initializing " + task.Name + "...");
                task.Initialize(context);
            }

            foreach (var task in tasks)
            {
                logOutput.WriteMessage("Applying " + task.Name + "...");
                task.ApplyAll(context);
            }

            foreach (var task in tasks)
            {
                logOutput.WriteMessage("Finalizing " + task.Name + "...");
                task.Finalize(context);
            }

            var endTime = DateTime.Now;

            logOutput.WriteMessage("---------------------------------------------------");
            logOutput.WriteMessage("Finished obfuscation process at " + endTime);
            logOutput.WriteMessage("Duration: " + (endTime - startTime));


        }

		static void StartupSettings()
		{
			if (File.Exists("Opt.json"))
			{

			 current = Opts.FromJson(File.ReadAllText("Opt.json"));

			}
			else
			{
				var sett = new Opts()
				{
					SkipPrivateMethods = false,
					SkipPublicFields = false,
					SkipPublicMethods = false
				};
                File.WriteAllText("Opt.json",sett.ToJson());

				current = sett;

			}


		}
    }
}
