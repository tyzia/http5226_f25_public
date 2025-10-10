using System;
using Microsoft.Data.Sqlite;

namespace DatabasePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=StudentDemo.db;";

            // Ensure database and table exist
            InitializeDatabase(connectionString);

            while (true)
            {
                Console.WriteLine("Select:");
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. Add New Customer");
                Console.WriteLine("3. Update Customer Email");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice (1-4): ");

                string choice = Console.ReadLine() ?? "0";

                switch (choice)
                {
                    case "1":
                        ViewAllCustomers(connectionString);
                        break;
                    case "2":
                        AddNewCustomer(connectionString);
                        break;
                    case "3":
                        UpdateCustomerEmail(connectionString);
                        break;
                    case "4":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void InitializeDatabase(string connectionString)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Customers (
                        CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT,
                        City TEXT
                    )";

                using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert some sample data if table is empty
                string checkDataSql = "SELECT COUNT(*) FROM Customers";
                using (SqliteCommand command = new SqliteCommand(checkDataSql, connection))
                {
                    var count = (long)command.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertSampleSql = @"
                            INSERT INTO Customers (Name, Email, City) VALUES
                            ('John Doe', 'john@email.com', 'New York'),
                            ('Jane Smith', 'jane@email.com', 'Chicago'),
                            ('Bob Wilson', 'bob@example.com', 'Miami')";

                        using (SqliteCommand insertCommand = new SqliteCommand(insertSampleSql, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                            Console.WriteLine("✅ Sample customer data inserted");
                        }
                    }
                }
            }
        }

        static void ViewAllCustomers(string connectionString)
        {
            Console.WriteLine("\n📋 Customer List:");
            Console.WriteLine("=================");

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectSql = "SELECT CustomerID, Name, Email, City FROM Customers";
                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No customers found in database.");
                        return;
                    }

                    while (reader.Read())
                    {
                       long id = (long)reader["CustomerID"];
                       string name = reader["Name"] as string ?? string.Empty;
                       string email = reader["Email"] as string ?? string.Empty;
                       string city = reader["City"] as string ?? string.Empty;

                        Console.WriteLine($"ID: {id} | Name: {name} | Email: {email} | City: {city}");
                    }
                }
            }
        }

        static void AddNewCustomer(string connectionString)
        {
            Console.WriteLine("\n➕ Add New Customer");
            Console.WriteLine("==================");

            Console.Write("Enter customer name: ");
            string name = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ Customer name cannot be empty!");
                return;
            }

            Console.Write("Enter customer email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter customer city: ");
            string city = Console.ReadLine() ?? string.Empty;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string insertSql = "INSERT INTO Customers (Name, Email, City) VALUES (@Name, @Email, @City)";
                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);
                    command.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(city) ? DBNull.Value : city);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"✅ Added new customer: {name}");
                }
            }
        }

        static void UpdateCustomerEmail(string connectionString)
        {
            Console.WriteLine("\n✏️  Update Customer Email");
            Console.WriteLine("=======================");

            ViewAllCustomers(connectionString);

            Console.Write("\nEnter customer ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("❌ Invalid customer ID!");
                return;
            }

            Console.Write("Enter new email: ");
            string newEmail = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(newEmail))
            {
                Console.WriteLine("❌ Email cannot be empty!");
                return;
            }

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string updateSql = "UPDATE Customers SET Email = @Email WHERE CustomerID = @CustomerID";
                using (SqliteCommand command = new SqliteCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@Email", newEmail);
                    command.Parameters.AddWithValue("@CustomerID", customerId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"✅ Updated email for customer ID: {customerId}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Customer ID {customerId} not found!");
                    }
                }
            }
        }

    }
}