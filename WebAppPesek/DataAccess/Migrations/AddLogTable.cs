using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(1)]
    public class AddLogTable : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Log").Exists())
            {
                Create.Table("Log")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Text").AsString();
            }
        }

        public override void Down()
        {
            Delete.Table("Log");
        }
    }
}
