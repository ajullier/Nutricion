using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class RecipeOutputEntity: InOutRecipeEntity
   {
      public RecipeOutputEntity(int ID, int Recipe_ID, int Food_ID, decimal Quantity, int Unit_ID)
      {
         this.ID = ID;
         this.Recipe_ID = Recipe_ID;
         this.Food_ID = Food_ID;
         this.Quantity = Quantity;
         this.Unit_ID = Unit_ID;
      }
      public RecipeOutputEntity()
      {
      }
      public string Insert(int recipeID, int foodID, int unitID, decimal quantity)
      {
         DBConnect DB = new DBConnect();
         string query = "RecipeOutputInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("recipeID", recipeID);
            cmd.Parameters.AddWithValue("foodID", foodID);
            cmd.Parameters.AddWithValue("unitID", unitID);
            cmd.Parameters.AddWithValue("quantity", quantity);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string Detele(int recipeOutputID)
      {
         DBConnect DB = new DBConnect();
         string query = "RecipeOutputsDelete";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("recipeOutputID", recipeOutputID);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;

            return message;
         }
      }

   }
}
