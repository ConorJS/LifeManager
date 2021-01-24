import React, {Fragment, FunctionComponent, useState} from "react";
import {SizeIndicator} from "../../components/sizeindicator/size-indicator";

export const Tester: FunctionComponent = () => {
    const [selectedOption, setSelectedOption] = useState(1);

    function valueChanged(value: number) {
        setSelectedOption(value);
    }

    return (
        <Fragment>
            <SizeIndicator sizeNumber={6} outerDimensions={40}/>
        </Fragment>
    )
}