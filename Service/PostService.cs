using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Service
{
    public class PostService : CrudPost
    {
        DatabaseBlogContext db = new DatabaseBlogContext();

        public override void Delete(int id)
        {
            Post b = db.Posts.Find(id);
            if (b != null)
            {
                db.Posts.Remove(b);
                db.SaveChanges();
            }
        }

        public override void Edit(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
        }

        public override void Create(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
        }

        public override Post findPostId(int? id)
        {
            Post post = db.Posts.Find(id);
            return post;
        }

        public override List<Post> getList()
        {
            return db.Posts.ToList();
        }

        public override SelectList DropDownCreate()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            return categories;
        }

        public override SelectList DropDownEdit(int? id)
        {
            Post post = db.Posts.Find(id);
            SelectList categories = new SelectList(db.Categories, "Id", "Name", post.CategoryId);
            return categories;
        }

        public override List<Post> GetPost(int? searchingPost)
        {
            var result = db.Posts.Where(x => x.CategoryId == searchingPost).ToList();
            return result;
        }

        public override List<Category> getListCategories()
        {
            return db.Categories.ToList();
        }
    }
}