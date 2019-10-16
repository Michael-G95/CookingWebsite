using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Data.Logic;

namespace Data.DataAccess
{
    internal static class SQLDataAccess
    {
        // Class to provide an access layer to run database queries
        // This class is internal, the UI should not interact directly with this logic but use the Data.Logic namespace to interface with it

        public static string GetConnectionString(string connName ="Default")
        { // Return the database connection string
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security=True";
            //return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\MVCApp\MVCApp\MVCApp\App_Data\Database.mdf;Integrated Security=True";
        }


        public static List<T> DirectDataQuery<T>(string sql,Dictionary<string,object> SqlParams)
            where T: new() // T must be instantiable as its a data class
        {
            List<T> mod = new List<T>();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < SqlParams.Count; i++) // Add the parameters to query
                    {
                        cmd.Parameters.AddWithValue(
                            SqlParams.ElementAt(i).Key,
                            SqlParams.ElementAt(i).Value
                            );
                    }
                    var rdr = cmd.ExecuteReader();
                    mod = ModelMapper.MapSqlToModel<T>(rdr);
                    rdr.Close();
                    con.Close();
                }catch(Exception e)
                {
                    // Problem in connecting to the database
                    // In this case we will return null rather than an empty list
                    return null;
                }
            }
            return mod;
        }
        public static List<T> DirectDataQuery<T>(string sql)
            where T : new() // T must be instantiable as its a data class
        {
            List<T> mod = new List<T>();
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rdr = cmd.ExecuteReader();
                    mod = ModelMapper.MapSqlToModel<T>(rdr);
                    rdr.Close();
                    con.Close();
                }
                catch (Exception e)
                {
                    // Problem in connecting to the database
                    // In this case we will return null rather than an empty list
                    return null;
                }
            }
            return mod;
        }
        public static int DirectIntResultDataQuery(string sql, Dictionary<string,object> SqlParams)
        {
            int result=0;
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < SqlParams.Count; i++) // Add the parameters to query
                    {
                        cmd.Parameters.AddWithValue(
                            SqlParams.ElementAt(i).Key,
                            SqlParams.ElementAt(i).Value
                            );
                    }
                    var rdr = cmd.ExecuteReader();
                    rdr.Read();
                    result = (int)rdr[0];
                    rdr.Close();
                    con.Close();
                }
                catch(Exception e)
                {
                    return 0; // Problem in connecting to SQL - return 0
                }
            }
            return result;
        }

        public static int DirectDataUpdate(string sql,Dictionary<string,object> SqlParams)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString())) // Create the connection
            {
                try
                {
                    con.Open(); // Open connection
                    SqlCommand cmd = new SqlCommand(sql, con); // Create command to be executed
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < SqlParams.Count; i++) // Add the parameters to query
                    {
                        cmd.Parameters.AddWithValue(
                            SqlParams.ElementAt(i).Key,
                            SqlParams.ElementAt(i).Value
                            );
                    }
                    var rdr = cmd.ExecuteReader(); // Execute query
                    rdr.Close();
                    con.Close(); // Close connection 
                    return rdr.RecordsAffected; // Return records affected
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        
    }
}
