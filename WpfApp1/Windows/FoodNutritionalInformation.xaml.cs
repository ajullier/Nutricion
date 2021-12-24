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
   public class ListNutritionalInformation
   {
      public int ID { get; set; }
      public string Nutrient { get; set; }
      public decimal Quantity { get; set; }
      public string Abbreviation { get; set; }
   }
   public partial class FoodNutritionalInformation : Window
   {
      public int FoodId { get; set; }
      public int FoodNutritionalInformationID { get; set; }

      public FoodNutritionalInformation(int FoodId, string FoodName)
      {
         this.FoodId = FoodId;
         InitializeComponent();
         TextBlockTitle.Text = $"Información Nutricional de {FoodName}";
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         ListNutritionalInformation nutritionalInformation = new ListNutritionalInformation();
         string String = $"call food.ListFoodNutritionalInformation({this.FoodId})";
         MySqlDataReader reader = dB.Select(String);
         List<ListNutritionalInformation> listNutritionalInformation = new List<ListNutritionalInformation>();
         ManageData.CreateList(nutritionalInformation, reader, listNutritionalInformation);
         FoodNutritionalInformationDataGrid.ItemsSource = listNutritionalInformation;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         FoodNutritionalInformationEntity FoodNutritionalInformation = new FoodNutritionalInformationEntity();

         NutritionalInformationEntity nutritionalInformation = NutritionalInformation_ComboBox.SelectedItem as NutritionalInformationEntity;
         UnitEntity unit = Units_ComboBox.SelectedItem as UnitEntity;

         MessageBox.Show(FoodNutritionalInformation.FoodNutritionalInformationsInsert(this.FoodId, nutritionalInformation.ID, unit.ID, System.Convert.ToDecimal(TextBoxQuantity.Text)));

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         FoodNutritionalInformationEntity foodNutritionalInformation = new FoodNutritionalInformationEntity();
         UnitEntity unit = Units_ComboBox.SelectedItem as UnitEntity;
         MessageBox.Show(foodNutritionalInformation.FoodNutritionalInformationsUpdate(this.FoodNutritionalInformationID, System.Convert.ToDecimal(TextBoxQuantity.Text), unit.ID));
         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         ListNutritionalInformation nutritionalInformation = (ListNutritionalInformation)FoodNutritionalInformationDataGrid.SelectedItem;
         if (nutritionalInformation == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }

         List<NutritionalInformationEntity> nutritionalInformationSource = NutritionalInformation_ComboBox.ItemsSource as List<NutritionalInformationEntity>;
         var selectedItem = nutritionalInformationSource.FirstOrDefault(x => x.Name == nutritionalInformation.Nutrient);
         int selectedIndex = nutritionalInformationSource.IndexOf(selectedItem);
         NutritionalInformation_ComboBox.SelectedIndex = selectedIndex;


         List<UnitEntity> unitSource = Units_ComboBox.ItemsSource as List<UnitEntity>;
         var selectedItem2 = unitSource.FirstOrDefault(x => x.Abbreviation == nutritionalInformation.Abbreviation);
         var selectedIndex2 = unitSource.IndexOf(selectedItem2);
         Units_ComboBox.SelectedIndex = selectedIndex;

         TextBoxQuantity.Text = nutritionalInformation.Quantity.ToString();

         this.FoodNutritionalInformationID = nutritionalInformation.ID;

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
