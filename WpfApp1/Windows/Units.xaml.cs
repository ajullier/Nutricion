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
   /// Lógica de interacción para Units.xaml
   /// </summary>
   public partial class Units : Window
   {
      public int ID;
      public Units()
      {
         InitializeComponent();
         InitializeDataGrid();

      }
      public void InitializeDataGrid()
      {
         DBConnect dB = new DBConnect();
         UnitEntity unit = new UnitEntity();
         MySqlDataReader unitsReader = dB.Select("select * from food.units");
         List<UnitEntity> unitList = new List<UnitEntity>();
         ManageData.CreateList(unit, unitsReader, unitList);
         UnitsDataGrid.ItemsSource = unitList;
      }

      public void Button_ClickSave(object sender, RoutedEventArgs e)
      {
         UnitEntity unit = new UnitEntity(0, TextBoxName.Text, TextBoxAbbreviation.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(unit.UnitsInsert());

         InitializeDataGrid();
      }

      public void Button_ClickModify(object sender, RoutedEventArgs e)
      {
         UnitEntity unit = new UnitEntity(this.ID, TextBoxName.Text, TextBoxAbbreviation.Text, TextBoxDescripcion.Text, CheckBoxActive.IsChecked.Value);
         MessageBox.Show(unit.UnitsUpdate());

         InitializeDataGrid();
      }

      private void Button_ClickGetValues(object sender, RoutedEventArgs e)
      {
         UnitEntity unit = (UnitEntity)UnitsDataGrid.SelectedItem;
         if (unit == null)
         {
            MessageBox.Show("Debe seleccionar un ítem de la grilla primero");
            return;
         }
         TextBoxAbbreviation.Text = unit.Abbreviation;
         TextBoxDescripcion.Text = unit.Description;
         TextBoxName.Text = unit.Name;
         CheckBoxActive.IsChecked = unit.Active;
         this.ID = unit.ID;
      }
   }
}
