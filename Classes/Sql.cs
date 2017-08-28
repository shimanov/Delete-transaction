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
            //string databaseName = "DB632732";

            return databaseName;
        }

        private string Zip()
        {
            string namePc = Environment.MachineName.ToLower();
            string[] mindex = namePc.Split('-');
            string dbanme = mindex[1];

            return dbanme;
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
                    object receiptid = reader.GetValue(2);
                    object grossamount = reader.GetValue(18);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Найдена проводка {0} \t на сумму {1}", receiptid, grossamount);
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

        public string SearchNoPosting(string number)
        {
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + PcName() + ";"
                                                         + "Integrated Security = SSPI");

            //Console.WriteLine("Введите номер окна на котором зависла транзакция (например 01):");
            //Console.ForegroundColor = ConsoleColor.Green;
            //string num = Console.ReadLine();
            //Console.ResetColor();

            SqlCommand command = new SqlCommand
            {
                CommandText = "use" + PcName() + "DECLARE @dataareaid NVARCHAR(4) = 'rp', @storeid NVARCHAR(6) = '" + Zip() + "', @terminalid NVARCHAR(10) = '" + Zip() + "." + number + "'SELECT b.receiptid,a.* FROM GM_Transaction a JOIN RETAILTRANSACTIONsalestrans b ON a.NotSavedRetailTransactionId = b.TRANSACTIONID AND a.TERMINALID = b.TERMINALID AND a.DataAreaId = b.DATAAREAID AND a.STOREID = b.STORE WHERE a.RetailTransactionId IS NULL AND a.TERMINALID = @terminalid AND a.DataAreaId = @dataareaid AND a.STOREID = @storeid",
                CommandType = CommandType.Text,
                Connection = connection
            };

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string receiptid = reader.GetString(0);
                    string payment = reader.GetString(12);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Найдена проводка {0} на сумму {1}", receiptid, payment);
                    Console.ResetColor();
                }
                reader.Close();
                command.CommandTimeout = 240;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
