using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models{

    public class TodoItem{
        public string? Id {get; set;}

        [Required]
        public string? Title {get; set;}

        public enum Priority {
            Low,
            Medium,
            High
        }
        public Priority Priority {get; set;}

        public bool IsComplete {get; set;}

        public DateTime? CreatedAt {get; set;}
        
        public DateTime? UpdatedAt {get; set;}
    }
};