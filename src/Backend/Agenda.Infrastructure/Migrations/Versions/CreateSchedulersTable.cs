using FluentMigrator;

namespace Agenda.Infrastructure.Migrations.Versions;

[Migration(0000001, "Create schedulers table")]
public class CreateSchedulersTable : VersionBase
{
    public override void Up()
    {
        CreateTable("Schedulers")
            .WithColumn("ProfessionalId").AsInt64().NotNullable();
    }
}