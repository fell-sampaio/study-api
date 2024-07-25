using System.ComponentModel.DataAnnotations;

namespace StudyApi.Api.DTOs;

public class AdressDto
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(200, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string PublicArea { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(50, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 1)]
    public string Number { get; set; }

    public string AdditionalAdressDetails { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(8, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 8)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string District { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string City { get; set; }

    [Required(ErrorMessage = "{0} field must be provided")]
    [StringLength(50, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 2)]
    public string State { get; set; }

    public Guid SupplierId { get; set; }
}
