import React, {FunctionComponent, useState} from "react";
import {SizePicker, SizePickerTools} from "../../components/sizepicker/size-picker";

export const Tester: FunctionComponent = () => {
    const [selectedSize, setSelectedSize] = useState('N/A');

    function sizeChosenHandler(size: number): void {
        setSelectedSize(SizePickerTools.sizeStringFromNumber(size));
    }

    return (
        <React.Fragment>
            <SizePicker initialSize={1} sizeSelected={sizeChosenHandler}/>

            <div>
                {selectedSize}
            </div>
        </React.Fragment>
    )
}