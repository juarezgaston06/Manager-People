using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPeople
{
    public class Province
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Province name is required.")]
        [StringLength(50, ErrorMessage = "Province name cannot be longer than 50 characters.")]
        public string? Name { get; set; }
    }
}
