import {ValueType} from "react-select";

export class ReactSelectTools {

    /**
     * Resolves a ValueType into an array of OptionType.
     * @param value The ValueType to resolve.
     */
    public static resolve<T>(value: ValueType<T, false>): T[] {
        let optionTypes: T[];
        if (value == null) {
            optionTypes = [];
        } else if (Array.isArray(value)) {
            optionTypes = value;
        } else {
            optionTypes = [value];
        }
        return optionTypes;
    }
}