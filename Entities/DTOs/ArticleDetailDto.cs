using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ArticleDetailDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public string TopicTitle { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public string SharingDate { get; set; }
        public List<CommentDetailDto> CommentDetails { get; set; }
    }
}
