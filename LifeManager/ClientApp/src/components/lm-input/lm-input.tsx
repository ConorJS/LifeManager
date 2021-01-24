import React, {Fragment, FunctionComponent, ReactElement} from "react";
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
    let headerChildren: ReactElement[] = [];

    const baseKey: string = `${props.label ?? "NoLabelInput@" + StringTools.generateId().toString()}-input`;
    
    if (props.label) {
        const labelKey: string = `${baseKey}-label`;
        headerChildren.push(<label id={labelKey} key={labelKey} htmlFor={props.id}>{props.label}</label>);
    }

    if (props.maxLength && props.value.length > props.maxLength * 0.85) {
        const warningKey: string = `${baseKey}-exceed-limit-warning`;
        headerChildren.push(
            <span id={warningKey} key={warningKey} className="input-limit-warning">
                {props.value.length}/{props.maxLength}
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
                          value={props.value}
                          rows={4}
                          maxLength={props.maxLength ?? undefined}
                          onChange={props.onChange}/>
                :
                <input id={props.id}
                       type="string"
                       value={props.value}
                       maxLength={props.maxLength ?? undefined}
                       onChange={props.onChange}/>
            }
        </Fragment>
    );
}