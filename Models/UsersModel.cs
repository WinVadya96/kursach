using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursach.Models
{
    public class UsersModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdminIn { get; set; }
    }
}