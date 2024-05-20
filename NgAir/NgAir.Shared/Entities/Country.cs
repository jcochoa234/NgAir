using NgAir.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Country")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; } = null!;

        public ICollection<State>? States { get; set; }

        [Display(Name = "States")]
        public int StatesNumber => States == null || States.Count == 0 ? 0 : States.Count;
    }
}
