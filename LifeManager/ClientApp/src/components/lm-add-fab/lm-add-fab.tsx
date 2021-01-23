import React, {FunctionComponent} from "react";
import {Fab} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import './lm-add-fab.scss';

interface LmAddFabProps {
    selected: () => void
}

export const LmAddFab: FunctionComponent<LmAddFabProps> = (props: LmAddFabProps) => {
    return (
        <Fab className="lm-add-fab" aria-label="add" onClick={() => props.selected()}>
            <AddIcon/>
        </Fab>
    )
}