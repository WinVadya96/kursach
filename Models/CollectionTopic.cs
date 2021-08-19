using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kursach.Models
{
    public class CollectionTopic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Collection> Collections { get; set; }
    }
}