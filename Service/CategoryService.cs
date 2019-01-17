using Blog.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Blog.Service
{
    public class CategoryService : CrudCategory
    {
        DatabaseBlogContext db = new DatabaseBlogContext();

        public override void Delete(int id)
        {
            Category b = db.Categories.Find(id);
            if (b != null)
            {
                db.Categories.Remove(b);
                db.SaveChanges();
            }
        }

        public override void Edit(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
        }

        public override void Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public override Category findCategoryId(int? id)
        {
            Category category = db.Categories.Find(id);
            return category;
        }

        public override List<Category> getList()
        {
            return db.Categories.ToList();
        }
    }
}