
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace khantlintheinDotNetcore.MVCApp.Models
{
    [Table("Tb_blog")]
    public class BlogDataModel
    {
        [Key]
        public int Blog_ID { get; set; }

        public string? Blog_Name { get; set; }

        public string? Blog_Title { get; set; }

        public string? Blog_Content { get; set; }

        public string? Blog_Category { get; set; }
    }
}
