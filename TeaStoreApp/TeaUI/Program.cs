using TeaUI.Menus;
using Serilog;

namespace TeaUI
{
    class Program
    {
        /// <summary>
        /// P0 - Tea Store
        /// Shalei Kumar
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("loggingfile.txt")
            .CreateLogger();

            MainMenu start = new MainMenu();
            start.Start();
        }    
    }
}
