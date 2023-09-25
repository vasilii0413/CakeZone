using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeZone.EntityFramework.Entities
{
    [Table("Order")]
    public class OrderModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte OrderId { get; set; }
        [ForeignKey("CakeId")]
        public byte CakeId { get; set; }
        public virtual CakeModel? Cake{ get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
