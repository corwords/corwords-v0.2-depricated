using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Corwords.Web.Models
{
    public class BlogPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string Permalink { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int? OriginalBlogPostId { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<BlogPostTag> BlogPostTags { get; set; }
    }
}