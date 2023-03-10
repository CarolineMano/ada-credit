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
            var subMenuGetClient = new ConsoleMenu(args, level: 2)
            .Add("Pelo número da conta", () => GetClientByAccountNumber.Show())
            .Add("Pelo CPF", () => GetClientByDocument.Show())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Consulta de Clientes";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subMenuClient = new ConsoleMenu(args, level: 1)
            .Add("Cadastrar novo cliente", () => AddNewClient.Show())
            .Add("Consultar dados de cliente", subMenuGetClient.Show)
            .Add("Alterar cadastro de cliente", () => UpdateClient.Show())
            .Add("Desativar cadastro de cliente", () => DeleteClient.Show())
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
            .Add("Alterar senha de funcionário", () => UpdateEmployeePassword.Show())
            .Add("Desativar cadastro de funcionário", () => DeleteEmployee.Show())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Funcionários";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subMenuTransaction = new ConsoleMenu(args, level: 1)
            .Add("Processar transações (Reconciliação Bancária)", () => ProcessTransactions.Show())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Transações";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var subMenuReports = new ConsoleMenu(args, level: 1)
            .Add("Listar clientes ativos", () => ListActiveClients.Show())
            .Add("Listar clientes inativos", () => ListInactiveClients.Show())
            .Add("Listar funcionários ativos", () => ListAllEmployees.Show())
            .Add("Listar transações com falha", () => ListFailedTransactions.Show())
            .Add("Voltar", ConsoleMenu.Close)
            .Configure(config =>
            {
                config.Selector = "--> ";
                config.EnableFilter = true;
                config.Title = "Relatórios";
                config.EnableBreadcrumb = true;
                config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
            });

            var menu = new ConsoleMenu(args, level: 0)
              .Add("Clientes", subMenuClient.Show)
              .Add("Funcionários", subMenuEmployee.Show)
              .Add("Transações", subMenuTransaction.Show)
              .Add("Relatórios", subMenuReports.Show)
              .Add("Voltar", () => Login.Show())
              .Add("Sair", () => Environment.Exit(0))
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