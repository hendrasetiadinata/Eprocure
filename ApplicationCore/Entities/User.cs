using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public partial class User
    {
        public User()
        {
            InverseCreatedByNavigation = new HashSet<User>();
            TenderCreator = new HashSet<Tender>();
            TenderLastUpdatedByNavigation = new HashSet<Tender>();
        }

        [StringLength(50)]
        public string UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string PasswordSalt { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        public bool Active { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedTime { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("InverseCreatedByNavigation")]
        public virtual User CreatedByNavigation { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<User> InverseCreatedByNavigation { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Tender> TenderCreator { get; set; }
        [InverseProperty("LastUpdatedByNavigation")]
        public virtual ICollection<Tender> TenderLastUpdatedByNavigation { get; set; }
    }
}
