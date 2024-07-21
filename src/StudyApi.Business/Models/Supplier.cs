namespace StudyApi.Business.Models;
public class Supplier : Entity
{
    public string Name { get; set; }
    public string Document { get; set; }
    public SupplierType SupplierType { get; set; }
    public Adress Adress { get; set; }
    public bool Ativo { get; set; }

    /* EF Relations */
    public IEnumerable<Product> Products { get; set; } = [];
}
