using Blog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Blog.Service
{
    public abstract class AbstractClassFilePost : CrudPost
    {
        public XmlSerializer xsSubmit { get; set; }
        public XmlSerializer xsSubmitCategory = new XmlSerializer(typeof(Category));
        public string currentPath { get; set; }
        public string currentPathCategory = HttpContext.Current.Server.MapPath("~") + "/Files/Categories";
        public String Name { get; set; }

        public override void Create(Post post)
        {
            int max = 0;
            foreach (var path in Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly))
            {
                Match m = Regex.Match(path, @"" + Name + @"\d+");
                int currentId = Convert.ToInt32(m.Value.Replace(Name, ""));
                if (currentId > max)
                {
                    max = currentId;
                }
            }
            int id = max + 1;
            post.Id = id;
            string newFilePath = currentPath + "/" + Name + id + ".txt";
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, post);
            File.WriteAllText(newFilePath, txtWriter.ToString());
            txtWriter.Close();
        }

        public override void Delete(int id)
        {
            File.Delete(currentPath + "/" + Name + id + ".txt");
        }

        public override void Edit(Post post)
        {
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, post);
            File.WriteAllText(currentPath + "/" + Name + post.Id + ".txt", txtWriter.ToString());
            txtWriter.Close();
        }

        public override Post findPostId(int? id)
        {
            Post post;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                post = (Post)xsSubmit.Deserialize(stream);
                stream.Close();
            }
            return post;
        }

        public override List<Post> getList()
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<Post> postList = new List<Post>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Post post = (Post)xsSubmit.Deserialize(stream);
                postList.Add(post);
                stream.Close();
            }
            return postList;
        }

        public override SelectList DropDownCreate()
        {
            string[] filesPaths = Directory.GetFiles(currentPathCategory, "*", SearchOption.TopDirectoryOnly);
            List<Category> categoryList = new List<Category>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Category category = (Category)xsSubmitCategory.Deserialize(stream);
                categoryList.Add(category);
                stream.Close();
            }
            SelectList categories = new SelectList(categoryList, "Id", "Name");
            return categories;
        }

        public override SelectList DropDownEdit(int? id)
        {
            Post post;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                post = (Post)xsSubmit.Deserialize(stream);
                stream.Close();
            }

            string[] filesPaths = Directory.GetFiles(currentPathCategory, "*", SearchOption.TopDirectoryOnly);
            List<Category> categoryList = new List<Category>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Category category = (Category)xsSubmitCategory.Deserialize(stream);
                categoryList.Add(category);
                stream.Close();
            }
            SelectList schools = new SelectList(categoryList, "Id", "Name", post.CategoryId); /////
            return schools;
        }

        public override List<Post> GetPost(int? searchingPost)
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<Post> postList = new List<Post>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Post post = (Post)xsSubmit.Deserialize(stream);
                postList.Add(post);
                stream.Close();
            }
            var result = postList.Where(x => x.CategoryId == searchingPost).ToList();
            return result;
        }

        public override List<Category> getListCategories()
        {
            string[] filesPaths = Directory.GetFiles(currentPathCategory, "*", SearchOption.TopDirectoryOnly);
            List<Category> categoryList = new List<Category>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Category category = (Category)xsSubmitCategory.Deserialize(stream);
                categoryList.Add(category);
                stream.Close();
            }
            return categoryList;
        }
    }
}