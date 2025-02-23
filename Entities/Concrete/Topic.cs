using Core.Entities;
using System.Security.Principal;

namespace Entities.Concrete
{
    public class Topic : IEntity
    {
        public int ID { get; set; }
        public string TopicTitle { get; set; }
    }
}
