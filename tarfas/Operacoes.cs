using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class Operacoes
{
    private string connectionString = 
        @"server=phpmyadmin.uni9.marize.us;User ID=user_poo;password=S3nh4!F0rt3;database=user_poo;";

    // Criar nova tarefa
    public int Criar(Tarefa tarefa)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string sql = @"INSERT INTO tarefa (nome, descricao, dataCriacao, status, dataExecucao) 
                           VALUES (@nome, @descricao, @dataCriacao, @status, @dataExecucao);
                           SELECT LAST_INSERT_ID();";
            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@nome", tarefa.Nome);
                cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                cmd.Parameters.AddWithValue("@dataCriacao", tarefa.DataCriacao);
                cmd.Parameters.AddWithValue("@status", tarefa.Status);
                cmd.Parameters.AddWithValue("@dataExecucao", tarefa.DataExecucao);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }

    // Buscar tarefa por ID
    public Tarefa Buscar(int id)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string sql = "SELECT id, nome, descricao, dataCriacao, dataExecucao, status FROM tarefa WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Tarefa
                        {
                            Id = reader.GetInt32("id"),
                            Nome = reader.GetString("nome"),
                            Descricao = reader.GetString("descricao"),
                            DataCriacao = reader.GetDateTime("dataCriacao"),
                            DataExecucao = reader.IsDBNull(reader.GetOrdinal("dataExecucao"))
                                           ? (DateTime?)null
                                           : reader.GetDateTime("dataExecucao"),
                            Status = reader.GetInt32("status")
                        };
                    }
                }
            }
        }

        return null;
    }

    // Listar todas as tarefas
    public IList<Tarefa> Listar()
    {
        var tarefas = new List<Tarefa>();
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string sql = "SELECT id, nome, descricao, dataCriacao, dataExecucao, status FROM tarefa";

            using (var cmd = new MySqlCommand(sql, conexao))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Descricao = reader.GetString("descricao"),
                        DataCriacao = reader.GetDateTime("dataCriacao"),
                        DataExecucao = reader.IsDBNull(reader.GetOrdinal("dataExecucao"))
                                       ? (DateTime?)null
                                       : reader.GetDateTime("dataExecucao"),
                        Status = reader.GetInt32("status")
                    });
                }
            }
        }

        return tarefas;
    }


    public bool Alterar(Tarefa tarefa)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string sql = @"UPDATE tarefa 
                           SET nome = @nome,
                               descricao = @descricao,
                               dataExecucao = @dataExecucao,
                               status = @status
                           WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@nome", tarefa.Nome);
                cmd.Parameters.AddWithValue("@descricao", tarefa.Descricao);
                cmd.Parameters.AddWithValue("@dataExecucao", tarefa.DataExecucao);
                cmd.Parameters.AddWithValue("@status", tarefa.Status);
                cmd.Parameters.AddWithValue("@id", tarefa.Id);

                return cmd.ExecuteNonQuery() > 0; 
            }
        }
    }

    // Excluir tarefa
    public bool Excluir(int id)
    {
        using (var conexao = new MySqlConnection(connectionString))
        {
            conexao.Open();
            string sql = @"DELETE FROM tarefa WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conexao))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0; 
            }
        }
    }
}
