using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public partial class Tender
    {
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string RefNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReleaseDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ClosingDate { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTime { get; set; }
        [StringLength(50)]
        public string LastUpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdatedTime { get; set; }

        [ForeignKey("CreatorId")]
        [InverseProperty("TenderCreator")]
        public virtual User Creator { get; set; }
        [ForeignKey("LastUpdatedBy")]
        [InverseProperty("TenderLastUpdatedByNavigation")]
        public virtual User LastUpdatedByNavigation { get; set; }
    }
}
