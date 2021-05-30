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
    id: Number;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    displayName: string;

    status: string;

    configuration: UserConfiguration;

    constructor(id: Number, displayName: string, status: string, configuration: UserConfiguration) {
        this.id = id;
        this.displayName = displayName;
        this.status = status;
        this.configuration = configuration;
    }
}

export class UserConfiguration {
    id: Number;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    toDoTaskConfig: ToDoTaskConfig;

    constructor(id: Number, toDoTaskConfig: ToDoTaskConfig) {
        this.id = id;
        this.toDoTaskConfig = toDoTaskConfig;
    }
}

export class ColumnSortOrder {
    userConfigurationId?: Number;
    
    columnName: string;

    isSortedAscending: boolean;

    precedence: Number;

    constructor(userConfigurationId: Number, columnName: string, isSortedAscending: boolean, precedence: Number) {
        this.userConfigurationId = userConfigurationId;
        this.columnName = columnName;
        this.isSortedAscending = isSortedAscending;
        this.precedence = precedence;
    }
}

export class ToDoTaskConfig {
    userConfigurationId: Number;

    columnSortOrderConfig: ColumnSortOrder[];

    hideCompletedAndCancelled: boolean;

    constructor(userConfigurationId: Number, columnSortOrderConfig: ColumnSortOrder[],
                hideCompletedAndCancelled: boolean) {
        this.userConfigurationId = userConfigurationId;
        this.columnSortOrderConfig = columnSortOrderConfig;
        this.hideCompletedAndCancelled = hideCompletedAndCancelled;
    }
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

    // TODO: Refactor this away?
    if (doRefresh) {
        refresh();
        setDoRefresh(false);
    }

    if (activeUser === undefined) {
        // Don't render anything until the user has loaded.
        return <Fragment/>;
    }

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
            activeComponent = <ToDoTaskViewer config={activeUser.configuration.toDoTaskConfig}/>
            break;

        case MenuItem.TEST:
            activeComponent = <Tester/>
            break;
    }

    function refresh() {
        loadUser().then(setActiveUser);
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
                         onClick={() => {
                             loadUser().then((userData) => {
                                 setActiveUser(userData);
                                 setSelectedNavigationItem(MenuItem.TODO_TASKS)
                             });
                         }}/>
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
