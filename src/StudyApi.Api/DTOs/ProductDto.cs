using System.ComponentModel.DataAnnotations;

namespace StudyApi.Api.DTOs;

public class ProductDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(200, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(1000, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string Description { get; set; }
    public string ImageUpload { get; set; }
    public string Image { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    public decimal Value { get; set; }

    [ScaffoldColumn(false)]
    public DateTime RegistrationDate { get; set; }
    public bool Active { get; set; }

    [ScaffoldColumn(false)]
    public string SupplierName { get; set; }
}
