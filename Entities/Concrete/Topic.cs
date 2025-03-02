using Core.Entities;
using System.Security.Principal;

namespace Entities.Concrete
{
    public class Topic : IEntity
    {
        public int Id { get; set; }
        public string TopicTitle { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
