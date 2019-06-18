using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIP.ChefsCorner.BL;


namespace TIP.ChefsCorner.BL
{
   public class RecipeSearch
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imagetype { get; set; }
        public int likes { get; set; }
        public string ErrorMessage { get; set; }
        
      
        public class RecipeSearchList : List<RecipeSearch>
        {

        }
    }
}
