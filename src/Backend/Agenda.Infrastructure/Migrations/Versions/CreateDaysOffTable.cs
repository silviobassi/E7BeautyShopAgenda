using FluentMigrator;

namespace Agenda.Infrastructure.Migrations.Versions;

[Migration(0000004, "Create days off table")]
public class CreateDaysOffTable : VersionBase
{
    public override void Up()
    {
        CreateTable("DaysOff")
            .WithColumn("DayOnWeek").AsInt32().NotNullable()
            .WithColumn("SchedulerId").AsInt64().NotNullable().ForeignKey("Schedulers", "Id");
    }
}