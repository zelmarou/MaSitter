using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSitter.Models
{
    public class PersonalSpaceModel
    {
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public Guid user_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Text { get; set; }
        public float Price { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
