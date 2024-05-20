using NgAir.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NgAir.Shared.Entities
{
    public class City : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "City")]
        [MaxLength(100, ErrorMessage = "The maximun length for field {0} is {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; } = null!;

        public int StateId { get; set; }

        public State? State { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
