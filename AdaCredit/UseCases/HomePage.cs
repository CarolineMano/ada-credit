using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class HomePage
    {
        public static void Show()
        {
            Console.WriteLine(@"             _          _____              _ _ _");   
            Console.WriteLine(@"    /\      | |        / ____|            | (_) | "); 
            Console.WriteLine(@"   /  \   __| | __ _  | |     _ __ ___  __| |_| |_ ");
            Console.WriteLine(@"  / /\ \ / _` |/ _` | | |    | '__/ _ \/ _` | | __|");
            Console.WriteLine(@" / ____ \ (_| | (_| | | |____| | |  __/ (_| | | |_ ");
            Console.WriteLine(@"/_/    \_\__,_|\__,_|  \_____|_|  \___|\__,_|_|\__|");

            Console.ReadKey();
        }
    }
}