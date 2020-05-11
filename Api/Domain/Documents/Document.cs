using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Documents
{
    public class Document
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}