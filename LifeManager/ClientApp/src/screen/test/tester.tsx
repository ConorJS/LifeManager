import React, {FunctionComponent, useState} from "react";
import {SizePicker} from "../../components/sizepicker/size-picker";

export const Tester: FunctionComponent = () => {
    const [selectedSize, setSelectedSize] = useState('N/A');

    function sizeChosen(size: string) {
        setSelectedSize(size);
    }

    return (
        <React.Fragment>
            <SizePicker sizeSelected={sizeChosen}/>

            <div>
                {selectedSize}
            </div>
        </React.Fragment>
    )
}