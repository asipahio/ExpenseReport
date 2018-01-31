import { Pipe, PipeTransform } from '@angular/core';
import { filter } from "underscore";
@Pipe({
    name: 'filter'
})
export class FilterPipe implements PipeTransform {

    transform(value: any, args?: string): any {
        if (value == null) { return value; }
        if (args == null) { return value; }
        if (typeof value != "object") { return value; }

        return filter(value, function (obj) {
            var isFound = false;
            for (var i in Object.keys(obj)) {
                var val = obj[Object.keys(obj)[i]];
                if (val.ExpenseCategory) {
                    if ((val.ExpenseCategory + "").toLowerCase().indexOf(args.toLowerCase()) > -1) {
                        isFound = true;
                        break;
                    }
                }
                if ((val + "").toLowerCase().indexOf(args.toLowerCase()) > -1) {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        });
    }

}
