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

namespace WpfApp1
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }
      
      public void Units_Click(object sender, RoutedEventArgs e)
      {
         Windows.Units window1 = new Windows.Units();
         window1.Show();
      }

      private void Foods_Click(object sender, RoutedEventArgs e)
      {
         Windows.Foods window1 = new Windows.Foods();
         window1.Show();
      }

      private void NutricionalInformacion_Clic(object sender, RoutedEventArgs e)
      {
         Windows.NutritionalInformation window1 = new Windows.NutritionalInformation();
         window1.Show();
      }

      private void PRUEBA_CLIC(object sender, RoutedEventArgs e)
      {
       
      }

      private void Recipes_Clic(object sender, RoutedEventArgs e)
      {
         Windows.Recipes window1 = new Windows.Recipes();
         window1.Show();
      }

      private void Objetives_Click(object sender, RoutedEventArgs e)
      {
         Windows.Objetives window1 = new Windows.Objetives();
         window1.Show();
      }
   }
}
