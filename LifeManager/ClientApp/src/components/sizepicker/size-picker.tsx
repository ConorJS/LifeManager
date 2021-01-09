import React, {FunctionComponent, useState} from "react";
import './size-picker.scss';

interface SizePickerProps {
    sizeSelected: (size: string) => void;
}

export const SizePicker: FunctionComponent<SizePickerProps> = (props: SizePickerProps) => {
    //== constants ====================================================================================================

    const sizes: string[] = ['S', 'M', 'L', 'XL', '2XL', '3XL', '4XL'];

    //== attributes ===================================================================================================

    const [activeButtonSize, setActiveButton] = useState('M');

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
                     props.sizeSelected(sizeString);
                 }}>

                <p className="button-text">
                    {sizeString}
                </p>
            </div>
        );
    }

    //== render =======================================================================================================

    let sizeButtons: React.ReactFragment[] = [];
    for (let size of sizes) {
        sizeButtons.push(button(size));
    }

    return (
        <div className="size-picker-container">
            {sizeButtons}
        </div>
    );
}