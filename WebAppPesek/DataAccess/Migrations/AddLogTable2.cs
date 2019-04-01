using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(2)]
    public class AddLogTable2 : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Log2").Exists())
            {
                Create.Table("Log2")
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Text").AsString();
            }
        }

        public override void Down()
        {
            Delete.Table("Log2");
        }
    }
}
