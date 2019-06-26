using System;
using FluentMigrator;

namespace Cmm.Host.Migrations
{
    /// <inheritdoc/>
    [Migration(3)]
    public class ChangeTableNames : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE events RENAME TO events_temp");
            Execute.Sql("ALTER TABLE device_event RENAME TO events");
            Execute.Sql("ALTER TABLE events_temp RENAME TO device_event");
        }
    }
}
