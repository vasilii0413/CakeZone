using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeZone.EntityFramework.Entities
{
    [Table("Filling")]
    public class FillingModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte FillingId { get; set; }
        public string? FillingType {  get; set; }
        public string? FillingDescription {  get; set; }

        //public ICollection<CakeModel> Cakes { get; set; }
        
    }
}
