using Blog.Models;
using Blog.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        CrudPost postService;

        public PostController()
        {
            string dataStore = ConfigurationManager.AppSettings["DataStore"].ToString();
            switch (dataStore)
            {
                case "DB":
                    postService = new PostService();
                    break;
                case "File":
                    postService = new PostFileService();
                    break;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (postService.findPostId(id) != null)
            {
                ViewBag.Categories = postService.DropDownEdit(id);
                return View(postService.findPostId(id));
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult UpdatePost(Post post)
        {
            postService.Edit(post);
            return RedirectToAction("Posts");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreatePost()
        {
            ViewBag.Posts = postService.DropDownCreate();
            //  ViewBag.Schools = schools;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            postService.Create(post);
            return RedirectToAction("Posts");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeletePost(int id)
        {
            postService.Delete(id);
            return RedirectToAction("Posts");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Posts()
        {
            return View(postService.getList());
        }

     /*   [HttpGet]
        public ActionResult PostsSearch(int? searchingCategory)
        {
            ViewBag.searchingCategory = postService.DropDownCreate();
            return View(postService.GetPost(searchingCategory));
        }*/

        [HttpGet]
        public ActionResult NewBlog(Category category)
        {
            ViewBag.Categories = postService.getListCategories();
            return View(postService.getList());
        }

        [HttpPost]
        public ActionResult PostSearch(int? searchingPost)
        {
            return View(postService.GetPost(searchingPost));
        }
    }
}