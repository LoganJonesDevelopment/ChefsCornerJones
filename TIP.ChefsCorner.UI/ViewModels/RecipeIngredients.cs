using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIP.ChefsCorner.BL;

namespace TIP.ChefsCorner.UI.ViewModels
{
    public class RecipeIngredients
    {
        public Recipe Recipe { get; set; }
        public MeasureList MeasureList { get; set; }
        public List<IngredientMeasure> Ingredients { get; set; }
        //public IEnumerable<IngredientMeasure> IngredientIds { get; set; }
    }   
}