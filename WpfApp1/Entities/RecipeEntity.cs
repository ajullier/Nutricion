using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class RecipeEntity:Generic
   {

      public bool Active { get; set; }
      public int Cooking_Time { get; set; }

      public RecipeEntity(int ID, string Name, string Description, bool Active, int Cooking_Time)
      {
         this.ID = ID;
         this.Name = Name;
         this.Active = Active;
         this.Description = Description;
         this.Cooking_Time = Cooking_Time;
      }
      public RecipeEntity()
      {
      }

      public string RecipeInsert()
      {
         DBConnect DB = new DBConnect();
         string query = "food.RecipeInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
            cmd.Parameters.AddWithValue("cookingTime", this.Cooking_Time);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string RecipeUpdate()
      {
         DBConnect DB = new DBConnect();
         string query = "food.RecipeUpdate";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("recipeID", this.ID);
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
            cmd.Parameters.AddWithValue("minutes", this.Cooking_Time);
            cmd.Parameters.AddWithValue("active", this.Active);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;

            return message;
         }
      }

   }
}
