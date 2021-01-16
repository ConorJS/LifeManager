import React, {FunctionComponent, useState} from "react";
import {Dropdown} from "react-bootstrap";

interface SwitchingDropdownProps {
    options: string[];
    initialSelection: string;
    selectionUpdated: (option: string) => void;
}

export const SwitchingDropdown: FunctionComponent<SwitchingDropdownProps> = (props: SwitchingDropdownProps) => {
    const [selection, setSelection] = useState(props.initialSelection);

    if (props.options.indexOf(props.initialSelection) === -1) {
        throw Error(`initialSelection ${props.initialSelection} is not a member of options: ${props.options}`);
    }
    
    const optionClicked = (option: string) => {
        setSelection(option);
        props.selectionUpdated(option);
    }

    let dropdownItems: any[] = [];
    props.options.forEach((option) => {
        dropdownItems.push(<Dropdown.Item onClick={() => optionClicked(option)}>{option}</Dropdown.Item>);
    });

    return (
        <Dropdown>
            <Dropdown.Toggle variant="success" id="dropdown-basic">
                {selection}
            </Dropdown.Toggle>

            <Dropdown.Menu>
                {dropdownItems}
            </Dropdown.Menu>
        </Dropdown>
    )
}  