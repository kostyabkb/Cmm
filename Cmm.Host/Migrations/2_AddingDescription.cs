using System;
using FluentMigrator;

namespace Cmm.Host.Migrations
{
    /// <inheritdoc/>
    [Migration(2)]
    public class AddingEventsDescriptionTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("device_event")
                .WithColumn("name").AsString(50).NotNullable().PrimaryKey()
                .WithColumn("description").AsString(1000).Nullable();

            Execute.Sql("INSERT INTO public.device_event (name) SELECT DISTINCT name FROM public.events");
        }
    }
}
