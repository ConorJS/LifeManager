import React, {FunctionComponent, useEffect, useState} from "react";
import {StateTools} from "../../tools/state-tools";
import {Typeahead} from 'react-bootstrap-typeahead';

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
        <Typeahead
            options={["Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"]}></Typeahead>
    )
}