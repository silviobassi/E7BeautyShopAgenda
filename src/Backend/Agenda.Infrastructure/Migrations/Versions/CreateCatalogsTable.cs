using FluentMigrator;

namespace Agenda.Infrastructure.Migrations.Versions;

[Migration(0000002, "Create catalogs table")]
public class CreateCatalogsTable : VersionBase
{
    public override void Up()
    {
        CreateTable("Catalogs")
            .WithColumn("DescriptionName").AsString().NotNullable()
            .WithColumn("DescriptionPrice").AsDecimal().NotNullable();
    }
}