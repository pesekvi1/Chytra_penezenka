using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace DataAccess.Migrations
{
    [Migration(3)]
    public class AddValueToLogTable : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Log").Column("value").Exists())
            {
                Alter.Table("Log")
                    .AddColumn("value").AsString(255);
            }
        }

        public override void Down()
        {
            Delete.Column("value")
                .FromTable("Log");
        }
    }
}
