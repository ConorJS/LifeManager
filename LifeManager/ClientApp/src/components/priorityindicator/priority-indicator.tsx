import React, {FunctionComponent, ReactElement} from "react";
import './priority-indicator.scss';
import {ElementTools} from "../../tools/element-tools";
import {StringTools} from "../../tools/string-tools";

interface PriorityIndicatorProps {
    priorityNumber: number;
}

const imagesForPriorities: Map<number, string> = new Map([
    [0, 'PriorityMeter_Red'],
    [1, 'PriorityMeter_Orange'],
    [2, 'PriorityMeter_Yellow'],
    [3, 'PriorityMeter_Green']
]);

export const PriorityIndicator: FunctionComponent<PriorityIndicatorProps> = (props: PriorityIndicatorProps) => {
    let images: ReactElement<any, any>[] = [];

    // Need to render all images, but set all but the active one to not display, otherwise the image doesn't load fast enough when the value changes.
    imagesForPriorities.forEach((imageName: string, key: number) => {
        const elementKey: string = ElementTools.makeListElementId("PriorityIndicator", StringTools.generateId().toString(), key);
        images.push(
            <img id={elementKey} key={elementKey}
                 style={key === props.priorityNumber ? {} : {display: "none"}}
                 className="priority-indicator-image"
                 src={`/resources/priority/${imageName}.png`}
                 alt="Dial-style indicator for priority/urgency"/>
        )
    });

    return <React.Fragment>{images}</React.Fragment>
}
