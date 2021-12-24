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
   public partial class Objetives : Window
   {
      public int ID;
      public Objetives()
      {
         InitializeComponent();
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         ObjetiveEntity Objetive = new ObjetiveEntity();
         MySqlDataReader ObjetivesReader = dB.Select("select * from food.objetives");
         List<ObjetiveEntity> ObjetiveList = new List<ObjetiveEntity>();
         ManageData.CreateList(Objetive, ObjetivesReader, ObjetiveList);
         ObjetivesDataGrid.ItemsSource = ObjetiveList;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         ObjetiveEntity Objetive = new ObjetiveEntity(0, TextBoxName.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(Objetive.ObjetiveInsert());

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         ObjetiveEntity Objetive = new ObjetiveEntity(this.ID, TextBoxName.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(Objetive.ObjetiveUpdate());

         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         ObjetiveEntity Objetive = (ObjetiveEntity)ObjetivesDataGrid.SelectedItem;
         if ( Objetive == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }

         TextBoxName.Text = Objetive.Name;
         CheckBoxActive.IsChecked = Objetive.Active;
         this.ID = Objetive.ID;
      }

      private void Button_ClickNutritionalInformation(object sender, RoutedEventArgs e)
      {
         ObjetiveNutritionalInformation window1 = new ObjetiveNutritionalInformation(this.ID, TextBoxName.Text);
         window1.Show();
      }
   }
}
