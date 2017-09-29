using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class Blog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string Username { get; set; }

        public List<BlogTag> BlogTags { get; set; }
    }
}