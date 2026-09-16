#set( $className = $classObject.getName() )
#set( $lowercaseClassName = $Utils.lowercaseFirstLetter( $className ) )
using ${className}.Api.Domain;
using ${className}.Api.Domain.Enums;
using ${className}.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ${className}.Persistence;

public class ${className}Configuration : IEntityTypeConfiguration<${className}>
{
    public void Configure(EntityTypeBuilder<${className}> builder)
    {
        builder.ToTable("${lowercaseClassName}s");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

#set( $includeHierarchy = false )
#set( $includePKs = false )
#set( $attributes = $classObject.getAttributesOnly( $includeHierarchy, $includePKs ) )
#foreach( $attribute in $attributes )
#set( $type = $attribute.getType() )
#set( $attributeName = $attribute.getName() )
#set( $attributeName = $Utils.capitalizeFirstLetter( $attributeName ) )
#if ( $hm.isValueObject( $type ) )
builder.OwnsOne(x => x.${type}, ${attributeName} =>
{
#set( $valueObject = $hm.getValueObject( $type ) )
#set( $voAttributes = $valueObject.getAttributesOnly( $includeHierarchy, $includePKs )  )
#foreach( $voAttribute in $voAttributes )
#set( $voAttributeName = $voAttribute.getName() )
    ${attributeName}.Property(x => x.${Utils.capitalizeFirstLetter( $voAttributeName )}).HasColumnName("${attributeName}_${voAttributeName}");
#end
});
#elseif( $attribute.isFromEnumerator() == true )
        builder.Property(x => x.${type}).HasConversion<string>();
#else
        builder.Property(x => x.${attributeName});
#end

#foreach( $singleAssociation = $singleAssociations )
#set( $type = $singleAssociation.getType() )
        builder.Property(x => x.${type}Id).IsRequired();
        // Exactly one $type per $className (1:1)
        builder.HasIndex(x => x.${type}Id).IsUnique();
#end
    }
}
