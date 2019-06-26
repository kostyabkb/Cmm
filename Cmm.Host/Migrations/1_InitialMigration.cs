using System;
using FluentMigrator;

namespace Cmm.Host.Migrations
{
    /// <inheritdoc/>
    [Migration(1)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("devices")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("os").AsString(64).NotNullable()
                .WithColumn("version").AsString(64).NotNullable();

            Create.Table("events")
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("date").AsDateTimeOffset().NotNullable()
                .WithColumn("device_id").AsGuid().NotNullable();

            Create.ForeignKey("events_device_id_fk")
                .FromTable("events").ForeignColumn("device_id")
                .ToTable("devices").PrimaryColumn("id");
        }
    }
}
