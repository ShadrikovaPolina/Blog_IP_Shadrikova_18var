using Blog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;

namespace Blog.Service
{
    public abstract class AbstractClassFileCategory : CrudCategory
    {
        public XmlSerializer xsSubmit { get; set; }
        public string currentPath { get; set; }
        public String Name { get; set; }


        public override void Create(Category category)
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
            category.Id = id;
            string newFilePath = currentPath + "/" + Name + id + ".txt";
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, category);
            File.WriteAllText(newFilePath, txtWriter.ToString());
            txtWriter.Close();
        }

        public override void Delete(int id)
        {
            File.Delete(currentPath + "/" + Name + id + ".txt");
        }

        public override void Edit(Category category)
        {
            StringWriter txtWriter = new StringWriter();
            xsSubmit.Serialize(txtWriter, category);
            File.WriteAllText(currentPath + "/" + Name + category.Id + ".txt", txtWriter.ToString());
            txtWriter.Close();
        }

        public override Category findCategoryId(int? id)
        {
            Category category;
            using (StreamReader stream = new StreamReader(currentPath + "/" + Name + id + ".txt", true))
            {
                category = (Category)xsSubmit.Deserialize(stream);
                stream.Close();
            }
            return category;
        }

        public override List<Category> getList()
        {
            string[] filesPaths = Directory.GetFiles(currentPath, "*", SearchOption.TopDirectoryOnly);
            List<Category> categorylList = new List<Category>();
            foreach (string item in filesPaths)
            {
                StreamReader stream = new StreamReader(item, true);
                Category category = (Category)xsSubmit.Deserialize(stream);
                categorylList.Add(category);
                stream.Close();
            }
            return categorylList;
        }
    }
}