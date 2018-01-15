using JosephM.Xrm.SharepointSample.Plugins.Core;
using JosephM.Xrm.SharepointSample.Plugins.Xrm;
using System;

namespace JosephM.Xrm.SharepointSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = new ConsoleSettings(new[]
                {
                    new ConsoleSettings.SettingFile(typeof(XrmSetting)),
                });
                if (!settings.ConsoleArgs(args))
                {
                    var xrmSetting = settings.Resolve<XrmSetting>();
                    var controller = new LogController(new ConsoleUserInterface(false));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.XrmDisplayString());
            }
            Console.WriteLine("Press Any Key To Close");
            Console.ReadKey();
        }
    }
}
