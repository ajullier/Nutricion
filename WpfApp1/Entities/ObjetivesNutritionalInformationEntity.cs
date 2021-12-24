using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class ObjetivesNutritionalInformationEntity
   {
      public string ObjetivesNutritionalInformationsInsert(int objetiveId, int nutritionalInformationId, decimal quantityMin, decimal quantityMax)
      {
         DBConnect DB = new DBConnect();
         string query = "ObjetiveNutritionalInformationInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nutritionalInformationID", nutritionalInformationId);
            cmd.Parameters.AddWithValue("objetiveID", objetiveId);
            cmd.Parameters.AddWithValue("quantityMin", quantityMin);
            cmd.Parameters.AddWithValue("quantityMax", quantityMax);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string ObjetiveNutritionalInformationsDelete(int objetiveNutritionalInformationID)
      {
         DBConnect DB = new DBConnect();
         string query = "ObjetiveNutritionalInformationDelete";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("objetiveNutritionalInformationID", objetiveNutritionalInformationID);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;

            return message;
         }
      }

   }
}
