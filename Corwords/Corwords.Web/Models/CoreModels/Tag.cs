﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<BlogTag> BlogTags { get; set; }
        public List<BlogPostTag> BlogPostTags { get; set; }
    }
}