using Business.Services.Abstractions;
using Data.Entities;

namespace Business.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private List<Deparment> DeparmentList = TestData.DeparmentList;

    public List<Deparment> GetDeparments()
    {
        return DeparmentList;
    }
    
    public List<Subdeparment> GetSubdeparments(string name)
    {
        var deparment = DeparmentList.FirstOrDefault(c => c.Name == name);
        
        if(deparment != null)
            return deparment.Subdeparments;

        return new List<Subdeparment>();
    }
}