namespace MagicVilla_VillaAPI.Log
{
    public class LoggingV2:ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "Error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error - " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                if (type == "warning")
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Warning - " + message);
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                else
                {
                    Console.WriteLine(message);
                }
               
            }
        }
    }
}

