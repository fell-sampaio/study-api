using System.ComponentModel.DataAnnotations;

namespace StudyApi.Api.DTOs;

public class SupplierDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(14, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 11)]
    public string Document { get; set; }

    public int SupplierType { get; set; }
    public AdressDto Adress { get; set; }
    public bool Active { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
}
