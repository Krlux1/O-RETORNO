using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UsandoBanco.BancoDados
{
    internal class DBinfo
    {
        //Utilizamos o @ para evitar o barra invertida. Por ex: \n
        public const string DBConnection = @"Data Source=BUE0102D015\SQLEXPRESS;Initial Catalog=IntegraDB;User ID=sa; Password=Senac@2023";
        public static bool TestDBConection()
        {
            var result = false;

            using (var conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(ID) FROM PRODUTOS";

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"O comando foi executado e retornou {reader.GetString(0)} registros em produtos!");
                }

                //conn.Close();
            }

            return result;
        }
    }
}
