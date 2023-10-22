namespace FIT5032_Week01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 9.")]
        public int? Rating { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }
    }
}
