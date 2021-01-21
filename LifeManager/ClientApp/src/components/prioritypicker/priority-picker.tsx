import React, {FunctionComponent, useState} from "react";
import './priority-picker.scss';
import {ObjectTools} from "../../tools/object-tools";

//== constants ====================================================================================================

const priorities: Map<number, string> = new Map([
    [0, 'I'],
    [1, 'II'],
    [2, 'III'],
    [3, 'IV']
]);

const buttonClassNames: Map<number, string> = new Map([
    [0, 'priority-one-button'],
    [1, 'priority-two-button'],
    [2, 'priority-three-button'],
    [3, 'priority-four-button']
]);

interface PriorityPickerProps {
    initialPriority: number;
    prioritySelected: (priority: number) => void;
}

export const PriorityPicker: FunctionComponent<PriorityPickerProps> = (props: PriorityPickerProps) => {
    //== attributes ===================================================================================================

    const [activeButtonPriority, setActiveButton] = useState(PriorityPickerTools.priorityStringFromNumber(props.initialPriority));

    //== methods ======================================================================================================

    function toggleActive(priorityString: string): void {
        setActiveButton(priorityString);
    }

    function button(ordinal: number): React.ReactFragment {
        let priorityString: string = ObjectTools.getOrFail(priorities, ordinal);

        return (
            <div className={`priority-picker-button 
                    ${ObjectTools.getOrFail(buttonClassNames, ordinal)} 
                    ${priorityString === activeButtonPriority ? "active" : ""}`}
                 key={'priority-picker-button-' + priorityString}
                 onClick={() => {
                     toggleActive(priorityString);
                     props.prioritySelected(PriorityPickerTools.priorityNumberFromString(priorityString));
                 }}>

                <p className="button-text">
                    {priorityString}
                </p>
            </div>
        );
    }

    //== render =======================================================================================================

    let priorityButtons: React.ReactFragment[] = [];
    priorities.forEach((priorityString, ordinal) => priorityButtons.push(button(ordinal)));

    return (
        <div className="priority-picker-container">
            {priorityButtons}
        </div>
    );
}

export class PriorityPickerTools {
    public static priorityStringFromNumber(findPriorityNumber: number): string {
        return ObjectTools.assignOrThrow(priorities.get(findPriorityNumber), `No priority string mapped to priority value: ${findPriorityNumber}`);
    }

    public static priorityNumberFromString(findPriorityString: string): number {
        return ObjectTools.firstKeyWithValue(findPriorityString, priorities);
    }
}