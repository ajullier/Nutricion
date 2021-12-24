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
   public partial class Foods : Window
   {
      public int ID;
      public Foods()
      {
         InitializeComponent();
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         FoodEntity Food = new FoodEntity();
         MySqlDataReader FoodsReader = dB.Select("select * from food.foods");
         List<FoodEntity> FoodList = new List<FoodEntity>();
         ManageData.CreateList(Food, FoodsReader, FoodList);
         FoodsDataGrid.ItemsSource = FoodList;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         FoodEntity Food = new FoodEntity(0, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(Food.FoodsInsert());

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         FoodEntity Food = new FoodEntity(this.ID, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(Food.FoodsUpdate());

         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         FoodEntity Food = (FoodEntity)FoodsDataGrid.SelectedItem;
         if (Food == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }
         TextBoxDescripcion.Text = Food.Description;
         TextBoxName.Text = Food.Name;
         CheckBoxActive.IsChecked = Food.Active;
         this.ID = Food.ID;
      }

      private void Button_ClickNutritionalInformation(object sender, RoutedEventArgs e)
      {
         FoodNutritionalInformation window1 = new FoodNutritionalInformation(this.ID, TextBoxName.Text);
         window1.Show();
      }
   }
}
