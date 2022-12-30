using AdaCredit.UseCases;

namespace AdaCredit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Login.Show();
            Menu.Show(args);
        }
    }
}