using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach.Models
{
    public class UserCollection
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set;}

        [Key]
        [Column(Order = 2)]
        public int CollectionId { get; set; }
        public virtual Collection Collection { get; set; }
    }
}