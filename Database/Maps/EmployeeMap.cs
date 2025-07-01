using FluentNHibernate.Mapping;
using HydroOffice.Database.Models;

namespace HydroOffice.Database.Maps;

public class EmployeeMap : ClassMap<Employee>
{
    public EmployeeMap()
    {
        Table("employees");

        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.FullName).Not.Nullable().Length(100);
        Map(x => x.Position)
            .CustomType<Position>()
            .Not.Nullable();
        Map(x => x.DateOfBirth);
    }
}