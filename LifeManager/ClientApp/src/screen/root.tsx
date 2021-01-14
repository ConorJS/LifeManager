import React, {FunctionComponent, useState} from 'react';
import {DataViewer} from "../components/data-viewer";

import './root.scss'
import {ToDoTaskViewer} from "./item/todotask/to-do-task-viewer";
import {Tester} from "./test/tester";

enum MenuItem {
    HOME,
    // Temporary
    DATA_VIEWER,
    TODO_TASKS,
    TEST
}

export const Root: FunctionComponent = () => {
    //== state ========================================================================================================

    const [selectedNavigationItem, setSelectedNavigationItem] = useState(MenuItem.HOME);

    //== render =======================================================================================================

    let activeComponent;
    switch (selectedNavigationItem) {
        case MenuItem.HOME:
            activeComponent =
                <React.Fragment>
                    Home
                </React.Fragment>
            break;

        case MenuItem.DATA_VIEWER:
            activeComponent = <DataViewer/>
            break;

        case MenuItem.TODO_TASKS:
            activeComponent = <ToDoTaskViewer/>
            break;

        case MenuItem.TEST:
            activeComponent = <Tester/>
            break;
    }

    return (
        <React.Fragment>
            <div className="navigation-container">
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

                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/test.png"
                         alt="Test (Question mark in a box, ornamented with a check mark)"
                         onClick={() => setSelectedNavigationItem(MenuItem.TEST)}/>
                </button>
            </div>

            <div className="screen-container">
                {activeComponent}
            </div>
        </React.Fragment>
    );
}
