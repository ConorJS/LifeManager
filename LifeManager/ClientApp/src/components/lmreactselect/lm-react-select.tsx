import React, {FunctionComponent, useState} from "react";
import Select, {OptionsType, ValueType} from "react-select";
import chroma, {Color} from "chroma-js";
import {ReactSelectTools} from "../../tools/react-select-tools";

interface LmReactSelectProps {
    options: LmReactSelectOptions[];
    valueChanged: (value: string) => void
}

export class LmReactSelectOptions {
    value: string;

    color: Color;

    constructor(value: string, color: Color) {
        this.value = value;
        this.color = color;
    }
}

export const LmReactSelect: FunctionComponent<LmReactSelectProps> = (props: LmReactSelectProps) => {
    type OptionType = { value: string, label: string, color: Color };

    // noinspection JSMismatchedCollectionQueryUpdate - eventually casting to react-select type, starting from rudimentary type.
    let tempOptions: OptionType[] = [];
    props.options.forEach(option => {
        tempOptions.push({
            value: option.value,
            label: option.value,
            color: option.color
        });
    })
    const options = tempOptions as OptionsType<OptionType>;

    const dot = (color = '#ccc') => ({
        alignItems: 'center',
        display: 'flex',

        ':before': {
            backgroundColor: color,
            borderRadius: 10,
            content: '" "',
            display: 'block',
            marginRight: 8,
            height: 10,
            width: 10,
        },
    });

    const colourStyles = {
        control: (styles: any) => ({...styles, backgroundColor: 'white'}),
        option: (styles: { [x: string]: any; }, {data, isDisabled, isFocused, isSelected}: any) => {
            return {
                ...styles,
                backgroundColor: isDisabled
                    ? null
                    : isSelected
                        ? data.color.css()
                        : isFocused
                            ? data.color.alpha(0.1).css()
                            : null,
                color: isDisabled
                    ? '#ccc'
                    : isSelected
                        ? chroma.contrast(data.color, 'white') > 2
                            ? 'white'
                            : 'black'
                        : data.color.css(),
                cursor: isDisabled ? 'not-allowed' : 'default',

                ':active': {
                    ...styles[':active'],
                    backgroundColor:
                        !isDisabled && (isSelected ? data.color.css() : data.color.alpha(0.3).css()),
                },
            };
        },
        input: (styles: any) => ({...styles, ...dot()}),
        placeholder: (styles: any) => ({...styles, ...dot()}),
        singleValue: (styles: any, {data}: any) => ({...styles, ...dot(data.color.css())}),
    };

    const [selectedOption, setSelectedOption] = useState(options[0]);

    const handleSelect = (selectedOption: ValueType<OptionType, false>) => {
        // ValueType<OptionType> has a union type (but should always resolve to OptionType for single-select, ReadonlyArray<OptionType> for multi).
        const option: OptionType = ReactSelectTools.resolve(selectedOption)[0];
        props.valueChanged(option.value);
        setSelectedOption(option);
    }

    return (
        <React.Fragment>
            <Select
                name="selectId"
                options={options}
                onChange={handleSelect}
                value={selectedOption}
                styles={colourStyles}
            />
        </React.Fragment>
    )
}