namespace FIT5032_Week01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
    }
}
