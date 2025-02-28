namespace Web_Presentation.Models
{
    public class Comment
    {
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
