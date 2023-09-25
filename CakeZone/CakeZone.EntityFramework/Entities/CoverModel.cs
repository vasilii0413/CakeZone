using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeZone.EntityFramework.Entities
{
    [Table("Cover")]
    public class CoverModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CoverId { get;set; }
        public string? CoverName { get; set; }
        public string? CoverDescription {  get; set; }

        //public ICollection<CakeModel>Cakes { get; set; }

    }
}
