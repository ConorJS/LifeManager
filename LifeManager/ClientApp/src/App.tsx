import React, {CSSProperties, FunctionComponent, useState} from 'react';
import {DataViewer} from './components/data-viewer';

import './App.scss'
import {ToDoTaskViewer} from "./components/to-do-task-viewer";

export const App: FunctionComponent = () => {
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
                         alt="my image"
                         onClick={() => setSelectedNavigationItem(MenuItem.HOME)}/>
                </button>

                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/view-icon.jpg"
                         alt="my image"
                         onClick={() => setSelectedNavigationItem(MenuItem.DATA_VIEWER)}/>
                </button>

                <button>
                    <img className="navigation-button"
                         src="/resources/navigationimages/task.png"
                         alt="my image"
                         onClick={() => setSelectedNavigationItem(MenuItem.TODO_TASKS)}/>
                </button>
            </div>

            <div style={displayToggleStyleFor(MenuItem.HOME)}>
                home
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

//== types ============================================================================================================

enum MenuItem {
    HOME,
    // Temporary
    DATA_VIEWER,
    TODO_TASKS
}
