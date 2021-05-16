import React, {Fragment, FunctionComponent, useState} from 'react';

import './root.scss'
import {ToDoTaskViewer} from "./item/to-do-task/to-do-task-viewer";
import {Tester} from "./test/tester";

enum MenuItem {
    HOME,
    // Temporary
    DATA_VIEWER,
    TODO_TASKS,
    TEST
}

export class User {
    id?: Number;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    displayName?: string;

    status?: string;
    
    configuration?: UserConfiguration;
}

export class UserConfiguration {
    id?: Number;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;
    
    toDoTaskConfig?: ToDoTaskConfig; 
}

export class ColumnSortOrder {
    columnName?: string;
    
    precedence?: Number;
}

export class ToDoTaskConfig {
    columnSortOrderConfig?: ColumnSortOrder[];

    hideCompletedAndCancelled?: boolean;
}

export const Root: FunctionComponent = () => {
    //== state ========================================================================================================

    const [doRefresh, setDoRefresh] = useState<boolean>(true);
    const [activeUser, setActiveUser] = useState<User>();
    const [selectedNavigationItem, setSelectedNavigationItem] = useState(MenuItem.HOME);

    //== methods ======================================================================================================
    
    async function loadUser(): Promise<User> {
        const response = await fetch('api/User/GetLoggedInUser');
        return await response.json();
    }

    //== execution ====================================================================================================

    let activeComponent;
    switch (selectedNavigationItem) {
        case MenuItem.HOME:
            activeComponent =
                <Fragment>
                    Home
                </Fragment>
            break;

        case MenuItem.DATA_VIEWER:
            activeComponent = "Nothing here!"
            break;

        case MenuItem.TODO_TASKS:
            activeComponent = <ToDoTaskViewer/>
            break;

        case MenuItem.TEST:
            activeComponent = <Tester/>
            break;
    }
    
    function refresh() {
        loadUser().then(data => {
            console.log("Calling setActiveUser...");
            setActiveUser(data);
        });
    }

    // TODO: Refactor this away?
    if (doRefresh) {
        refresh();
        setDoRefresh(false);
    }
    
    //== render =======================================================================================================

    return (
        <Fragment>
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
        </Fragment>
    );
}
