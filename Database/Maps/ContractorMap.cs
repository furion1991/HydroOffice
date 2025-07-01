using FluentNHibernate.Mapping;
using HydroOffice.Database.Models;

namespace HydroOffice.Database.Maps;

public class ContractorMap : ClassMap<Contractor>
{
    public ContractorMap()
    {
        Table("contractors");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Not.Nullable().Length(100);
        Map(x => x.INN).Not.Nullable().Length(12);
        References(x => x.Curator)
            .Column("curator_id")
            .Nullable()
            .Cascade.None();
    }
}