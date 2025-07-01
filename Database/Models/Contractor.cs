namespace HydroOffice.Database.Models;

public class Contractor
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; }

    public virtual string INN { get; set; }

    public virtual Employee? Curator { get; set; }
}