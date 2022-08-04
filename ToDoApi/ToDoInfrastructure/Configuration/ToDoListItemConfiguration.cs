using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoCore;

namespace ToDoInfrastructure.Configuration
{
    public class ToDoListItemConfiguration : IEntityTypeConfiguration<ToDoListItem>
    {
        public void Configure(EntityTypeBuilder<ToDoListItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(i => i.ToDoList)
                .WithMany(l => l.Items)
                .HasForeignKey(i => i.ToDoListId);
        }
    }
}
