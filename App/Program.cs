using System;
using System.Linq;
using System.Threading.Tasks;

namespace BingDesktop
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                return MainInernal(args).GetAwaiter().GetResult();
            }
            catch (Exception ex) 
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }

        private static async Task<int> MainInernal(string[] args)
        {
            var command = BingDesktopApp.Command.Latest;
            if (args.Length > 0)
            {
                if (!Enum.TryParse(args[0], true, out command))
                {
                    Console.Error.WriteLine($"Unknown command '{args[0]}'");
                    return 1;
                }
            }

            await new BingDesktopApp().Run(command, args.Skip(1).ToArray());
            return 0;
        }
    }
}
