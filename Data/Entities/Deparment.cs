namespace Data.Entities;

public class Deparment
{
    public string Name { get; private set; }
    public List<Subdeparment> Subdeparments { get; private set; }

    public Deparment(string name, List<Subdeparment> subdeparments)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("El nombre no puede ser vacio");

        if (subdeparments == null || !subdeparments.Any())
            throw new InvalidOperationException("El departamento necesita subdepartamentos");
        
        Name = name;
        Subdeparments = subdeparments;
    }

    public override string ToString()
    {
        return Name;
    }
}