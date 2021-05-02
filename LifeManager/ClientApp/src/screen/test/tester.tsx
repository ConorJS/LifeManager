import React, {Fragment, FunctionComponent} from "react";
import {SizeIndicator} from "../../components/size-indicator/size-indicator";

export const Tester: FunctionComponent = () => {
    return (
        <Fragment>
            <SizeIndicator sizeNumber={6} outerDimensions={40}/>
        </Fragment>
    )
}