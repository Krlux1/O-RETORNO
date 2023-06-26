using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Drawing;
using UsandoBanco.BancoDados;

namespace UsandoBanco
{
    internal class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public Produto(long id)
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, NOME, CODIGO, VALOR, DESCRICAO FROM PRODUTOS WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Id = reader.GetInt32(0);
                        Nome = reader.GetString(1);
                        Codigo = reader.GetString(2);
                        Valor = reader.GetDecimal(3);
                        Descricao = reader.GetString(4);
                                            }
                }
            }
        }
            public Produto(int id, string nome, string codigo ,decimal valor, string descricao)
        {
            Id = id;
            Nome = nome;    
            Codigo = codigo;
            Valor = valor;
            Descricao = descricao;

        }
        public Produto()
        {
            Console.WriteLine($"Informe o nome do produto:");
            Nome = Console.ReadLine();


            Console.WriteLine($"Informe o código do produto:");
            Codigo = Console.ReadLine();


            Console.WriteLine($"Informe o valor do produto:");
           decimal valor = 0;
            while (!decimal.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("Valor invalido");
                Thread.Sleep(2000);

                Console.WriteLine($"Informe o valor do produto:");
            }
            Valor = valor;


            Console.WriteLine($"Informe a descrição do produto:");
            Descricao = Console.ReadLine();


        }

        public bool isValid()
        {
            return Id > 0;
        }

        public void Save()
        {
            using (var conn = new SqlConnection(DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO PRODUTOS(NOME, CODIGO, VALOR, DESCRICAO) VALUES( @NOME, @CODIGO, @VALOR, @DESCRICAO)";
                cmd.Parameters.AddWithValue("@NOME", Nome);
                cmd.Parameters.AddWithValue("@CODIGO", Codigo);
                cmd.Parameters.AddWithValue("@VALOR", Valor);
                cmd.Parameters.AddWithValue("@DESCRICAO", Descricao);

                cmd.ExecuteNonQuery();
            }
        }
        public void Update()
        {
            {
                using (var conn = new SqlConnection(DBinfo.DBConnection))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();

                    cmd.CommandText = "UPDATE PRODUTOS SET NOME = @NOME, CODIGO = @CODIGO, VALOR = @VALOR, DESCRICAO = @DESCRICAO WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@NOME", Nome);
                    cmd.Parameters.AddWithValue("@CODIGO", Codigo);
                    cmd.Parameters.AddWithValue("@VALOR", Valor);
                    cmd.Parameters.AddWithValue("@DESCRICAO", Descricao);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete()
        {
            using (var conn = new SqlConnection( DBinfo.DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText= "DELETE FROM PRODUTOS WHERE ID = @ID";
                cmd.Parameters.AddWithValue("@ID", Id);

                cmd.ExecuteNonQuery ();
            }
        }

        public static List<Produto> GetAll()
        {
            var result = new List<Produto>();
            using (var conn = new SqlConnection(DBinfo.DBConnection)) 
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, NOME, CODIGO, VALOR, DESCRICAO FROM PRODUTOS";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var produto = new Produto(reader.GetInt32(0),reader.GetString(1),
                            reader.GetString(2),reader.GetDecimal(3),reader.GetString(4));
                        result.Add(produto);
                    }
                }
                
                   
                
            }
            return result;
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Nome:{Nome}");
            sb.AppendLine($"Codigo:{Codigo}");
            sb.AppendLine($"Valor:{Valor}");
            sb.AppendLine($"Descrição? {Descricao}");
            
            
            return sb.ToString();
        }
    }
           
}
