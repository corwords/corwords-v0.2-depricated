﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class Blog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string BaseUrl { get; set; }
    }
}