using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class ObjetiveEntity
   {
      public bool Active { get; set; }
      public int ID { get; set; }
      public string Name { get; set; }


      public ObjetiveEntity(int ID, string Name, bool Active)
      {
         this.ID = ID;
         this.Name = Name;
         this.Active = Active;
      }
      public ObjetiveEntity()
      {
      }

      public string ObjetiveInsert()
      {
         DBConnect DB = new DBConnect();
         string query = "food.ObjetiveInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string ObjetiveUpdate()
      {
         DBConnect DB = new DBConnect();
         string query = "food.ObjetiveUpdate";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("objetiveID", this.ID);
            cmd.Parameters.AddWithValue("namePar", this.Name);
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
