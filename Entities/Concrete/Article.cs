using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Article : IEntity
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public int UserID { get; set; }
        public int? CommentID { get; set; }
        public string Content { get; set; }
        public DateTime SharingDate { get; set; }
    }
}
