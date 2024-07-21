namespace StudyApi.Business.Models;
public class Adress : Entity
{
    public Guid SupplierId { get; set; }
    public string PublicArea { get; set; }
    public string Number { get; set; }
    public string AdditionalAdressDetails { get; set; }
    public string ZipCode { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    /* EF Relation */
    public Supplier Supplier { get; set; }
}
