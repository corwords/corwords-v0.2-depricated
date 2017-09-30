using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class BlogTag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogTagId { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}