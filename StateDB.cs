using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMaintenance
{
    public static class StateDB
    {
        // returnType list of State
        public static List<State> GetStates()
        {
            // Create list of State class
            List<State> states = new List<State>();

            // Declare SqlConnection (con) that use (GetConnection()) method in static class [MMABooksDB] for connect to database server
            SqlConnection con = MMABooksDB.GetConnection();

            // Sql query [ SELECT statement to retreive data from State class 
            string selectStatement =
                "SELECT StateCode, StateName "
                + "FROM States "
                + "ORDER BY StateName";

            // Used for sent sql select statement to database server vai connection (con)
            // to retreive data 
            SqlCommand selectCommnand =
                new SqlCommand(selectStatement, con);

            try
            {
                // start Open connection
                con.Open();

                SqlDataReader reader =
                    selectCommnand.ExecuteReader();

                while (reader.Read())
                {
                    // Create object of State class
                    State state = new State();

                    // Get data from database via sqldatareader to store in property data of  State class
                    state.StateCode = reader["StateCode"].ToString();

                    state.StateName = reader["StateName"].ToString();

                    // Add data to Collection list of State
                    states.Add(state);
                }
                // Stop reading data
                reader.Close();
            }
            catch(SqlException e)
            {
                throw e;
            }
            finally
            {
                // Close the current connection 
                con.Close();
            }
            // return states object of collection list to Function returnType
            return states;
        }
    }
}
