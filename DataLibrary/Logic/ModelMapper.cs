using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

namespace Data.Logic
{
    internal static class ModelMapper
    {
        public static T MapExact<T>(object FromModel)
            where T : new()   // Requires that we are able to instantiate a type T, we need to do this to be able to return one
            // Type parameter T is destination type, FromModel is the source object
        {
            // Class to map a UI model to a Data model which is ready to interact with database
            // This is accomplished by matching the names of the destination object's properties to ones in the source object

            object DataModel = new T();// Create object of destination type
            PropertyInfo[] fromProps = FromModel.GetType().GetProperties(); // Array of properties in the source model

            foreach (var toProp in DataModel.GetType().GetProperties())
            {
                // Foreach property in the destination object

                PropertyInfo prop = null; // Property that will try to match to one in source object

                foreach (PropertyInfo p in fromProps)
                {
                    // For each destination property, this loops over each source property to check for matches
                    if (p.Name == toProp.Name)
                    {
                        // If the name matches, save the current propertyinfo to prop and break out of foreach (we have found a match, no more checking needed)
                        prop = p;
                        break;
                    }
                }
                if (prop == null)
                {
                    // No match found for the destination property, in the source property
                    // Set value of destination property to null
                    // Set value sets the object (1st parameter)'s property toProp (must be from dest obj) to Value (2nd parameter), 3rd is null (used for passing in array)
                    toProp.SetValue(obj: DataModel, value: null, index: null);
                }
                else
                {
                    object value = prop.GetValue(FromModel); // Get the value of the source property which was found to match the destination property

                    // Set value of destination property to the value of the source property (NOT copying whole property)
                    // Set value sets the object (1st parameter)'s property toProp (must be from dest obj) to Value (2nd parameter), 3rd is null (used for passing in array)
                    toProp.SetValue(obj: DataModel, value: value, index: null);
                }
            }
            return (T)DataModel;
        }
        public static List<T> MapSqlToModel<T>(SqlDataReader result)
            where T : new() // Requires that we are able to instantiate a type T, this needs to do this to be able to return one
        {
            // Method to take a SQLReader and map it to a class of type T

            T obj = new T();// Create an instance of T which this will map the SQL into
            List<T> list = new List<T>(); // Create a list of T- this will be returned representing the rows of data
            
            List<string> names = new List<string>();
            // Select all the column names - this is done here rather than in the while/foreach loop to save looping through this a lot of times
            // ToUpper to assist in matching

            for(int i = 0; i < result.FieldCount; i++)
            { // for 0..number of columns returned
                names.Add(result.GetName(i).ToUpper()); // Add the name of each column into list
            }
            List<PropertyInfo> mappableProperties = new List<PropertyInfo>();

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                // For each property of the object type T
                if (names.Contains(prop.Name.ToUpper())){
                    //check to see if the property name is in the column names - is it mappable?
                    mappableProperties.Add(prop);
                }
            }
            //mappableProperties contains all properties with a matching data column

            while (result.Read())
            {
                obj = new T();
                foreach(PropertyInfo prop in mappableProperties)
                {
                    // For each property of the object type T which is mappable to the sql result
                    // Set the value of the property 
                    prop.SetValue(obj, result[prop.Name]??null, null); 
                }
                list.Add(obj);
            }
            
            return list;
        }
    }

}

    
