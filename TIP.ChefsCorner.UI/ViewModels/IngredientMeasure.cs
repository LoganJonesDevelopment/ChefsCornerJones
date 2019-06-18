using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIP.ChefsCorner.BL;

namespace TIP.ChefsCorner.UI.ViewModels
{
    public class IngredientMeasure
    {
        public Measure Measure { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}