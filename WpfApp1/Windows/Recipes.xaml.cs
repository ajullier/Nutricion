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
   /// <summary>
   /// Lógica de interacción para Foods.xaml
   /// </summary>
   public partial class Recipes : Window
   {
      public int ID;
      public Recipes()
      {
         InitializeComponent();
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         RecipeEntity recipe = new RecipeEntity();
         MySqlDataReader recipeReader = dB.Select("select * from food.recipes");
         List<RecipeEntity> RecipeList = new List<RecipeEntity>();
         ManageData.CreateList(recipe, recipeReader, RecipeList);
         RecipesDataGrid.ItemsSource = RecipeList;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         RecipeEntity recipe = new RecipeEntity(0, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value, Convert.ToInt16(TextBox_CookingTime.Text));
         MessageBox.Show(recipe.RecipeInsert());

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         RecipeEntity recipe = new RecipeEntity(this.ID, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value, Convert.ToInt16(TextBox_CookingTime.Text));
         MessageBox.Show(recipe.RecipeUpdate());

         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         RecipeEntity recipe = (RecipeEntity)RecipesDataGrid.SelectedItem;
         if (recipe == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }
         TextBoxDescripcion.Text = recipe.Description;
         TextBoxName.Text = recipe.Name;
         CheckBoxActive.IsChecked = recipe.Active;
         TextBox_CookingTime.Text = recipe.Cooking_Time.ToString();
         this.ID = recipe.ID;
      }

      private void Button_ClickRecipeInputs(object sender, RoutedEventArgs e)
      {
         RecipeInput window1 = new RecipeInput(this.ID, TextBoxName.Text);
         window1.Show();
      }

      private void Recipe_Outputs_Click(object sender, RoutedEventArgs e)
      {
         Recipeoutput window1 = new Recipeoutput(this.ID, TextBoxName.Text);
         window1.Show();
      }
   }
}
