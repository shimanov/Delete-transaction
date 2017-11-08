using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeleteTransactionGUI.Classes
{
    public class DeleteTransaction
    {
        //Автосвойства
        //Адрес подключения к серверу
        //public string Connection;

        ////Номер зависшей транзакции
        //public string Posting ;

        //Конструктор по умолчанию
        public DeleteTransaction()
        {

        }

        public string PcName()
        {
            string namePc = Environment.MachineName.ToLower();
            string[] index = namePc.Split('-');
            string databaseName = "DB" + index[1];
            //string databaseName = "DB630000";

            return databaseName;
        }


        //Пользовательский конструктор
        public string DeleteTransaction(string connection, string posting)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);

            using (SqlCommand command = new SqlCommand(@"select *from RETAILTRANSACTIONTABLE where RECEIPTID = '" + posting +"'"))
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object receiptid = reader.GetValue(3);
                            object grossamount = reader.GetValue(19);
                        }
                    }
                    command.CommandTimeout = 240;
                    command.ExecuteNonQuery();

                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show($"Транзакция с номером {posting} не найдена");
                }
            }
        }
    }
}
