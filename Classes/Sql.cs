using System;
using System.Data;
using System.Data.SqlClient;

namespace Transaction.Classes
{
    public class Sql
    {
        private string PcName()
        {
            string namePc = Environment.MachineName.ToLower();
            string[] index = namePc.Split('-');
            string databaseName = "DB" + index[1];
            //string databaseName = "DB630000";

            return databaseName;
        }

        public void SearchPosting(string posting)
        {
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + PcName() + ";"
                                                         + "Integrated Security = SSPI");

            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from RETAILTRANSACTIONTABLE where RECEIPTID ='" + posting + "'",
                CommandType = CommandType.Text,
                Connection = connection
            };

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                //выводим данные
                while (reader.Read())
                {
                    object receiptid = reader.GetValue(3); //3
                    object grossamount = reader.GetValue(19); //19

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Найдена проводка {0} на сумму {1}", receiptid, grossamount);
                    Console.ResetColor();
                }
                reader.Close();
                //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                command.CommandTimeout = 240;
                command.ExecuteNonQuery();
                connection.Close();
                //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
            }
            catch (Exception exception)
            {
                //Logger.Log.Error(exception.Message);
            }
        }

        public void DeletePosting(string posting)
        {
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + PcName() + ";"
                                                         + "Integrated Security = SSPI");

            SqlCommand command = new SqlCommand
            {
                CommandText = "delete from RETAILTRANSACTIONTABLE where RECEIPTID ='" + posting + "'",
                CommandType = CommandType.Text,
                Connection = connection
            };

            try
            {
                connection.Open();

                //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                command.CommandTimeout = 240;
                command.ExecuteNonQuery();
                connection.Close();
                //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
            }
            catch (Exception exception)
            {
                //Logger.Log.Error(exception.Message);
            }
        }
    }
}
