using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
   public class InOutRecipeEntity
   {
      public int ID { get; set; }
      public int Recipe_ID { get; set; }
      public int Food_ID { get; set; }
      public decimal Quantity { get; set; }
      public int Unit_ID { get; set; }

   }
}
