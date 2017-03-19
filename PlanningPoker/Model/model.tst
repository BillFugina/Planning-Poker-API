${ string Prop(Property p){
        var prefix = !p.Type.IsPrimitive ? "I" : "";

        return $"{p.Name}: {prefix}{p.Type.Name}";
    }

}import {IGuid} from 'model'
$Enums(PlanningPoker.Model.*)[
export enum $Name { $Values[
    $Name= $Value,]
}
]

$Classes(PlanningPoker.Model.*)[
export interface I$Name { $Properties[
    $Prop]
}
]