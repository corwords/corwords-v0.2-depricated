using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class BlogPostTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogPostTagId { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}