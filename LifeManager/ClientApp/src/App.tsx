import React, {CSSProperties, FunctionComponent, useState} from 'react';
import {DataViewer} from './components/data-viewer';

import './App.scss'
import {ToDoTaskViewer} from "./components/to-do-task-viewer";
import {ModalImpl} from "./components/modal-impl";

enum MenuItem {
    HOME,
    // Temporary
    DATA_VIEWER,
    TODO_TASKS
}

export const App: FunctionComponent = () => {
    //== state ========================================================================================================

    const [selectedNavigationItem, setSelectedNavigationItem] = useState(MenuItem.HOME);
    const [displayModal, setDisplayModal] = useState(false);

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
                {displayModal &&
                <div>
                    <ModalImpl handleClose={() => setDisplayModal(false)}>
                        <div className="modal-container">
                            test modal contents
                        </div>
                    </ModalImpl>
                </div>
                }

                <button onClick={() => setDisplayModal(true)}>
                    T
                </button>
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
