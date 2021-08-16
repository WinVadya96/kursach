using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kursach.Models
{
    public class CollectionTopic
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public string TopicId { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual Collection Collection { get; set; }
    }
}