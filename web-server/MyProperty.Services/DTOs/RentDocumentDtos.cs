using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProperty.Services.DTOs
{
    public class RentDocumentDto
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }
    }
}
