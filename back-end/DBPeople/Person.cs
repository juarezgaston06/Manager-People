using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPeople
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "DNI is required.")]
        [MaxLength(8, ErrorMessage = "DNI cannot be longer than 8 characters.")]
        public string? DNI { get; set; }

        public string? Phone { get; set; }

        [Required(ErrorMessage = "Date of entry is required.")]
        public DateTime EntryDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        [Required(ErrorMessage = "You must select a province.")]
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }
    }
}
