#set( $className = $classObject.getName() )
#set( $singleAssociations = $classObject.getSingleAssociations() )
#set( $totalSingleAssociations = singleAssociations.size() )
namespace ${appName}.Api.${className};

#set( $singleAssociationsAsArgs = "" )
#foreach( $singleAssociation in $singleAssociations )
#set( $type = $singleAssociation.getType() )
#set( $lcType = $Utils.lowercaseFirstLetter( $type ) )
#set( $singleAssociationsAsArgs = "${singleAssociationsAsArgs}Guid ${type}Id" )
#if ( $velocityCount < $totalSingleAssociations )
#set( $singleAssociationsAsArgs = "${singleAssociationsAsArgs}, ")
#end
#end
public record Create${className}Request(string ${className}Number, decimal Balance, ${singleAssociationsAsArgs});
public record Update${className}Request(string ${className}Number, decimal Balance, ${singleAssociationsAsArgs});
public record ${className}Response(Guid Id, string ${className}Number, decimal Balance, ${singleAssociationsAsArgs});
