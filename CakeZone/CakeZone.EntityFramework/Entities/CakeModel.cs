using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeZone.EntityFramework.Entities
{
    [Table("Cake")]
    public class CakeModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CakeId { get; set; }
        public string? CakeName { get; set; }
        public string? CakeDescription { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Weight {  get; set; }
        public string? ImageURL { get; set; }
        public byte CoverId { get;set; }
        public CoverModel Cover { get; set; }
        public byte FillingId { get; set; }
        public FillingModel Filling { get; set; }

        //public ICollection<CategoryModel> Categories { get; set; }

    }
}
