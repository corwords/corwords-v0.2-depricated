using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models.CoreModels
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}