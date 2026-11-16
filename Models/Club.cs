using System.ComponentModel.DataAnnotations;

namespace KampusPortal.Models
{
    public enum ClubCategory
    {
        Spor, Sanat, Teknoloji, Sosyal
    }

    public class Club
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kulüp adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Kulüp adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ClubCategory Category { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
}