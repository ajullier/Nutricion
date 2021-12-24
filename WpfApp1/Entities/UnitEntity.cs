using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class UnitEntity:Generic
   {
      public string Abbreviation { get; set; }
      public bool Active { get; set; }

      public UnitEntity(int ID, string Name, string Abbreviation, string Description, bool Active)
      {
         this.ID = ID;
         this.Name = Name;
         this.Abbreviation = Abbreviation;
         this.Active = Active;
         this.Description = Description;
      }
      public UnitEntity()
      {
      }

      public string UnitsInsert()
      {
         DBConnect DB = new DBConnect();
         string query = "food.UnitInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("abbreviationPar", this.Abbreviation);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string UnitsUpdate()
      {
         DBConnect DB = new DBConnect();
         string query = "food.UnitUpdate";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("unitID", this.ID);
            cmd.Parameters.AddWithValue("namePar", this.Name);
            cmd.Parameters.AddWithValue("abbreviationPar", this.Abbreviation);
            cmd.Parameters.AddWithValue("descriptionPar", this.Description);
            cmd.Parameters.AddWithValue("activePar", this.Active);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;

            return message;
         }
      }

   }
}
