using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPHBA.Model.ViewModels
{
    public class PropertyInfoReqVM
    {
        public long? Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string PropertyType { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [MaxLength(255)]
        public string Location { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(500)]
        public string Feature { get; set; }
        public IFormFile? Image { get; set; }
        public string? BrokerId { get; set; }
    }
}
