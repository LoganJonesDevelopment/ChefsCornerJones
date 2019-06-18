using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIP.ChefsCorner.BL;
using TIP.ChefsCorner.UI.Models;
using TIP.ChefsCorner.UI.ViewModels;

namespace TIP.ChefsCorner.UI.Controllers
{
    public class RecipeController : Controller
    {
        RecipeList recipelist;
        Login login;
        // GET: Recipe
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated())
            {
                recipelist = new RecipeList();
                FetchLogin();
                recipelist.Load(login.Id);
                return View(recipelist);
            } else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Recipe recipe = new Recipe();
                FetchLogin();
                recipe.LoadById(id);
                if (recipe.UserId == login.Id)
                {
                    return View(recipe);
                } else
                {
                    return RedirectToAction("Recipe", "Index", new { returnurl = HttpContext.Request.Url });
                }
            } else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }          
        }

        // GET: Recipe/Create
        public ActionResult Create(/*int i = 1*/)
        {
            //ViewBag.i = i;
            if (Authenticate.IsAuthenticated())
            {
                login = FetchLogin();
                //RecipeIngredients rcin = new RecipeIngredients();
                //rcin.Recipe = new Recipe();
                //rcin.MeasureList = new MeasureList();
                //rcin.Ingredients = new List<IngredientMeasure>();
                //for (int n = 1; n <= i; n++)
                //{
                //    IngredientMeasure im = new IngredientMeasure();
                //    im.Ingredient = new Ingredient();
                //    im.Measure = new Measure();
                //    rcin.Ingredients.Add(im);
                //}
                //rcin.MeasureList.Load();
                //rcin.Recipe.UserId = login.Id;
                //rcin.Recipe.ImagePath = "";
                //rcin.Recipe.UserName = login.ScreenName;
                //return View(rcin);
                Recipe recipe = new Recipe();
                return View(recipe);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Recipe/Create
        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            try
            {
                //IngredientList ingredients = new IngredientList();
                //foreach (IngredientMeasure im in recipemodel.Ingredients)
                //{
                //    ingredients.Add(im.Ingredient);
                //}
                //IngredientChecker.CheckIngredients(ingredients);
                //IEnumerable<IngredientMeasure> newingredientids = new List<IngredientMeasure>();
                //newingredientids = recipemodel.Ingredients;
                //newingredientids.ToList().ForEach(ingredientid => BL.RecipeIngredient.Add(recipemodel.Recipe.Id, ingredientid.Ingredient.Id, ingredientid.Measure.Id));
                //recipemodel.Recipe.Insert();
                login = FetchLogin();
                recipe.UserId = login.Id;
                recipe.UserName = login.ScreenName;
                recipe.Insert();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                //RecipeIngredients rcin = new RecipeIngredients();
                //rcin.Recipe = new Recipe();
                //rcin.MeasureList = new MeasureList();
                //rcin.Recipe.LoadById(id);
                //rcin.MeasureList.Load();
                //FetchLogin();
                //if (rcin.Recipe.UserId == login.Id)
                //{
                //    IEnumerable<IngredientMeasure> existingIngredientIds = new List<IngredientMeasure>();
                //    List<IngredientMeasure> ingredientmeasures = new List<IngredientMeasure>();
                //    foreach (MeasuredIngredient mi in rcin.Recipe.Ingredients)
                //    {
                //        IngredientMeasure im = new IngredientMeasure();
                //        im.Ingredient.Id = mi.Id;
                //        im.Measure.Id = mi.Measurement.Id;
                //        ingredientmeasures.Add(im);
                //    }
                //    existingIngredientIds = ingredientmeasures;
                //    Session["ingredientids"] = existingIngredientIds;
                //    return View(rcin);
                //}
                //else
                //{
                //    return RedirectToAction("Recipe", "Index", new { returnurl = HttpContext.Request.Url });
                //}                
                Recipe recipe = new Recipe();
                recipe.LoadById(id);
                return View(recipe);
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Recipe recipe)
        {
            try
            {
                //IEnumerable<IngredientMeasure> oldingredientids = new List<IngredientMeasure>();
                //if (Session["ingredientids"] != null)
                //    oldingredientids = (IEnumerable<IngredientMeasure>)Session["ingredientids"];
                //IEnumerable<IngredientMeasure> newingredientids = new List<IngredientMeasure>();
                ////if (rcin.IngredientIds != null) This property in the viewmodel is being removed, use rcin.Ingredients instead to fetch the ids.
                ////    newingredientids = rcin.IngredientIds;
                //IEnumerable<IngredientMeasure> deletes = oldingredientids.Except(newingredientids);
                //IEnumerable<IngredientMeasure> adds = newingredientids.Except(oldingredientids);
                //adds.ToList().ForEach(i => RecipeIngredient.Add(id, i.Ingredient.Id, i.Measure.Id));
                //deletes.ToList().ForEach(i => RecipeIngredient.Delete(id, i.Ingredient.Id, i.Measure.Id));
                //rcin.Recipe.Update();
                recipe.Update();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int id)
        {
            if (Authenticate.IsAuthenticated())
            {
                Recipe recipe = new Recipe();
                recipe.LoadById(id);
                FetchLogin();
                if (login.Id == recipe.UserId)
                {
                    try
                    {
                        recipe.Delete(id);
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                } else
                {
                    return RedirectToAction("Index");
                }                
            }
            else
            {
                return RedirectToAction("Login", "Login", new { returnurl = HttpContext.Request.Url });
            }
        }
        private Login FetchLogin() //Initialises the login field and retrieves data from session, should only be used within an if statement with Authenticate.IsAuthenticated to prevent error.
        {
            login = new Login();
            login = (Login)Session["user"];
            return login;
        }
    }
}
