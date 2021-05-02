import React, {FunctionComponent} from "react";

import CheckBoxIcon from '@material-ui/icons/CheckBox';
import CheckBoxOutlineBlankIcon from '@material-ui/icons/CheckBoxOutlineBlank';
import CloseIcon from '@material-ui/icons/Close';
import {ElementTools} from "../../tools/element-tools";
import {StringTools} from "../../tools/string-tools";
import './settings-widget.scss';

export class Setting {
    settingName: string;

    settingValue: boolean;

    constructor(settingName: string, settingValue: boolean) {
        this.settingName = settingName;
        this.settingValue = settingValue;
    }
}

interface SettingsWidgetProps {
    options: Setting[];

    optionChanged: (option: string, setTo: boolean) => void;

    widgetClosed: () => void;
}

export const SettingsWidget: FunctionComponent<SettingsWidgetProps> = (props: SettingsWidgetProps) => {
    let listToCheckDuplicates: string[] = [];
    let optionElements: JSX.Element[] = [];

    props.options.forEach(option => {
        if (listToCheckDuplicates.includes(option.settingName)) {
            throw Error(`Passed setting: '${option.settingName}' to SettingsWidget more than once. Setting names must not be duplicated, per widget.`);
        }
        listToCheckDuplicates.push(option.settingName);

        const settingsRowKey: string = ElementTools.makeListElementId("SettingsWidget", StringTools.generateId().toString(), option.settingName);

        let checkBox: JSX.Element = option.settingValue ? <CheckBoxIcon/> : <CheckBoxOutlineBlankIcon/>

        optionElements.push(
            <React.Fragment key={settingsRowKey}>
                <div className="settings-option-row"
                     onClick={() => props.optionChanged(option.settingName, !option.settingValue)}>

                    <div className="settings-row-check-box">{option.settingValue}
                        {checkBox}
                    </div>

                    <div className="settings-row-label">
                        {option.settingName}
                    </div>
                </div>
            </React.Fragment>
        )
    })

    return (
        <div className="settings-widget lm-shadowed" onClick={(event: React.MouseEvent) => event.stopPropagation()}>
            <div className="settings-widget-header">
                <div>
                    <span>Settings</span>
                </div>

                <CloseIcon className="close-settings-widget-button" onClick={props.widgetClosed}/>
            </div>

            <div className="settings-widget-container">
                {optionElements}
            </div>
        </div>
    )
}