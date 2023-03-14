using System;
using System.Data;
using System.Data.SqlClient;

namespace DisconnectedDemo;
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
                //Create the SqlDataAdapter instance by specifying the command text and connection object
                var dataAdapter = new SqlDataAdapter("select * from student", connection);
                
                //Creating DataSet Object
                var dataSet = new DataSet();
                
                //Filling the DataSet using the Fill Method of SqlDataAdapter object
                //Here, we have not specified the data table name and the data table will be created at index position 0
                dataAdapter.Fill(dataSet);
                
                //Iterating through the DataSet 
                //First fetch the Datatable from the dataset and then fetch the rows using the Rows property of Datatable
                foreach (DataRow row in dataSet.Tables[0].Rows)
                    //Accessing the Data using the string column name as key
                    Console.WriteLine(row["Id"] + ",  " + row["Name"] + ",  " + row["Email"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Occurred: {ex.Message}");
        }
    }
    static void WriteData()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                //Create the SqlDataAdapter instance by specifying the command text and connection object
                var dataAdapter = new SqlDataAdapter("SELECT * FROM Student", connection);

                // At this point SqlCommandBuilder should generate T-SQL statements automatically
                var commandBuilder = new SqlCommandBuilder(dataAdapter);

                //Creating DataSet Object
                var dataSet = new DataSet();
                //Filling the DataSet using the Fill Method of SqlDataAdapter object
                dataAdapter.Fill(dataSet);

                //Now Update First Row i.e. Index Position 0
                var dataRow = dataSet.Tables[0].Rows[0];
                dataRow["Name"] = "Name Updated";

                //Provide the DataSet and the DataTable name to the Update method
                //Here, SqlCommandBuilder will automatically generate the UPDATE SQL Statement 
                var rowsUpdated = dataAdapter.Update(dataSet, dataSet.Tables[0].TableName);

                if (rowsUpdated == 0)
                    Console.WriteLine("\nNo Rows Updated");
                else
                    Console.WriteLine($"\n{rowsUpdated} Row(s) Updated");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }
}