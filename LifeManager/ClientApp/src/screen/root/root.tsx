import React, {CSSProperties, FunctionComponent, useState} from 'react';
import {DataViewer} from "../../components/data-viewer";

import './root.scss'
import {ToDoTaskViewer} from "../item/todotask/to-do-task-viewer";

enum MenuItem {
    HOME,
    // Temporary
    DATA_VIEWER,
    TODO_TASKS
}

export const Root: FunctionComponent = () => {
    //== state ========================================================================================================

    const [selectedNavigationItem, setSelectedNavigationItem] = useState(MenuItem.HOME);

    //== methods ======================================================================================================

    function displayToggleStyleFor(menuItem: MenuItem): CSSProperties {
        return menuItem === selectedNavigationItem ? {} : {display: "none"}
    }

    //== render =======================================================================================================

    return (
        <React.Fragment>
            <div>
                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/home.png"
                         alt="Home button (LifeManager logo)"
                         onClick={() => setSelectedNavigationItem(MenuItem.HOME)}/>
                </button>

                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/view-icon.jpg"
                         alt="Summary button (Eye; abstract/stencil)"
                         onClick={() => setSelectedNavigationItem(MenuItem.DATA_VIEWER)}/>
                </button>

                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/task.png"
                         alt="Task list (Notepad; abstract/stencil)"
                         onClick={() => setSelectedNavigationItem(MenuItem.TODO_TASKS)}/>
                </button>
            </div>

            <div style={displayToggleStyleFor(MenuItem.HOME)}>
                Home
            </div>

            <div style={displayToggleStyleFor(MenuItem.DATA_VIEWER)}>
                <DataViewer/>
            </div>

            <div style={displayToggleStyleFor(MenuItem.TODO_TASKS)}>
                <ToDoTaskViewer/>
            </div>
        </React.Fragment>
    );
}
