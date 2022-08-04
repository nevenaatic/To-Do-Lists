using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore;

namespace ToDoInfrastructure.Configuration
{
    public class SharedListConfiguration : IEntityTypeConfiguration<SharedList>
    {
        public void Configure(EntityTypeBuilder<SharedList> builder)
        {
            builder.HasKey(x => x.GeneratedLink);
            builder.Property(x => x.ValidTime)
                .IsRequired();
            builder.Property(x => x.ListId)
                .IsRequired();
        }
    }
}
