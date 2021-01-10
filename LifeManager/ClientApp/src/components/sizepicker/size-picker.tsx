import React, {FunctionComponent, useState} from "react";
import './size-picker.scss';
import {ObjectTools} from "../../tools/object-tools";

//== constants ====================================================================================================

const sizes: Map<number, string> = new Map([
    [0, 'S'],
    [1, 'M'],
    [2, 'L'],
    [3, 'XL'],
    [4, '2XL'],
    [5, '3XL'],
    [6, '4XL']
]);

interface SizePickerProps {
    initialSize: number;
    sizeSelected: (size: number) => void;
}

export const SizePicker: FunctionComponent<SizePickerProps> = (props: SizePickerProps) => {
    //== attributes ===================================================================================================

    const [activeButtonSize, setActiveButton] = useState(SizePickerTools.sizeStringFromNumber(props.initialSize));

    //== methods ======================================================================================================

    function toggleActive(sizeString: string): void {
        setActiveButton(sizeString);
    }

    function button(sizeString: string): React.ReactFragment {
        return (
            <div className={`size-picker-button ${sizeString === activeButtonSize ? "active" : ""}`}
                 key={'size-picker-button-' + sizeString}
                 onClick={() => {
                     toggleActive(sizeString);
                     props.sizeSelected(SizePickerTools.sizeNumberFromString(sizeString));
                 }}>

                <p className="button-text">
                    {sizeString}
                </p>
            </div>
        );
    }

    //== render =======================================================================================================

    let sizeButtons: React.ReactFragment[] = [];
    sizes.forEach((sizeString) => sizeButtons.push(button(sizeString)));

    return (
        <div className="size-picker-container">
            {sizeButtons}
        </div>
    );
}

export class SizePickerTools {
    public static sizeStringFromNumber(findSizeNumber: number): string {
        return ObjectTools.assignOrThrow(sizes.get(findSizeNumber), `No size string mapped to size value: ${findSizeNumber}`);
    }

    public static sizeNumberFromString(findSizeString: string): number {
        let foundSizeNumber = -1;

        sizes.forEach((sizeString, sizeNumber) => {
            if (sizeString === findSizeString) {
                foundSizeNumber = sizeNumber;
            }
        });

        if (foundSizeNumber === -1) {
            throw Error(`Could not find number for size string ${findSizeString}`);
        }

        return foundSizeNumber;
    }
}