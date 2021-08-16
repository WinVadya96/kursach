using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kursach.Models
{
    public class UserCollection
    {
        [Key]
        [Column(Order=1)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public string NameId { get; set; }

        public virtual ApplicationUser User { get; set;}
        public virtual Collection Collection { get; set; }
    }
}