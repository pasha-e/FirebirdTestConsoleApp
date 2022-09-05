using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace FirebirdTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FbConnection connection =
                new FbConnection(@"Server=localhost;User=SYSDBA;Password=master;Database=C:\firebird\testcreate.fdb"))
            {
                connection.Open();

                if (CreateTables(connection, @"c:\firebird\createtable.sql"))
                {
                    Console.WriteLine("Tables for samples creates succesfully.");
                }
                else
                {
                    Console.WriteLine("Tables for samples creates previously.");
                }

                DataTable dt = new DataTable();

                //создаём адаптер к соединению с БД, выполняющий sql запрос
                FbDataAdapter da = new FbDataAdapter("select * from employer", connection);
                //с помощью этого адаптера заполняем табличку результатом запроса
                da.Fill(dt);

                //вывод результата
                Console.WriteLine("Table Employer\n");
                foreach (var row in dt.Rows)
                {
                    Console.WriteLine(CreateRowString((DataRow)row));
                }

                dt = new DataTable();
                da = new FbDataAdapter("select * from post", connection);
                da.Fill(dt);

                Console.WriteLine("\nTable Post");
                foreach (var row in dt.Rows)
                {
                    Console.WriteLine(CreateRowString((DataRow)row));
                }

                //выведем связанные данные Employer - Post
                dt = new DataTable();
                da = new FbDataAdapter("select * from employer", connection);
                da.Fill(dt);

                Console.WriteLine("\n Employer - Post");
                foreach (var row in dt.Rows)
                {
                    var emplId = ((DataRow)row)[0];

                    //получим id должности через id сотрудника
                    DataTable tableTemp = new DataTable();
                    FbDataAdapter daTemp = new FbDataAdapter("select id_post from POST_OF_EMPLOYERS where id_employer = @id_employer", connection);
                    daTemp.SelectCommand.Parameters.Add("id_employer", (int)emplId);
                    daTemp.Fill(tableTemp);

                    var postId = tableTemp.Rows[0][0];

                    //получим наименование должности через id
                    tableTemp = new DataTable();
                    daTemp = new FbDataAdapter("select title from POST where id = @id", connection);
                    daTemp.SelectCommand.Parameters.Add("id", (int)postId);
                    daTemp.Fill(tableTemp);

                    var postStr = tableTemp.Rows[0][0];

                    Console.WriteLine(String.Format("{0} {1}",CreateRowString((DataRow)row), postStr));
                }
            }
        }


        private static bool CreateTables(FbConnection connection, string path)
        {
            bool result = false;

            try
            {
                string sqlText = String.Empty;
                using (StreamReader reader = new StreamReader(path))
                {
                    sqlText = reader.ReadToEnd();
                }

                sqlText = sqlText.Replace("\r\n", "");

                var sqlTextList = sqlText.Split(";", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < sqlTextList.Length; i++)
                {
                    sqlTextList[i] += ";";

                }


                foreach (var sqlCmd in sqlTextList)
                {
                    FbCommand cmd = new FbCommand(sqlCmd);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
            }

            return result;

        }

        static string CreateRowString(DataRow row)
        {
            string result = string.Empty;

            foreach (var item in row.ItemArray)
            {
                result += $"{item}  ";
            }

            return result;
        }
    }
}
