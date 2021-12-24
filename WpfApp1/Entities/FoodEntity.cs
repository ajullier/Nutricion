using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class FoodEntity:Generic
   {
      public bool Active { get; set; }

      public FoodEntity(int ID, string Name, string Description, bool Active)
      {
         this.ID = ID;
         this.Name = Name;
         this.Active = Active;
         this.Description = Description;
      }
      public FoodEntity()
      {
      }

      public string FoodsInsert()
      {
         DBConnect DB = new DBConnect();
         string query = "food.FoodInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string FoodsUpdate()
      {
         DBConnect DB = new DBConnect();
         string query = "food.FoodUpdate";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("foodID", this.ID);
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
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
