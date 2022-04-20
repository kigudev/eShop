using Data.Entities;

namespace Business;

public static class TestData
{
    public static List<Product> ProductList = new List<Product>
    {
        new Product(1, "Silla", 100, "Una silla negra", "Sillatronic", "s1s2", 10),
        new Product(2, "Mesa", 500, "Una mesa blanca", "Sillatronic", "s1s3", 5),
        new Product(3, "TV", 500, "Una tv plana", "Teletronic", "tv123", 1),
    };

    public static List<Deparment> DeparmentList = new List<Deparment>
    {
        new Deparment("Electrónicos", new List<Subdeparment>
        {
            new Subdeparment("TVs"),
            new Subdeparment("Celulares"),
            new Subdeparment("Audio"),
        }),
        new Deparment("Muebles", new List<Subdeparment>
        {
            new Subdeparment("Cocina"),
            new Subdeparment("Comedor"),
            new Subdeparment("Sala")
        }),
        new Deparment("Alimentos", new List<Subdeparment>
        {
            new Subdeparment("Lacteos"),
            new Subdeparment("Carnes frías"),
            new Subdeparment("Pastas"),
        }),
    };

    public static List<Provider> GetProviders()
    {
        var providers = new List<Provider>();
        var p1 = new Provider(1, "Gamesa", "proveedor@gamesa.com");
        p1.AddAddress("islas 123", "mexicali");
        p1.AddPhoneNumber("6861234567");
        providers.Add(p1);
        var p2 = new Provider(2, "Levis", "proveedor@levis.com");
        p2.AddAddress("islas levis 123", "tijuana");
        p2.AddPhoneNumber("6641231656");
        providers.Add(p2);
        var p3 = new Provider(2, "Mercado Chuchita", "proveedor@chuchita.com");
        p3.AddAddress("islas chu 123", "tijuana");
        p3.AddPhoneNumber("6641231644");
        providers.Add(p3);
        
        return providers;
    }
}