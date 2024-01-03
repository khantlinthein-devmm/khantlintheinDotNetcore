using khantlintheinDotNetcore.ConsoleApp.AdoDotNetExamples;
using khantlintheinDotNetcore.ConsoleApp.DapperExamples;
using khantlintheinDotNetcore.ConsoleApp.EFCoreExamples;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("--- Myanmar ---");


// Ctrl + .
// Ctrl + D
// Alt + Up Down Key
// F10 - summary ( debug )
// F11 - details ( debug )
// Ctrl + M,H ( region )
// Ctrl + K, D  ( reformat code )

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
//{
//    DataSource = ".", // server name
//    InitialCatalog = "DotNetcore", // databse name
//    UserID = "sa", // username
//    Password = "sa@2655" // password
//};

//SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

//Console.WriteLine("Connection is opening...");
//connection.Open();
//Console.WriteLine("Connection is opened.");


//string query = @"SELECT [Blog_ID]
//      ,[Blog_Title]
//      ,[Blog_Name]
//      ,[Blog_Content]
//      ,[Blog_Category]
//  FROM [dbo].[Tb_Blog]
//";

//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable datatable = new DataTable();
//sqlDataAdapter.Fill(datatable);


//Console.WriteLine("Connection is closing...");
//connection.Close();
//Console.WriteLine("Connection is closed.");

// dataset 
// data table 
// data rows
// data columns 


//foreach( DataRow dr in datatable.Rows)
//{ 
//    Console.WriteLine($"ID => {dr["Blog_ID"]} " );
//    Console.WriteLine($"Title => {dr["Blog_Title"]} ");
//    Console.WriteLine($"Name => {dr["Blog_Name"]} ");
//    Console.WriteLine($"Content => {dr["Blog_Content"]} ");
//    Console.WriteLine($"Category => {dr["Blog_Category"]} ");
//    Console.WriteLine("----------------------");
//}


//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Run();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

EFCoreExample efCoreExample = new EFCoreExample();
efCoreExample.Run();

Console.ReadKey();