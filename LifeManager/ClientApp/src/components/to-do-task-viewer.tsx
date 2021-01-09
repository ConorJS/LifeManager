import React, {CSSProperties, FunctionComponent, SyntheticEvent, useState} from "react";
import {LmModal} from "./lm-modal";
import './to-do-task-viewer.scss';

export const ToDoTaskViewer: FunctionComponent = () => {
    //== attributes ===================================================================================================

    const [doRefresh, setDoRefresh] = useState<boolean>(true);
    const [toDoTasks, setToDoTasks] = useState<ToDoTask[]>([]);
    const [activeAction, setActiveAction] = useState<Action>(Action.NONE);
    const [activeItemDetails, setActiveItemDetails] = useState<ActiveItemDetails>(new ActiveItemDetails());
    const [itemBeingEdited, setItemBeingEdited] = useState<ToDoTask>();

    //== methods ======================================================================================================

    const loadAllTasks = async (): Promise<ToDoTask[]> => {
        const response = await fetch('api/ToDoTask/GetAll');
        return await response.json();
    }

    const changeAction = (activeItemType: Action): void => {
        setActiveItemDetails({
            ...activeItemDetails,
            newName: '',
            newRelativeSize: 1,
            newComments: ''
        });

        setActiveAction(activeItemType);
    }

    const stopAction = (): void => {
        changeAction(Action.NONE);
        setItemBeingEdited(undefined);
    }

    function creating(): boolean {
        return Action.CREATE === activeAction;
    }

    function editing(): boolean {
        return Action.EDIT === activeAction;
    }

    function activeItemAttributeChangeHandler(changeEvent: SyntheticEvent, itemAttribute: ItemAttribute): void {
        if (Action.NONE === activeAction) {
            console.error(`Tried to update value ${itemAttribute}, but there is no active item.`);
            return;
        }

        const target: HTMLInputElement = changeEvent.target as HTMLInputElement;
        const value: any = target.value;

        switch (itemAttribute) {
            case ItemAttribute.NAME:
                setActiveItemDetails({...activeItemDetails, newName: value as string});
                break;

            case ItemAttribute.RELATIVE_SIZE:
                setActiveItemDetails({...activeItemDetails, newRelativeSize: Number.parseInt(value)});
                break;

            case ItemAttribute.COMMENTS:
                setActiveItemDetails({...activeItemDetails, newComments: value as string});
                break;
        }
    }

    const editItem = (toDoTask: ToDoTask) => {
        changeAction(Action.EDIT);
        setActiveItemDetails({
            ...activeItemDetails,
            newName: toDoTask.name,
            newComments: toDoTask.comments,
            newRelativeSize: toDoTask.relativeSize
        })
        setItemBeingEdited(toDoTask)
    }

    const createToDoTask = () => {
        if (!creating()) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log("We are not in edit mode, yet saveToDoTask was called.");
            return;
        }

        const toDoTask = new ToDoTask(
            activeItemDetails.newName, activeItemDetails.newComments, activeItemDetails.newRelativeSize);

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(toDoTask)
        };

        fetch('api/ToDoTask/Create', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                stopAction();
                refresh();
            });
    }

    const saveToDoTask = (): void => {
        if (!editing() || !itemBeingEdited) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log(`${editing() ? "No item is being edited" : "We are not in edit mode"}, yet saveToDoTask was called.`);
            return;
        }

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                ...itemBeingEdited,
                name: activeItemDetails.newName,
                comments: activeItemDetails.newComments,
                relativeSize: activeItemDetails.newRelativeSize
            })
        };

        fetch('api/ToDoTask/Update', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                stopAction();
                refresh();
            });
    }

    const removeToDoTask = (toDoTask: ToDoTask): void => {
        fetch(`api/ToDoTask/Remove/${toDoTask.id}`).then(() => {
            // Close the edit window if the item being edited was just removed.
            if (itemBeingEdited === toDoTask) {
                stopAction();
            }

            refresh();
        });
    }

    const refresh = (): void => {
        loadAllTasks().then(data => {
            setToDoTasks(data);
        });
    }

    //== execution ====================================================================================================

    // TODO: Refactor this away?
    if (doRefresh) {
        refresh();
        setDoRefresh(false);
    }

    //== render =======================================================================================================

    const items: JSX.Element[] = [];
    toDoTasks.forEach((toDoTask, index) => {
        items.push(
            <div key={'toDoTask' + index}>
                <div onClick={() => editItem(toDoTask)}>
                    {toDoTask.name} ({toDoTask.relativeSize} at {toDoTask.dateTimeCreated})
                </div>
                <button onClick={() => removeToDoTask(toDoTask)}>
                    R
                </button>
            </div>
        );
    });

    let modalElement;
    if (creating() || editing()) {
        modalElement =
            <LmModal handleClose={() => setActiveAction(Action.NONE)}>
                <div className="modal-container">
                    <div>{editing() ? "Edit" : "Creat"}ing a To Do task...</div>

                    <div className="modal-field">
                        <label htmlFor="active-todo-task-name">Name</label>
                        <input id="active-todo-task-name"
                               type="string"
                               value={activeItemDetails.newName}
                               onChange={(event) => activeItemAttributeChangeHandler(event, ItemAttribute.NAME)}/>
                    </div>

                    <div className="modal-field">
                        <label htmlFor="editing-todo-task-relative-size">Size</label>
                        <input id="editing-todo-task-relative-size"
                               type="number"
                               value={activeItemDetails.newRelativeSize}
                               onChange={(event) => activeItemAttributeChangeHandler(event, ItemAttribute.RELATIVE_SIZE)}/>
                    </div>

                    <div className="modal-field">
                        <label htmlFor="editing-todo-task-comments">Comments</label>
                        <textarea id="editing-todo-task-comments"
                                  rows={4}
                                  typeof="string"
                                  value={activeItemDetails.newComments}
                                  onChange={(event) => activeItemAttributeChangeHandler(event, ItemAttribute.COMMENTS)}/>
                    </div>

                    <div className="modal-buttons-container">
                        <button className="btn btn-primary modal-button"
                                onClick={editing() ? saveToDoTask : createToDoTask}>
                            Save
                        </button>

                        <button className="btn btn-primary modal-button"
                                onClick={stopAction}>
                            Cancel
                        </button>
                    </div>
                </div>
            </LmModal>
    }

    return (
        <div>
            <div>To Do Tasks:</div>
            <div>{items}</div>

            <button className="btn btn-primary"
                    onClick={() => changeAction(Action.CREATE)}>
                New
            </button>

            {modalElement}
        </div>
    )
}

//== types ============================================================================================================

export class ActiveItemDetails {
    newName: string = '';

    newRelativeSize: number = 1;

    newComments: string = '';
}

export class ToDoTask {
    id?: Number;

    name: string;
    
    comments: string;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    relativeSize: number;

    constructor(name: string, comments: string, relativeSize: number) {
        this.name = name;
        this.comments = comments;
        this.relativeSize = relativeSize;
    }
}

export enum Action {
    NONE,
    CREATE,
    EDIT
}

export enum ItemAttribute {
    NAME,
    RELATIVE_SIZE,
    COMMENTS
}