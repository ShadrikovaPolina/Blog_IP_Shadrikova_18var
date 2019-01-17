using Blog.Models;
using Blog.Service;
using System.Configuration;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        CrudCategory categoryService;

        public CategoryController()
        {
            string dataStore = ConfigurationManager.AppSettings["DataStore"].ToString();
            switch (dataStore)
            {
                case "DB":
                    categoryService = new CategoryService();
                    break;
                case "File":
                    categoryService = new CategoryFileService();
                    break;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (categoryService.findCategoryId(id) != null)
            {
                return View(categoryService.findCategoryId(id));
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            categoryService.Edit(category);
            return RedirectToAction("Categories");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            categoryService.Create(category);
            return RedirectToAction("Categories");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategory(int id)
        {
            categoryService.Delete(id);
            return RedirectToAction("Categories");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Categories()
        {
            return View(categoryService.getList());
        }
    }
}