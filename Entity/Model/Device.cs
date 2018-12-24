using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;

namespace Entity.Model
{
    public class Device : BaseEntity
    {
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
