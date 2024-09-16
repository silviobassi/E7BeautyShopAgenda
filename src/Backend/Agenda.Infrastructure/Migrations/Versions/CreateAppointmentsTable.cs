using FluentMigrator;

namespace Agenda.Infrastructure.Migrations.Versions;

[Migration(0000003, "Create appointments table")]
public class CreateAppointmentsTable : VersionBase
{
    public override void Up()
    {
        CreateTable("Appointments")
            .WithColumn("AppointmentHour").AsDateTimeOffset().NotNullable()
            .WithColumn("Available").AsBoolean().NotNullable()
            .WithColumn("Duration").AsInt32().NotNullable()
            .WithColumn("ClientId").AsInt64().Nullable()
            .WithColumn("CatalogId").AsInt64().Nullable().ForeignKey("Catalogs", "Id")
            .WithColumn("SchedulerId").AsInt64().NotNullable().ForeignKey("Schedulers", "Id");
    }
}