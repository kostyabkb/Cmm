using System;
using Cmm.Host.Model;
using FluentMigrator;

namespace Cmm.Host.Migrations
{
    /// <inheritdoc/>
    [Migration(4)]
    public class AddingLevel : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("events")
                .AddColumn("level")
                .AsInt16()
                .WithDefaultValue((int)EventLevel.Undefined);
        }
    }
}
