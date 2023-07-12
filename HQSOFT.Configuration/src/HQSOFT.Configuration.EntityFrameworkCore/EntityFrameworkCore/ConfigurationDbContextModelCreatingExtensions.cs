using HQSOFT.Configuration.CSAttributeDetails;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HQSOFT.Configuration.CSAttributes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace HQSOFT.Configuration.EntityFrameworkCore;

public static class ConfigurationDbContextModelCreatingExtensions
{
    public static void ConfigureConfiguration(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(ConfigurationDbProperties.DbTablePrefix + "Questions", ConfigurationDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CSAttributeDetail>(b =>
{
    b.ToTable(ConfigurationDbProperties.DbTablePrefix + "CSAttributeDetails", ConfigurationDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.HasIndex(x => x.ValueID).IsUnique();
    b.Property(x => x.ValueID).HasColumnName(nameof(CSAttributeDetail.ValueID)).IsRequired().HasMaxLength(CSAttributeDetailConsts.ValueIDMaxLength);
    b.Property(x => x.Description).HasColumnName(nameof(CSAttributeDetail.Description)).IsRequired().HasMaxLength(CSAttributeDetailConsts.DescriptionMaxLength);
    b.Property(x => x.SortOrder).HasColumnName(nameof(CSAttributeDetail.SortOrder));
    b.Property(x => x.Disabled).HasColumnName(nameof(CSAttributeDetail.Disabled));
    b.HasOne<CSAttribute>().WithMany().HasForeignKey(x => x.CSAttributeId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CSAttribute>(b =>
{
    b.ToTable(ConfigurationDbProperties.DbTablePrefix + "CSAttributes", ConfigurationDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.HasIndex(x => x.AttributeID).IsUnique();
    b.Property(x => x.AttributeID).HasColumnName(nameof(CSAttribute.AttributeID)).IsRequired().HasMaxLength(CSAttributeConsts.AttributeIDMaxLength);
    b.Property(x => x.Description).HasColumnName(nameof(CSAttribute.Description)).IsRequired().HasMaxLength(CSAttributeConsts.DescriptionMaxLength);
    b.Property(x => x.ControlType).HasColumnName(nameof(CSAttribute.ControlType));
    b.Property(x => x.EntryMask).HasColumnName(nameof(CSAttribute.EntryMask)).HasMaxLength(CSAttributeConsts.EntryMaskMaxLength);
    b.Property(x => x.RegExp).HasColumnName(nameof(CSAttribute.RegExp)).HasMaxLength(CSAttributeConsts.RegExpMaxLength);
    b.Property(x => x.List).HasColumnName(nameof(CSAttribute.List));
    b.Property(x => x.IsInternal).HasColumnName(nameof(CSAttribute.IsInternal));
    b.Property(x => x.ContainsPersonalData).HasColumnName(nameof(CSAttribute.ContainsPersonalData));
    b.Property(x => x.ObjectName).HasColumnName(nameof(CSAttribute.ObjectName)).HasMaxLength(CSAttributeConsts.ObjectNameMaxLength);
    b.Property(x => x.FieldName).HasColumnName(nameof(CSAttribute.FieldName)).HasMaxLength(CSAttributeConsts.FieldNameMaxLength);
});

        }
    }
}