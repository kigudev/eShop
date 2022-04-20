using Data.Entities;

namespace Business.Services.Abstractions;

public interface IDepartmentService
{
    public List<Deparment> GetDeparments();
    public List<Subdeparment> GetSubdeparments(string name);
}