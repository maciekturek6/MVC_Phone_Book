using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phone_book.Models
{
    public class PersonModel
    {
        [Key]
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Imię")]
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        [MinLength(3)]
        [MaxLength(255)]
        public string LastName { get; set; }

        [DisplayName("Numer telefonu")]
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string Phone { get; set; }

        [DisplayName("Adrs e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime Created { get; set; }

        [DisplayName("Data modyfikacji")]
        public DateTime Updated { get; set; }

        public PersonModel()
        {
            var now = DateTime.Now;
            Created = now;
            Updated = now;
        }
    }
}
