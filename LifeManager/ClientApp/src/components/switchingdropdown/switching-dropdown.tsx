import React, {FunctionComponent, useState} from "react";
import {Dropdown} from "react-bootstrap";
import {StringTools} from "../../tools/string-tools";
import {ElementTools} from "../../tools/element-tools";

interface SwitchingDropdownProps {
    options: string[];
    selection: string;
    selectionUpdated: (option: string) => void;
}

export const SwitchingDropdown: FunctionComponent<SwitchingDropdownProps> = (props: SwitchingDropdownProps) => {
    if (props.options.indexOf(props.selection) === -1) {
        throw Error(`initialSelection ${props.selection} is not a member of supplied option list: ${props.options}`);
    }
    
    const optionClicked = (option: string) => {
        props.selectionUpdated(option);
    }

    let containerIdHash = StringTools.generateId();
    let dropdownItems: any[] = [];
    props.options.forEach((option, index) => {
        const dropdownId = ElementTools.makeListElementId(SwitchingDropdown.name, containerIdHash, index);
        dropdownItems.push(<Dropdown.Item id={dropdownId} key={dropdownId} onClick={() => optionClicked(option)}>{option}</Dropdown.Item>);
    });
    
    let formattedContainerId = ElementTools.makeContainerElementId(SwitchingDropdown.name, containerIdHash);

    return (
        <Dropdown id={formattedContainerId} key={formattedContainerId}>
            <Dropdown.Toggle variant="success" id="dropdown-basic">
                {props.selection}
            </Dropdown.Toggle>

            <Dropdown.Menu>
                {dropdownItems}
            </Dropdown.Menu>
        </Dropdown>
    )
}  