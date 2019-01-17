using Blog.Models;
using System.Collections.Generic;

namespace Blog.Service
{
    public abstract class CrudCategory
    {
        public abstract Category findCategoryId(int? id);

        public abstract void Edit(Category category);

        public abstract void Create(Category category);

        public abstract void Delete(int id);

        public abstract List<Category> getList();
    }
}