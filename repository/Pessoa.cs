using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySqlConnector;
using Model;

namespace Repo
{
    public class RepoPessoa
    {
        public static List<Pessoa> pessoas = new List<Pessoa>();
        static private MySqlConnection conexao;

        public static void InitConexao()
        {
            string info = "server=localhost;database=projetointegrador;user id=root;password=''";
            conexao = new MySqlConnection(info);
            try
            {
                conexao.Open();
            }
            catch
            {
                MessageBox.Show("Impossível estabelecer conexão");
            }
        }

        public static void CloseConexao()
        {
            conexao.Close();
        }

        public static List<Pessoa> Sincronizar()
        {
            InitConexao();
            string query = "SELECT * FROM pessoas";
            MySqlCommand command = new MySqlCommand(query, conexao);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Aqui você pode acessar os dados retornados pela consulta SELECT
                int id = Convert.ToInt32(reader["id"].ToString());
                Pessoa pessoa = new Pessoa();
                pessoa.Id = id;
                pessoa.Idade = Convert.ToInt32(reader["idade"].ToString());
                pessoa.Nome = reader["nome"].ToString();
                pessoa.Cpf = reader["cpf"].ToString();
                pessoas.Add(pessoa);
            }
            CloseConexao();
            return pessoas;
        }

        public static void alterar(int index, string nome, int idade, string cpf)
        {
            Pessoa pessoaAtual = pessoas[index];

            // Se os parâmetros forem vazios ou inválidos, use os valores atuais
            if (string.IsNullOrEmpty(nome))
            {
                nome = pessoaAtual.Nome;
            }
            if (string.IsNullOrEmpty(cpf))
            {
                cpf = pessoaAtual.Cpf;
            }

            // Adicione lógica para alterar a idade, caso necessário

            // Atualize a pessoa na lista
            pessoaAtual.Nome = nome;
            pessoaAtual.Idade = idade;
            pessoaAtual.Cpf = cpf;

            // Aqui você também pode adicionar a lógica para atualizar o banco de dados, se necessário
        }
    }
}
