using NgAir.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class State : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "State")]
        [MaxLength(100, ErrorMessage = "The maximun length for field {0} is {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }

        [Display(Name = "Cities")]
        public int CitiesNumber => Cities == null || Cities.Count == 0 ? 0 : Cities.Count;
    }
}
