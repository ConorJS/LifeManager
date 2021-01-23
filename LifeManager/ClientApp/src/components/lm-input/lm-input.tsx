import React, {Fragment, FunctionComponent, ReactElement} from "react";
import './lm-input.scss';

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

    if (props.label) {
        headerChildren.push(<label htmlFor={props.id}>{props.label}</label>);
    }

    if (props.maxLength && props.value.length > props.maxLength * 0.85) {
        headerChildren.push(
            <span className="input-limit-warning">
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