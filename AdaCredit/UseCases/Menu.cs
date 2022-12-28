using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTools;

namespace AdaCredit.UseCases
{
    public static class Menu
    {
        public static void Show(string[] args)
        {
            var subMenuClient = new ConsoleMenu(args, level: 1)
            .Add("Cadastrar novo cliente", () => SomeAction("Sub_One"))
            .Add("Consultar dados de cliente", () => SomeAction("Sub_Two"))
            .Add("Alterar cadastro de cliente", () => SomeAction("Sub_Three"))
            .Add("Desativar cadastro de cliente", () => SomeAction("Sub_Four"))
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Clientes";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subMenuEmployee = new ConsoleMenu(args, level: 1)
            .Add("Cadastrar novo funcionário", () => AddNewEmployee.Show())
            .Add("Alterar senha de funcionário", () => SomeAction("Sub_Two"))
            .Add("Desativar cadastro de funcionário", () => SomeAction("Sub_Four"))
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Funcionários";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var menu = new ConsoleMenu(args, level: 0)
              .Add("Clientes", subMenuClient.Show)
              .Add("Funcionários", subMenuEmployee.Show)
              .Add("Change me", (thisMenu) => thisMenu.CurrentItem.Name = "I am changed!")
              .Add("Close", ConsoleMenu.Close)
              .Add("Action then Close", (thisMenu) => { SomeAction("Close"); thisMenu.CloseMenu(); })
              .Add("Exit", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Ada Credit";
                  config.EnableWriteTitle = false;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();
        }

        private static void SomeAction(string v)
        {
            Console.WriteLine(v);
        }
    }
}