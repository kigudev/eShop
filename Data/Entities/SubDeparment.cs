namespace Data.Entities;

public class Subdeparment
{
    public string Name { get; private set; }
    public Deparment Deparment { get; set; }
    public List<Product> Products { get; set; }

    public Subdeparment(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("El nombre no puede ser vacio");
        
        Name = name;
        Products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public override string ToString()
    {
        return Name;
    }
}