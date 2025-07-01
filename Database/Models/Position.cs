using System.ComponentModel;

namespace HydroOffice.Database.Models;

public enum Position
{
    [Description("Руководитель")]
    Manager,
    [Description("Работник")]
    Worker
}