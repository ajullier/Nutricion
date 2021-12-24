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
   /// Lógica de interacción para NutritionalInformations.xaml
   /// </summary>
   public partial class NutritionalInformation : Window
   {
      public int ID = 0;
      public NutritionalInformation()
      {
         InitializeComponent();
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         NutritionalInformationEntity NutritionalInformation = new NutritionalInformationEntity();
         MySqlDataReader NutritionalInformationsReader = dB.Select("select * from food.nutritional_information");
         List<NutritionalInformationEntity> NutritionalInformationList = new List<NutritionalInformationEntity>();
         ManageData.CreateList(NutritionalInformation, NutritionalInformationsReader, NutritionalInformationList);
         NutritionalInformationDataGrid.ItemsSource = NutritionalInformationList;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         NutritionalInformationEntity NutritionalInformation = new NutritionalInformationEntity(0, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(NutritionalInformation.NutritionalInformationsInsert());

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         NutritionalInformationEntity NutritionalInformation = new NutritionalInformationEntity(ID, TextBoxName.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(NutritionalInformation.NutritionalInformationsUpdate());

         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         NutritionalInformationEntity NutritionalInformation = (NutritionalInformationEntity)NutritionalInformationDataGrid.SelectedItem;
         if (NutritionalInformation == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }
         TextBoxDescripcion.Text = NutritionalInformation.Description;
         TextBoxName.Text = NutritionalInformation.Name;
         CheckBoxActive.IsChecked = NutritionalInformation.Active;
         this.ID = NutritionalInformation.ID;
      }
   }
}
