using Blog.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Service
{
    public abstract class CrudPost
    {
        public abstract Post findPostId(int? id);

        public abstract void Edit(Post post);

        public abstract void Create(Post post);

        public abstract void Delete(int id);

        public abstract List<Post> getList();

        public abstract SelectList DropDownCreate();

        public abstract SelectList DropDownEdit(int? id);

        public abstract List<Post> GetPost(int? searchingPost);

        public abstract List<Category> getListCategories();
    }
}