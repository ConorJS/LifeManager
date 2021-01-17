import React, {FunctionComponent, useState} from "react";
import {SwitchingDropdown} from "../../components/switchingdropdown/switching-dropdown";

export const Tester: FunctionComponent = () => {
    const [selectedOption, setSelectedOption] = useState('Ready');

    return (
        <React.Fragment>
            <SwitchingDropdown options={['Ready', 'InProgress', 'Complete', 'Cancelled']}
                               selection={selectedOption}
                               selectionUpdated={setSelectedOption}/>

            <div>
                {selectedOption}
            </div>

        </React.Fragment>
    )
}