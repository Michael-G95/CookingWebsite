using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Logic
{
    public static class QueryProcessor
    {
        // static class to allow interfacing between SQLDataAccess and a UI/Controller
        


        public static List<T> GetCategories<T>()
            // Function to ute the GetCategories stored procedure
            where T : new() // T must be instantiable as it is a data class
        {
            
            return SQLDataAccess.DirectDataQuery<T>("GetCategories");
        }

        public static List<T> GetComments<T>()
           // Function to ute the GetCategories stored procedure
           where T : new() // T must be instantiable as it is a data class
        {
            return SQLDataAccess.DirectDataQuery<T>("GetComments");
        }

        public static T GetUserRole<T>(string username)
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            {//Add parameters
                {"@username",username }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetUserRole",Sqlparams)[0];
        }

        public static List<T> GetProductsByCategory<T>()
            // Function to ute the GetCategories stored procedure
            where T : new() // T must be instantiable as it is a data class
        {
            return SQLDataAccess.DirectDataQuery<T>("GetProductsByCategory");
        }

        public static List<T> GetProductsForName<T>(string argument)
            // Function to ute the GetCategories stored procedure
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            {//Add parameters
                {"@name",argument }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetProductsForName", Sqlparams);
        }

        public static List<T> GetProductsForCategory<T>(string argument)
            // Function to ute the GetCategories stored procedure
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@CategoryName",argument }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetProductsForCategory",Sqlparams);
        }

        public static int InsertUser(string username, string password,string role)
        {
            // Function to ute the InsertUser stored procedure
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            {
                { "@username", username },
                { "@password", password },
                { "@role", role }
            };
            return SQLDataAccess.DirectDataUpdate("InsertUser",Sqlparams);
        }

        public static T GetProductForID<T>(string argument)
            // Function to ute the GetProductForID stored procedure, and return the **first** record retrieved
            // This is done as the procedure will only return 1 or 0 records - cant have a duplicate primary key in the database (this would be an error in sql)
            // Return new object T if any other than 1 record - either an error in sql, or none for that ID 
            where T :  new() // T must be instantiable as it is a data class
        {

            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@ProductID",argument }
            };

            var results = SQLDataAccess.DirectDataQuery<T>("GetProductForID ",Sqlparams); // Run query
            return (results.Count != 1) ? new T() : results[0]; // If results count isnt 1, return a new T, else the first results record
        }

        public static List<T> GetRoles<T>()
            where T : new() // T must be instantiable as it is a data class
        {
            return SQLDataAccess.DirectDataQuery<T>("GetRoles");
        }

        public static List<T> GetRecipes<T>()
            // Function to ute the GetRecipes stored procedure
            // To get all recipe records
            where T : new() // T must be instantiable as it is a data class
        {
            return SQLDataAccess.DirectDataQuery<T>("GetRecipes");
        }
        public static List<T> GetRecipesForName<T>(string argument)
            // Function to ute the GetRecipes stored procedure
            // To get all recipe records where *argument* like recipetitle
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeTitle",argument }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetRecipes",Sqlparams);
        }

        public static T GetRecipeForID<T>(string argument)
            // Function to ute the GetRecipeForID stored procedure, and return the **first** record retrieved
            // This is done as the procedure will only return 1 or 0 records - cant have a duplicate primary key in the database (this would be an error in sql)
            // Return new object T if any other than 1 record - either an error in sql, or none for that ID 
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeID",argument }
            };
            var results = SQLDataAccess.DirectDataQuery<T>("GetRecipeForID ",Sqlparams); // Run query
            return (results.Count != 1) ? new T() : results[0]; // If results count isnt 1, return a new T, else the first results record
        }
        public static List<T> GetMealCategories<T>()
             where T : new() // T must be instantiable as it is a data class
        {
            return SQLDataAccess.DirectDataQuery<T>("GetMealCategories");
        }

        public static List<T> GetRecipesForCategory<T>(string CategoryName)
             where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@CategoryName",CategoryName }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetRecipesForCategoryName",Sqlparams);
        }
        public static List<T> GetRecipeMethodForRecipeID<T>(int RecipeID)
             where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeID",RecipeID }
            };
            return SQLDataAccess.DirectDataQuery<T>("GetRecipeMethodForRecipeID",Sqlparams); // Run query
        }
        public static List<T> GetRecipeIngredientsForRecipeID<T>(int RecipeID)
             where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeID",RecipeID}
            };
            return SQLDataAccess.DirectDataQuery<T>("GetRecipeIngredientsForRecipeID",Sqlparams);
        }

        public static List<T> SearchRecipesForName<T>(string Query)
             where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@Query",Query }
            };
            return SQLDataAccess.DirectDataQuery<T>("SearchRecipesForName ",Sqlparams);
        }

        public static T GetFirstRestaurant<T>()
            // Function to ute the GetProductForID stored procedure, and return the **first** record retrieved
            // This is done as the procedure will only return 1 or 0 records - cant have a duplicate primary key in the database (this would be an error in sql)
            // Return new object T if any other than 1 record - either an error in sql, or none for that ID 
            where T : new() // T must be instantiable as it is a data class
        {

            var results = SQLDataAccess.DirectDataQuery<T>("GetFirstRestaurant "); // Run query

            return (results.Count != 1) ? new T() : results[0]; // If results count isnt 1, return a new T, else the first results record
        }

        public static T GetRestaurantForID<T>(string argument)
            // Function to ute the GetProductForID stored procedure, and return the **first** record retrieved
            // This is done as the procedure will only return 1 or 0 records - cant have a duplicate primary key in the database (this would be an error in sql)
            // Return new object T if any other than 1 record - either an error in sql, or none for that ID 
            where T : new() // T must be instantiable as it is a data class
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RestaurantID",argument }
            };
            var results = SQLDataAccess.DirectDataQuery<T>("GetRestaurantForID ",Sqlparams); // Run query

            return (results.Count != 1) ? new T() : results[0]; // If results count isnt 1, return a new T, else the first results record
        }

        public static int InsertComment(string Name,string Email, string Subject, string Message)
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@CommentName",Name },
                {"@CommentEmail",Email},
                {"@CommentSubject",Subject},
                {"@CommentMessage",Message }
            };
            return SQLDataAccess.DirectDataUpdate("insertComment",Sqlparams);
        }

        public static bool ValidateAccount(string AccountName,string Password)
        {
            // Funciton to ute the ValidateAccount stored procedure, returning true if any records are returned (the account is valid)
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@Username",AccountName },
                {"@Password",Password }
            };
            return
                (SQLDataAccess.DirectDataQuery<Object>("ValidateAccount",Sqlparams).Count > 0);
        }

        public static int InsertRecipe(string RecipeTitle,string RecipeDescription, string RecipeCreator, int RecipePortions, string CategoryName)
        {
            // Function to ute the InsertRecipe procedure and return the resulting rows affected
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeTitle",RecipeTitle },
                {"@RecipeDescription",RecipeDescription },
                {"@RecipeCreator",RecipeCreator },
                {"@RecipePortions",RecipePortions },
            };
            var rows = SQLDataAccess.DirectDataUpdate("InsertRecipe",Sqlparams);
            Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeTitle",RecipeTitle },
                {"@CategoryName",CategoryName },
            };
            rows += SQLDataAccess.DirectDataUpdate("LinkRecipeCategory",Sqlparams);
            return rows;
        }


        public static int InsertRecipeMethod(string RecipeName,string RecipeMethod)
        {
            // Function to ute the InsertRecipeMethod procedure and return the resulting rows affected
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeName",RecipeName },
                {"@MethodText",RecipeMethod },
            };
            return SQLDataAccess.DirectDataUpdate("InsertRecipeMethod",Sqlparams);
        }

        public static int InsertRecipeIngredient(string RecipeName, string IngredientName,decimal IngredientQuantity, string IngredientUnits)
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@RecipeName",RecipeName },
                {"@IngredientName",IngredientName },
                {"@IngredientQuantity",IngredientQuantity },
                {"@IngredientUnit",IngredientUnits },
            };
            // Function to ute the InsertRecipeIngredient procedure and return the resulting rows affected
            return SQLDataAccess.DirectDataUpdate("InsertRecipeIngredient",Sqlparams);
        }

        public static bool ValidateLogin (string Username, string Password)
        {
            Dictionary<string, object> Sqlparams = new Dictionary<string, object>
            { //Add parameters
                {"@Username",Username },
                {"@Password",Password },
            };
            return (SQLDataAccess.DirectIntResultDataQuery("ValidateLogin",Sqlparams)) > 0 ? true : false; 
        }


    }
}
