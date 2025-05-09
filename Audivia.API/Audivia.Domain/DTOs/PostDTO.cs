
namespace Audivia.Domain.DTOs
{
    public class PostDTO
    {
        public string Id { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string[]? Images { get; set; }

        public string? Content { get; set; }

        public string? Location { get; set; }

        public UserShortDTO? User { get; set; }

        public int? Likes { get; set; }
        public int? Comments { get; set; }
        public string? Time { get; set; } // 2 gio, 3 ngay, ...
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
