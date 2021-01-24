import React, {FunctionComponent} from "react";
import './size-indicator.scss'
import {SizePickerTools} from "../sizepicker/size-picker";
import {ElementTools} from "../../tools/element-tools";
import {StringTools} from "../../tools/string-tools";

interface SizeIndicatorProps {
    sizeNumber: number;
    outerDimensions: number;
}

export const SizeIndicator: FunctionComponent<SizeIndicatorProps> = (props: SizeIndicatorProps) => {
    const maxSize: number = 7;
    const spacing: number = Math.floor(props.outerDimensions / 40);

    let height: number = Math.floor(((props.outerDimensions - spacing) / maxSize) - spacing);
    let width: number = Math.floor(props.outerDimensions - (spacing * 2));

    let rectangles: any[] = [];
    for (let i = maxSize - 1; i >= 0; i--) {
        const key: string = ElementTools.makeListElementId("SizeIndicator", StringTools.generateId().toString(), i);
        rectangles.push(
            <div id={key} key={key}
                 style={{width: width, height: height, marginBottom: i === 0 ? 0 : spacing, marginLeft: spacing}}
                 className={props.sizeNumber >= i ? "rectangle-on" : "rectangle-off"}/>
        )
    }

    return (
        <React.Fragment>
            <div style={{width: props.outerDimensions, height: props.outerDimensions}}>
                {rectangles}

                <span
                    className="size-text"
                    style={{
                        float: 'right',
                        marginTop: Math.floor(0 - props.outerDimensions / 2.25),
                        fontSize: Math.floor(props.outerDimensions / 3),
                        fontWeight: 'bold',
                        marginRight: Math.floor(props.outerDimensions / 16)
                    }}
                >
                    {SizePickerTools.sizeStringFromNumber(props.sizeNumber)}
                </span>
            </div>
        </React.Fragment>
    );
}