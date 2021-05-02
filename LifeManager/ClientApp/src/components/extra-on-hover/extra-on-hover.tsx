import React, {FunctionComponent} from "react";

import './extra-on-hover.scss';

interface ExtraOnHoverProps {
    always: JSX.Element;
    extra?: JSX.Element;
}

export const ExtraOnHover: FunctionComponent<ExtraOnHoverProps> = (props: ExtraOnHoverProps) => {
    let extraContainer;
    if (props.extra) {
        extraContainer =
            <div className="extra" style={{overflow: 'hidden'}}>
                {props.extra}
            </div>
    }

    return (
        <React.Fragment>
            <div className="extra-on-hover-container">
                <div>
                    {props.always}
                </div>

                {extraContainer}
            </div>
        </React.Fragment>
    )
}