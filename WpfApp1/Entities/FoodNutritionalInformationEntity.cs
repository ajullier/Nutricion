using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WpfApp1
{
   public class FoodNutritionalInformationEntity
   {
      public string FoodNutritionalInformationsInsert(int foodId, int nutritionalInformationId, int unitId, decimal quantity)
      {
         DBConnect DB = new DBConnect();
         string query = "FoodNutritionalInformationInsert";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("nutritionalInformationID", nutritionalInformationId);
            cmd.Parameters.AddWithValue("foodID", foodId);
            cmd.Parameters.AddWithValue("unitID", unitId);
            cmd.Parameters.AddWithValue("quantity", quantity);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;
           
            return message;
         }
      }

      public string FoodNutritionalInformationsUpdate(int foodNutritionalInformationID, decimal quantity, int unitID)
      {
         DBConnect DB = new DBConnect();
         string query = "FoodNutritionalInformationUpdate";
         using (var db = DB.connection)
         {
            db.Open();
            var cmd = new MySqlCommand(query, db);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("foodNutritionalInformationID", foodNutritionalInformationID);
            cmd.Parameters.AddWithValue("unitID", unitID);
            cmd.Parameters.AddWithValue("quantity", quantity);
            cmd.Parameters.Add("message", MySqlDbType.VarChar, 500);
            cmd.Parameters["@message"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string message = (string)cmd.Parameters["@message"].Value;

            return message;
         }
      }

   }
}
