﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
   public class DBConnect
   {
      public MySqlConnection connection;
      private string server;
      private string database;
      private string uid;
      private string password;

      //Constructor
      public DBConnect()
      {
         Initialize();
      }

      //Initialize values
      private void Initialize()
      {
         server = "localhost";
         database = "food";
         uid = "root";
         password = "TJTQ";
         string connectionString;
         connectionString = "SERVER=" + server + ";" + "DATABASE=" +
       database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

         connection = new MySqlConnection(connectionString);
      }

      //open connection to database
      public bool OpenConnection()
      {
         try
         {
            connection.Open();
            return true;
         }
         catch (MySqlException ex)
         {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
               case 0:
                  MessageBox.Show("Cannot connect to server.  Contact administrator");
                  break;

               case 1045:
                  MessageBox.Show("Invalid username/password, please try again");
                  break;
            }
            return false;
         }
      }

      //Close connection
      public bool CloseConnection()
      {
         try
         {
            connection.Close();
            return true;
         }
         catch (MySqlException ex)
         {
            MessageBox.Show(ex.Message);
            return false;
         }
      }

      //Select statement

      public MySqlDataReader Select(string query)
      {


         //Open connection
         OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

         //return list to be displayed
         return dataReader;
      }
   }
}
