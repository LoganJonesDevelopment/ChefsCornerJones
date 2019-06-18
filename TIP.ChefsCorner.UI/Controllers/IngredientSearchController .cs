using System.Web.Mvc;
using TIP.ChefsCorner.BL;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace TIP.ChefsCorner.UI.Controllers
{
    public class IngredientSearchController : Controller
    {

        public async Task<ActionResult> Index(string ingred)
        {
            if (ingred != "" && ingred != null)
            {



                using (var webclient = new WebClient())
                {
                    string myurl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/ingredients/autocomplete?number=1&intolerances=egg&query=";
                    //string myurl = "https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/food/products/search?number=10&query=";
                    webclient.Headers[HttpRequestHeader.Accept] = "application/json";
                    webclient.Headers["X-RapidAPI-Key"] = "037ed14f35msh15f3f745fd822dbp10e5b8jsn21f31374305f";
                    string json = await webclient.DownloadStringTaskAsync(myurl + ingred);
                  
                    var ingredSearch = IngredientSearch.FromJson(json);

                    if (ingredSearch.Length == 0 && ingred != null)
                    {
                        ViewBag.Message = "Sorry could find that ingredient. Maybe check the spelling";
                        // TODO: display a message back that there aren't any substitutions
                        return View(ingredSearch);

                    }
                    else
                    {
                        return View(ingredSearch);
                    }


                   
                }
            }
            return View();

        }



    }
}





   