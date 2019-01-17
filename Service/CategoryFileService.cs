using Blog.Models;
using System.Web;
using System.Xml.Serialization;

namespace Blog.Service
{
    public class CategoryFileService : AbstractClassFileCategory
    {
        new string Name = "Category";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Categories";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(Category));

        public CategoryFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.currentPath = currentPath;
        }
    }
}