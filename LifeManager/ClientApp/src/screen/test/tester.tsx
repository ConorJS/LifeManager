import React, {Fragment, FunctionComponent, useState} from "react";
import {PriorityPicker} from "../../components/prioritypicker/priority-picker";

export const Tester: FunctionComponent = () => {
    const [selectedOption, setSelectedOption] = useState(1);

    function valueChanged(value: number) {
        setSelectedOption(value);
    }

    return (
        <Fragment>
            <PriorityPicker
                initialPriority={selectedOption}
                prioritySelected={valueChanged}
            />

            {selectedOption}
        </Fragment>
    )
}