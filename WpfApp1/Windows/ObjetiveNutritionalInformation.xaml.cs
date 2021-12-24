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
   /// Lógica de interacción para FoodNutritionalInformations.xaml
   /// </summary>
   /// 
   public class ListNutritionalInformationObjetive
   {
      public int ID { get; set; }
      public string Nutrient { get; set; }
      public decimal QuantityMin { get; set; }
      public decimal QuantityMax { get; set; }
   }
   public partial class ObjetiveNutritionalInformation : Window
   {
      public int ObjetiveId { get; set; }
      public int ObjetiveNutritionalInformationID { get; set; }

      public ObjetiveNutritionalInformation(int ObjetiveId, string ObjetiveName)
      {
         this.ObjetiveId = ObjetiveId;
         InitializeComponent();
         TextBlockTitle.Text = $"Macro nutrientes del objetivo: {ObjetiveName}";
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         ListNutritionalInformationObjetive nutritionalInformation = new ListNutritionalInformationObjetive();
         string String = $"call food.ListObjetiveNutritionalInformation({this.ObjetiveId})";
         MySqlDataReader reader = dB.Select(String);
         List<ListNutritionalInformationObjetive> listNutritionalInformationObjetive = new List<ListNutritionalInformationObjetive>();
         ManageData.CreateList(nutritionalInformation, reader, listNutritionalInformationObjetive);
         ObjetiveNutritionalInformationDataGrid.ItemsSource = listNutritionalInformationObjetive;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         ObjetivesNutritionalInformationEntity ObjetivesNutritionalInformation = new ObjetivesNutritionalInformationEntity();

         NutritionalInformationEntity nutritionalInformation = NutritionalInformation_ComboBox.SelectedItem as NutritionalInformationEntity;

         MessageBox.Show(ObjetivesNutritionalInformation.ObjetivesNutritionalInformationsInsert(this.ObjetiveId, nutritionalInformation.ID, System.Convert.ToDecimal(TextBoxQuantityMin.Text), System.Convert.ToDecimal(TextBoxQuantityMax.Text)));

         InitializeDataGrid();
      }

      public void Button_ClickDelete(object sender, RoutedEventArgs e)
      {
         ListNutritionalInformationObjetive nutritionalInformation = (ListNutritionalInformationObjetive)ObjetiveNutritionalInformationDataGrid.SelectedItem;
         if (nutritionalInformation == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }
         this.ObjetiveNutritionalInformationID = nutritionalInformation.ID;
         ObjetivesNutritionalInformationEntity objetiveNutritionalInformation = new ObjetivesNutritionalInformationEntity();
         MessageBox.Show(objetiveNutritionalInformation.ObjetiveNutritionalInformationsDelete(this.ObjetiveNutritionalInformationID));
         InitializeDataGrid();
      }

      private void NutritionalInformation_ComboBox_Intialized(object sender, EventArgs e)
      {
         DBConnect dB = new DBConnect();
         NutritionalInformationEntity NutritionalInformation = new NutritionalInformationEntity();
         MySqlDataReader FoodNutritionalInformationsReader = dB.Select("select * from nutritional_information where active = 1");
         List<NutritionalInformationEntity> NutritionalInformationList = new List<NutritionalInformationEntity>();
         ManageData.CreateList(NutritionalInformation, FoodNutritionalInformationsReader,NutritionalInformationList);
         NutritionalInformation_ComboBox.ItemsSource = NutritionalInformationList;
      }
   }
}
