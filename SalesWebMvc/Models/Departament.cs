using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Departament
    {
        [Display (Name = "Identificador")]
        public int Id { get; set; }

        [Display (Name = "Nome") ]
        public string Name { get; set; }
    }
}
