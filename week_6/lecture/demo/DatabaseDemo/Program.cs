using System;
using Microsoft.Data.Sqlite;

namespace DatabaseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will create a file called "StudentDemo.db" in your project folder
            string connectionString = "Data Source=StudentDemo.db;";
            
            CreateDatabaseAndTables(connectionString);
            DemoCRUDOperations(connectionString);
        }
        
        static void CreateDatabaseAndTables(string connectionString)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                // Create Customers table
                string createCustomersTable = @"
                    CREATE TABLE IF NOT EXISTS Customers (
                        CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT,
                        City TEXT
                    );";
                
                using (SqliteCommand command = new SqliteCommand(createCustomersTable, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("✅ Customers table created/verified");
                }

                string insertData = @"
                    INSERT INTO Customers (Name, Email, City) VALUES
                    ('John Doe', 'john@email.com', 'New York'),
                    ('Jane Smith', 'jane@email.com', 'Los Angeles'),
                    ('Bob Johnson', 'bob@email.com', 'Chicago'),
                    ('Alice Brown', NULL, 'Houston');";

                using (SqliteCommand insertCmd = new SqliteCommand(insertData, connection))
                {
                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("✅ Sample data inserted");
                }

            }
        }
        
        static void DemoCRUDOperations(string connectionString)
        {
            Console.WriteLine("\n📊 Reading data from database:");
            
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                // READ - Show all customers
                string selectSql = "SELECT CustomerID, Name, Email, City FROM Customers";
                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["CustomerID"]}, Name: {reader["Name"]}, Email: {reader["Email"] ?? "N/A"}, City: {reader["City"]}");
                    }
                }
                
                // CREATE - Add a new customer
                Console.WriteLine("\n➕ Adding new customer...");
                string insertSql = "INSERT INTO Customers (Name, Email, City) VALUES (@Name, @Email, @City)";
                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Name", "Charlie Wilson");
                    command.Parameters.AddWithValue("@Email", "charlie@email.com");
                    command.Parameters.AddWithValue("@City", "Miami");
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"✅ Added {rowsAffected} new customer");
                }
            }
        }
    }
}