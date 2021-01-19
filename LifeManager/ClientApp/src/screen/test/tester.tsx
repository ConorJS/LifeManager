import React, {Fragment, FunctionComponent, useState} from "react";
import chroma from 'chroma-js';
import {AppConstants} from "../../app-constants";
import {LmReactSelect, LmReactSelectOptions} from "../../components/lmreactselect/lm-react-select";

export const Tester: FunctionComponent = () => {
    const [selectedOption, setSelectedOption] = useState('Ready');
    
    function valueChanged(value: string) {
        setSelectedOption(value);
    } 
    
    return (
        <Fragment>
            <LmReactSelect
                options={[
                    new LmReactSelectOptions('Ready', chroma(AppConstants.LM_RED)),
                    new LmReactSelectOptions('In Progress', chroma("#ecd50b")),
                    new LmReactSelectOptions('Complete', chroma(AppConstants.LM_GREEN)),
                    new LmReactSelectOptions('Cancelled', chroma('#888888')),
                ]}
                valueChanged={valueChanged}
                selection={selectedOption}
            />
            
            {selectedOption}
        </Fragment>
    )
}