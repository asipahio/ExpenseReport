import { Pipe, PipeTransform } from '@angular/core';
import { sortBy } from "underscore";

@Pipe({
    name: 'sortBy',
    pure: false
})
export class SortByPipe implements PipeTransform {

    transform(value: any, args?: any, isReverse?: boolean): any {
        if (value == null) { return value; }
        if (args == null) { return value; }
        if (typeof value != "object") { return value; }

        return isReverse ? sortBy(value, args).reverse() : sortBy(value, args);
    }

}
;
