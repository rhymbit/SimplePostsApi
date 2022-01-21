using System.ComponentModel.DataAnnotations.Schema;

namespace PostsApiDatabase
{
    [Table("posts")]
    public class Post
    {
        [Column("id")]
        public string? Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("added")]
        public DateTime? Added { get; set; }
    }
}
