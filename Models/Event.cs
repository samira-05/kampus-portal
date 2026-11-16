using System.ComponentModel.DataAnnotations;

namespace KampusPortal.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Etkinlik başlığı zorunludur.")]
        [StringLength(150, ErrorMessage = "Etkinlik başlığı en fazla 150 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Etkinlik tarihi zorunludur.")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Etkinlik yeri zorunludur.")]
        [StringLength(100, ErrorMessage = "Etkinlik yeri en fazla 100 karakter olabilir")]
        public string Location { get; set; } = string.Empty;

        public int ClubId { get; set; }

        public Club? Club { get; set; }

    }
}

