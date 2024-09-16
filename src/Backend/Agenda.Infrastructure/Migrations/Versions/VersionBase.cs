using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Agenda.Infrastructure.Migrations.Versions;

public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
    {
        return Create.Table(table)
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().Nullable();
    }
}