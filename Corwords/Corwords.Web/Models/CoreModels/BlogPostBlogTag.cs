using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class BlogPostBlogTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogPostBlogTagId { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public int BlogTagId { get; set; }
        public BlogTag BlogTag { get; set; }
    }
}