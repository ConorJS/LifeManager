import React, {Fragment, FunctionComponent, useEffect, useState} from "react";
import {SizeIndicator} from "../../components/size-indicator/size-indicator";
import {StateTools} from "../../tools/state-tools";

export class SomeType {
    someArray: Number[];
    
    someOtherValue: string;
    
    constructor(someArray: Number[], someOtherValue: string) {
        this.someArray = someArray;
        this.someOtherValue = someOtherValue;
    }
}

export const Tester: FunctionComponent = () => {
    const [someState, setSomeState] = useState(new SomeType([1, 2, 3], 'dummy'));
    
    useEffect(() => {
        console.log(someState);
    }, [someState]);
    
    
    function clickHandler() {
        console.log("Pressed");
        
        StateTools.updateArray(someState, setSomeState, 'someArray', 5, 1);
    }
    
    return (
        <Fragment>
            <button onClick={clickHandler}/>
            <SizeIndicator sizeNumber={6} outerDimensions={40}/>
        </Fragment>
    )
}