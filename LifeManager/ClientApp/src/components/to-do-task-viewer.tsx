import React, {CSSProperties, FunctionComponent, SyntheticEvent, useState} from "react";

export const ToDoTaskViewer: FunctionComponent = () => {
    //== attributes ===================================================================================================

    const [doRefresh, setDoRefresh] = useState<boolean>(true);
    const [toDoTasks, setToDoTasks] = useState<ToDoTask[]>([]);

    const [creatingItem, setCreatingItem] = useState<boolean>(false);
    const [creatingItemName, setCreatingItemName] = useState<string>();
    const [creatingItemRelativeSize, setCreatingItemRelativeSize] = useState<number>();

    const [editingItem, setEditingItem] = useState<ToDoTask>();
    const [editingItemName, setEditingItemName] = useState<string>();
    const [editingItemRelativeSize, setEditingItemRelativeSize] = useState<number>();

    //== methods ======================================================================================================

    const loadAllTasks = async (): Promise<ToDoTask[]> => {
        const response = await fetch('api/ToDoTask/GetAll');
        return await response.json();
    }

    const displayCreateToDoTaskUi = (): void => {
        setCreatingItem(true);
    }

    const displayEditToDoTaskUi = (toDoTask: ToDoTask): void => {
        setEditingItem(toDoTask);
        setEditingItemName(toDoTask.name);
        setEditingItemRelativeSize(toDoTask.relativeSize);
        
        cancelCreateToDoTaskUi();
    }

    const cancelCreateToDoTaskUi = (): void => {
        setCreatingItem(false);
    }

    const cancelEditToDoTaskUi = (): void => {
        setEditingItem(undefined);
    }

    // TODO: Remove handler/editing pattern, and just pass values into onClick
    function creatingItemNameChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setCreatingItemName(target.value);
    }

    function creatingItemRelativeSizeChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setCreatingItemRelativeSize(Number.parseInt(target.value));
    }

    function editingItemNameChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setEditingItemName(target.value);
    }

    function editingItemRelativeSizeChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setEditingItemRelativeSize(Number.parseInt(target.value));
    }

    const createToDoTask = () => {
        if (!creatingItemName || !creatingItemRelativeSize) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log("Required fields missing; cannot create a To Do task.");
            return;
        }

        const toDoTask = new ToDoTask(creatingItemName, creatingItemRelativeSize);

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(toDoTask)
        };

        fetch('api/ToDoTask/Create', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                refresh();
            });
    }

    const saveToDoTask = (): void => {
        if (!editingItem || !editingItemName || !editingItemRelativeSize) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log("Required fields missing; cannot update To Do task.");
            return;
        }

        editingItem.name = editingItemName;
        editingItem.relativeSize = editingItemRelativeSize;

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(editingItem)
        };

        fetch('api/ToDoTask/Update', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                setEditingItem(undefined);
                refresh();
            });
    }

    const refresh = (): void => {
        loadAllTasks().then(data => {
            setToDoTasks(data);
        });
    }

    //== execution ====================================================================================================

    if (doRefresh) {
        refresh();
        setDoRefresh(false);
    }

    //== render =======================================================================================================

    const displayNone: CSSProperties = {display: "none"};
    const items: JSX.Element[] = [];
    toDoTasks.forEach((toDoTask, index) => {
        items.push(
            <div key={'toDoTask' + index}
                 onClick={() => displayEditToDoTaskUi(toDoTask)}>
                {toDoTask.name} ({toDoTask.relativeSize} at {toDoTask.dateTimeCreated})
            </div>
        );
    });

    return (
        <div>
            <div>To Do Tasks:</div>
            <div>{items}</div>

            <button className="btn btn-primary"
                    onClick={displayCreateToDoTaskUi}>
                New
            </button>

            <div className="create-details"
                 style={creatingItem ? {} : displayNone}>

                <div>Creating a To Do task...</div>

                <div>
                    <label htmlFor="creating-todo-task-name">Name</label>
                    <input id="creating-todo-task-name"
                           type="string"
                           value={creatingItemName}
                           onChange={creatingItemNameChangeHandler}/>
                </div>

                <div>
                    <label htmlFor="creating-todo-task-relative-size">Relative Size</label>
                    <input id="creating-todo-task-relative-size"
                           type="number"
                           value={creatingItemRelativeSize}
                           onChange={creatingItemRelativeSizeChangeHandler}/>
                </div>

                <div>
                    <button className="btn btn-primary"
                            onClick={createToDoTask}>
                        Create
                    </button>

                    <button className="btn btn-primary"
                            onClick={cancelCreateToDoTaskUi}>
                        Cancel
                    </button>
                </div>
            </div>

            <div style={!!editingItem ? {} : displayNone}>
                <div>Editing a To Do task...</div>

                <div>
                    <label htmlFor="editing-todo-task-name">Name</label>
                    <input id="editing-todo-task-name"
                           type="string"
                           value={editingItemName}
                           onChange={editingItemNameChangeHandler}/>
                </div>

                <div>
                    <label htmlFor="editing-todo-task-relative-size">Relative Size</label>
                    <input id="editing-todo-task-relative-size"
                           type="number"
                           value={editingItemRelativeSize}
                           onChange={editingItemRelativeSizeChangeHandler}/>
                </div>

                <div>
                    <button className="btn btn-primary"
                            onClick={saveToDoTask}>
                        Save
                    </button>

                    <button className="btn btn-primary"
                            onClick={cancelEditToDoTaskUi}>
                        Cancel
                    </button>
                </div>
            </div>
        </div>
    )
}

//== types ============================================================================================================

export class ToDoTask {
    id?: Number;

    name: string;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    relativeSize: number;

    constructor(name: string, relativeSize: number) {
        this.name = name;
        this.relativeSize = relativeSize;
    }
}