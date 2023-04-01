using System.ComponentModel.DataAnnotations;

namespace Parcial2_CarmonaSantiago.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "¿Boleta usada?")]
        public bool IsUsed { get; set; }

        [Display(Name = "Portería de ingreso")]
        public string? EntranceGate { get; set; }
    }
}
