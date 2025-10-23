using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var operacoes = new Operacoes();
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("\n=== MENU DE TAREFAS ===");
            Console.WriteLine("1 - Criar nova tarefa");
            Console.WriteLine("2 - Listar todas as tarefas");
            Console.WriteLine("3 - Alterar tarefa");
            Console.WriteLine("4 - Excluir tarefa");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CriarTarefa(operacoes);
                    break;
                case "2":
                    ListarTarefas(operacoes);
                    break;
                case "3":
                    AlterarTarefa(operacoes);
                    break;
                case "4":
                    ExcluirTarefa(operacoes);
                    break;
                case "0":
                    sair = true;
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static void CriarTarefa(Operacoes operacoes)
    {
        var tarefa = new Tarefa();
        Console.Write("Nome: ");
        tarefa.Nome = Console.ReadLine();
        Console.Write("Descrição: ");
        tarefa.Descricao = Console.ReadLine();
        tarefa.DataCriacao = DateTime.Now;
        tarefa.Status = 1;
        tarefa.DataExecucao = null;

        int id = operacoes.Criar(tarefa);
        Console.WriteLine($"Tarefa criada com sucesso! ID: {id}");
    }

    static void ListarTarefas(Operacoes operacoes)
    {
        var tarefas = operacoes.Listar();
        Console.WriteLine("\n=== LISTA DE TAREFAS ===");
        foreach (var t in tarefas)
        {
            Console.WriteLine($"ID: {t.Id} | Nome: {t.Nome} | Status: {t.Status} | Data Criação: {t.DataCriacao} | Data Execução: {t.DataExecucao}");
        }
    }


    static void AlterarTarefa(Operacoes operacoes)
    {
        Console.Write("Digite o ID da tarefa que deseja alterar: ");
        int id = int.Parse(Console.ReadLine());
        var t = operacoes.Buscar(id);

        if (t != null)
        {
            Console.Write("Novo nome: ");
            t.Nome = Console.ReadLine();
            Console.Write("Nova descrição: ");
            t.Descricao = Console.ReadLine();
            Console.Write("Novo status (número): ");
            t.Status = int.Parse(Console.ReadLine());
            Console.Write("Nova data de execução (yyyy-mm-dd) ou vazio: ");
            string dataExec = Console.ReadLine();
            t.DataExecucao = string.IsNullOrEmpty(dataExec) ? null : DateTime.Parse(dataExec);

            operacoes.Alterar(t);
            Console.WriteLine("Tarefa alterada com sucesso!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    static void ExcluirTarefa(Operacoes operacoes)
    {
        Console.Write("Digite o ID da tarefa que deseja excluir: ");
        int id = int.Parse(Console.ReadLine());

        if (operacoes.Excluir(id))
        {
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada ou erro ao excluir.");
        }
    }
}
