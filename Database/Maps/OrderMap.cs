using FluentNHibernate.Mapping;
using HydroOffice.Database.Models;

namespace HydroOffice.Database.Maps;

public class OrderMap : ClassMap<Order>
{
    public OrderMap()
    {
        Table("orders");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Date).Not.Nullable();
        Map(x => x.Amount).Not.Nullable().Precision(18).Scale(2);

        References(x => x.Employee)
            .Column("employee_id")
            .Nullable()
            .ForeignKey("fk_order_contractor")
            .Cascade.None();

        References(x => x.Contractor)
            .Column("contractor_id")
            .Nullable()
            .Cascade.None();
    }
}