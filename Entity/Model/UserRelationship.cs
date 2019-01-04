using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;

namespace Entity.Model
{
    public class UserRelationShip : BaseEntity
    {
        public bool IsFriend { get; set; }
        public bool Ignored { get; set; }
        public bool IsBlock { get; set; }
        public string CurrentUserId { get; set; }
        [ForeignKey("CurrentUserId")]
        public virtual User CurrentUser { get; set; }
        public string OtherUserId { get; set; }
        [ForeignKey("OtherUserId")]
        public virtual User OtherUser{ get; set; }
    }
}
