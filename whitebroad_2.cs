namespace WriteBroad2
{
    using Npgsql;
    using System;
    using System.Data;
    using System.Linq;
    using System.Collections.Generic;
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }

        public Employee(int id, string name, double salary, string department)
        {
            Id = id;
            Name = name;
            Salary = salary;
            Department = department;
        }
    }
    public class ConnectPostgreSQL
    {
        public NpgsqlConnection ConnectDB()
        {
            string connInfo = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                            "localhost", 5432, "newuser", "password", "Demo");
            NpgsqlConnection conn = null;
            try
            {
                Console.WriteLine("Connecting to PostgreSQL...");
                conn = new NpgsqlConnection(connInfo);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            if (conn == null)
            {
                Console.WriteLine("Failed to connect to PostgreSQL");
            } else {
                Console.WriteLine("Connected to PostgreSQL");
            }
            return conn;
        }
    }

    public class DatabaseInit
    {
        public static void InitDB()
        {
            ConnectPostgreSQL conn = new ConnectPostgreSQL();
            NpgsqlConnection connDB = conn.ConnectDB();
            if (null != connDB)
            {
                Console.WriteLine("Connection established");
                string sql = "DROP TABLE IF EXISTS Employee";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connDB);
                cmd.ExecuteNonQuery();
                sql = @"CREATE TABLE Employee(
                        Id SERIAL PRIMARY KEY,
                        Name VARCHAR(50),
                        Salary REAL,
                        Department VARCHAR(50)
                        )";
                cmd = new NpgsqlCommand(sql, connDB);
                cmd.ExecuteNonQuery();
                sql = @"INSERT INTO Employee(Name, Salary, Department) VALUES
                        ('A', 1000, 'IT'),
                        ('B', 2000, 'IT'),
                        ('C', 3000, 'IT'),
                        ('D', 4000, 'IT'),
                        ('E', 5000, 'IT'),
                        ('F', 6000, 'IT'),
                        ('G', 7000, 'IT'),
                        ('H', 8000, 'IT'),
                        ('I', 9000, 'IT'),
                        ('J', 10000, 'IT')";
                cmd = new NpgsqlCommand(sql, connDB);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Connection failed");
            }
        }
    }

    public class DatabaseQueryWithLINQ
    {
        public static void GetEmployeesWithHighSalary()
        {
            ConnectPostgreSQL conn = new ConnectPostgreSQL();
            NpgsqlConnection connDB = conn.ConnectDB();
            if (null != connDB)
            {
                Console.WriteLine("Connection established");
                string sql = "SELECT * FROM Employee";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, connDB);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                List<Employee> employees = new List<Employee>();
                while (dr.Read())
                {
                    employees.Add(new Employee(dr.GetInt32(0), dr.GetString(1), dr.GetDouble(2), dr.GetString(3)));
                }
                var query = from employee in employees
                            where employee.Salary > 3000
                            orderby employee.Name ascending
                            select employee;
                Console.WriteLine("Employees with salary higher than 3000:");
                foreach (var employee in query)
                {
                    Console.WriteLine(employee.Name + " (" + employee.Department + ")");
                }
            }
            else
            {
                Console.WriteLine("Connection failed");
            }

        }
    }

}




