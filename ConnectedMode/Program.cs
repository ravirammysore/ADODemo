using System;
using System.Data.SqlClient;

namespace ConnectedDemo;
class Program
{
    static string server = "(localdb)";
    static string instance = "mssqllocaldb";
    static string database = "StudentDB";
    static string authentication = "Integrated Security = true";

    static string ConString = $"Data Source={server}\\{instance}; Initial Catalog={database};{authentication}";
    static void Main(string[] args)
    {
        WriteData();
        ReadData();
        Console.ReadKey();
    }

    static void ReadData()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                // Creating SqlCommand objcet   
                var cm = new SqlCommand("select * from student", connection);

                // Opening Connection  
                connection.Open();

                // Executing the SQL query  
                var sdr = cm.ExecuteReader();
                while (sdr.Read())
                    Console.WriteLine(sdr["Name"] + ",  " + sdr["Email"] + ",  " + sdr["Mobile"]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }
    static void WriteData()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                var cmd = new SqlCommand("insert into Student values (105, 'Ramesh', 'Ramesh@dotnettutorial.net', '1122334455')", connection);
                connection.Open();
                
                var rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine("Inserted Rows = " + rowsAffected);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }
}