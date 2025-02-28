namespace Web_Presentation.Models
{
    public class Article
    {
        public int TopicID { get; set; }
        public int UserID { get; set; }
        public int? CommentID { get; set; }
        public string Content { get; set; }
        public DateTime SharingDate { get; set; }
    }
}
