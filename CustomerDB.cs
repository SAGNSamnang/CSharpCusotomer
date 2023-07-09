using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMaintenance
{
    public static class CustomerDB
    {
        // Method GetCustomer() to get data of Customers table from database server to store in dataset
        public static Customer GetCustomer (int customerID)
        {
            // Declare SqlConnection (con) that use (GetConnection()) method in static class [MMABooksDB] for connect to database server
            SqlConnection con = MMABooksDB.GetConnection();

            // Using Sql query to get data from Customer table in database server
            string selectStatement =
                "SELECT CustomerID, Name, Address, City,State, ZipCode "
                + "FROM Customers "
                + "WHERE CustomerID = @CustomerID";

            // Used for sent sql statement to database server vai connection (con)
            SqlCommand selectCommnand = new SqlCommand(selectStatement, con);

            // Use command object to store data in dataset via customerID
            selectCommnand.Parameters.AddWithValue(
                "@CustomerID", customerID);

            // Use try-catch to handle error
            try
            {
                // Open the connection to work with data
                con.Open();

                //-> ??
                SqlDataReader customerReader =
                    selectCommnand.ExecuteReader(CommandBehavior.SingleRow);

                // Read data one by one from SqlDataReader to load in class [Customer]
                if (customerReader.Read())
                {
                    // Create object customer for working with data in class [Customer]
                    Customer customer = new Customer();

                    // Use Object customer vai CustomerID property to hole data from SqlDataReader object
                    // and convert to its Int32
                    customer.CustomerID = (int) customerReader["CustomerID"];

                    customer.Name = customerReader["Name"].ToString();

                    customer.Address = customerReader["Address"].ToString();

                    customer.City = customerReader["City"].ToString();

                    customer.State = customerReader["State"].ToString();

                    customer.ZipCode = customerReader["ZipCode"].ToString();

                    // return Customer object
                    return customer;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        // Method AddCustomer() used for insert data into Customers table in database server
        public static int AddCustomer (Customer customer)
        {
            // Declare SqlConnection (con) that use (GetConnection()) method in static class [MMABooksDB] for connect to database server
            SqlConnection con = MMABooksDB.GetConnection();

            // Sql insert statement
            string insertStatement =
                "INSERT INTO Customers (Name, Address, City, State, ZipCode)" +
                "VALUES (@name, @address, @city, @state, @zipcode)";

            SqlCommand insertCommand =
                new SqlCommand(insertStatement, con);

            insertCommand.Parameters.AddWithValue(
                "@name", customer.Name);

            insertCommand.Parameters.AddWithValue(
                "@address", customer.Address);

            insertCommand.Parameters.AddWithValue(
                "@city", customer.City);

            insertCommand.Parameters.AddWithValue(
                "@state", customer.State);

            insertCommand.Parameters.AddWithValue(
                "@zipcode", customer.ZipCode);

            try
            {
                con.Open();

                insertCommand.ExecuteNonQuery();

                // declare select statement for -
                string selectStatement =
                    "SELECT IDENT_CURRENT ('Customers') FROM Customers";

                // Used for sent sql select statement to database server vai connection (con)
                // to retrieve data 
                SqlCommand selectCommand =
                    new SqlCommand(selectStatement, con);

                // - ExecuteScalar() using for retrieve a single value from Database after the execution of the SQL Statement.
                // holding in variable ( customerID )
                int customerID = Convert.ToInt32(selectCommand.ExecuteScalar());

                // return value of customerID ( int )
                return customerID;
            }
            catch(SqlException e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
        }

        // Update Customer info in Customers table via user input from FORM
        // returntype boolean [ true, false ] - object of Customer class as parameter
        public static bool UpdateCustomer (Customer oldCustomer, Customer newCustomer)
        {
            // Declare SqlConnection (con) that use (GetConnection()) method in static class [MMABooksDB] for connect to database server
            SqlConnection con = MMABooksDB.GetConnection();

            //Sql update statement
            string updateStatement =
                "UPDATE Customers SET " +
                "Name = @NewName, " +
                "Address = @NewAddress, " +
                "City = @NewCity, " +
                "State = @NewState, " +
                "ZipCode = @NewZipCode " +
                "WHERE Name = @OldName " +
                "AND Address = @OldAddress " +
                "AND City = @OldCity " +
                "AND State = @OldState " +
                "AND ZipCode = @OldZipCode";

            // Used for sent sql update statement to database server vai connection (con)
            // to update data 
            SqlCommand updateCommand =
                new SqlCommand(updateStatement, con);

            updateCommand.Parameters.AddWithValue(
                "@NewName", newCustomer.Name);

            updateCommand.Parameters.AddWithValue(
                "@NewAddress", newCustomer.Address);

            updateCommand.Parameters.AddWithValue(
                "@NewCity", newCustomer.City);

            updateCommand.Parameters.AddWithValue(
                "@NewState", newCustomer.State);

            updateCommand.Parameters.AddWithValue(
                "@NewZipCode", newCustomer.ZipCode);

            /////////////////////////////////////////////////

            updateCommand.Parameters.AddWithValue(
                "@OldName", oldCustomer.Name);

            updateCommand.Parameters.AddWithValue(
                "@OldAddress", oldCustomer.Address);

            updateCommand.Parameters.AddWithValue(
                "@OldCity", oldCustomer.City);

            updateCommand.Parameters.AddWithValue(
                "@OldState", oldCustomer.State);

            updateCommand.Parameters.AddWithValue(
                "@OldZipCode", oldCustomer.ZipCode);

            try
            {
                con.Open();
                
                //count data from updateCommand
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }


        // Delete Customer info in Customers table via user input from FORM
        // returntype boolean [ true, false ] - object of Customer class as parameter
        public static bool DeleteCustomer (Customer customer)
        {
            // Declare SqlConnection (con) that use (GetConnection()) method in static class [MMABooksDB] for connect to database server
            SqlConnection con = MMABooksDB.GetConnection();

            // Sql delete statement 
            string deleteStatement =
                "DELETE FROM Customers " +
                "WHERE Name = @Name " +
                "AND Address = @Address " +
                "AND City = @City " +
                "AND State = @State " +
                "AND ZipCode = @ZipCode ";

            // Used for sent sql delete statement to database server vai connection (con)
            // to delete data 
            SqlCommand deleteCommand =
                new SqlCommand(deleteStatement, con);

            deleteCommand.Parameters.AddWithValue(
                "@Name", customer.Name);

            deleteCommand.Parameters.AddWithValue(
                "@Address", customer.Address);

            deleteCommand.Parameters.AddWithValue(
                "@City", customer.City);

            deleteCommand.Parameters.AddWithValue(
                "@State", customer.State);

            deleteCommand.Parameters.AddWithValue(
                "@ZipCode", customer.ZipCode);

            try
            {
                con.Open();
                //count data from updateCommand
                int count = deleteCommand.ExecuteNonQuery();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
