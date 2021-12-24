using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using WpfApp1.Templates;
using WpfApp1.Utilities;

namespace WpfApp1.Windows
{

   public class RecipeOutputList
   {
      public int ID { get; set; }
      public string Food { get; set; }
      public decimal Quantity { get; set; }
      public string Unit { get; set; }
   }
   public partial class Recipeoutput : Window
   {
      public int RecipeId { get; set; }
      public int RecipeOutputID { get; set; }

      public Recipeoutput(int RecipeId, string RecipeName)
      {
         this.RecipeId = RecipeId;
         InitializeComponent();
         TextBlockTitle.Text = $"Ingredientes de {RecipeName}";
         InitializeDataGrid();
      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         RecipeOutputList recipeOutput = new RecipeOutputList();
         string String = $"call food.ListRecipeOutput({this.RecipeId})";
         MySqlDataReader reader = dB.Select(String);
         List<RecipeOutputList> listRecipeOutput = new List<RecipeOutputList>();
         ManageData.CreateList(recipeOutput, reader, listRecipeOutput);
         RecipeoutputDataGrid.ItemsSource = listRecipeOutput;

      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         RecipeOutputEntity recipeOutput = new RecipeOutputEntity();

         FoodEntity food= Food_ComboBox.SelectedItem as FoodEntity;
         UnitEntity unit = Units_ComboBox.SelectedItem as UnitEntity;

         MessageBox.Show(recipeOutput.Insert(this.RecipeId, food.ID, unit.ID, System.Convert.ToDecimal(TextBoxQuantity.Text)));

         InitializeDataGrid();
      }

      public void Button_ClickDelete(object sender, RoutedEventArgs e)
      {
         RecipeOutputList recipeOutputList = (RecipeOutputList)RecipeoutputDataGrid.SelectedItem;
         if (recipeOutputList == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }

         RecipeOutputEntity recipeOutput = new RecipeOutputEntity();
         
         recipeOutput.Detele(recipeOutputList.ID);
         InitializeDataGrid();
      }

      private void Food_ComboBox_Intialized(object sender, EventArgs e)
      {
         DBConnect dB = new DBConnect();
         FoodEntity food = new FoodEntity();
         MySqlDataReader reader = dB.Select("select * from foods where active = 1");
         List<FoodEntity> foodList = new List<FoodEntity>();
         ManageData.CreateList(food, reader,foodList);
         Food_ComboBox.ItemsSource = foodList;
      }

      private void Units_ComboBox_Intialized(object sender, EventArgs e)
      {
         DBConnect dB = new DBConnect();
         UnitEntity unit = new UnitEntity();
         MySqlDataReader UnitReader = dB.Select("select * from units where active = 1");
         List<UnitEntity> unitList = new List<UnitEntity>();
         ManageData.CreateList(unit, UnitReader, unitList);
         Units_ComboBox.ItemsSource = unitList;
      }
   }
}
