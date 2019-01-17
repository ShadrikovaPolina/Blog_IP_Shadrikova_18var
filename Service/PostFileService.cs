using Blog.Models;
using System.Web;
using System.Xml.Serialization;

namespace Blog.Service
{
    public class PostFileService : AbstractClassFilePost
    {
        new string Name = "Post";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Posts";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(Post));

        public PostFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.currentPath = currentPath;
        }
    }
}