import React, {Fragment, FunctionComponent, ReactElement, SyntheticEvent, useState} from "react";
import './lm-input.scss';
import {StringTools} from "../../tools/string-tools";

interface LmInputProps {
    id: string;
    value: string;
    maxLength?: number;
    onChange: (event: any) => void
    useTextArea?: boolean;
    label?: string;
}

export const LmInput: FunctionComponent<LmInputProps> = (props: LmInputProps) => {
    const [tempValue, setTempValue] = useState("" + props.value);

    /**
     * Internal change handler. We only call the supplied change handler when the field is unfocused, to minimise re-render work.
     *
     * @param event The change event (fired whenever the contents of the input field changes).
     */
    function onChangeHandler(event: SyntheticEvent): void {
        const target: HTMLInputElement = event.target as HTMLInputElement;
        const value: string = target.value;
        setTempValue(value);
    }

    let headerChildren: ReactElement[] = [];
    const baseKey: string = `${props.label ?? "NoLabelInput@" + StringTools.generateId().toString()}-input`;

    if (props.label) {
        const labelKey: string = `${baseKey}-label`;
        headerChildren.push(<label id={labelKey} key={labelKey} htmlFor={props.id}>{props.label}</label>);
    }

    if (props.maxLength && tempValue.length > props.maxLength * 0.85) {
        const warningKey: string = `${baseKey}-exceed-limit-warning`;
        headerChildren.push(
            <span id={warningKey} key={warningKey} className="input-limit-warning">
                {tempValue.length}/{props.maxLength}
            </span>
        );
    }

    return (
        <Fragment>
            <div className="label-with-warning-container">
                {headerChildren}
            </div>

            {props.useTextArea
                ?
                <textarea id={props.id}
                          typeof="string"
                          value={tempValue}
                          rows={4}
                          maxLength={props.maxLength ?? undefined}
                          onBlur={props.onChange}
                          onChange={onChangeHandler}/>
                :
                <input id={props.id}
                       type="string"
                       value={tempValue}
                       maxLength={props.maxLength ?? undefined}
                       onBlur={props.onChange}
                       onChange={onChangeHandler}/>
            }
        </Fragment>
    );
}