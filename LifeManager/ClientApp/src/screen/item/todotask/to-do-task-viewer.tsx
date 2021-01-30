import React, {FunctionComponent, SyntheticEvent, useState} from "react";
import {LmModal} from '../../../components/modal/lm-modal';
import './to-do-task-viewer.scss';
import {SizePicker} from "../../../components/sizepicker/size-picker";
import {ToDoTaskTable} from "./to-do-task-table";
import {PriorityPicker} from "../../../components/prioritypicker/priority-picker";
import {LmAddFab} from "../../../components/lm-add-fab/lm-add-fab";
import {LmInput} from "../../../components/lm-input/lm-input";
import DeleteForeverIcon from '@material-ui/icons/DeleteForever';
import {ConfirmationModal} from "../../../components/confirmationmodal/confirmation-modal";

//== types ============================================================================================================

export class ActiveItemDetails {
    newName: string = '';

    newStatus: string = 'Ready';

    newComments: string = '';

    newRelativeSize: number = 1;

    newPriority: number = 1;
}

export class ToDoTask {
    id?: Number;

    dateTimeCreated?: Date;

    dateTimeLastModified?: Date;

    name: string;

    status: string;

    comments: string;

    relativeSize: number;

    priority: number;

    constructor(name: string, status: string, comments: string, relativeSize: number, priority: number) {
        this.name = name;
        this.status = status;
        this.comments = comments;
        this.relativeSize = relativeSize;
        this.priority = priority;
    }
}

export enum Action {
    NONE,
    CREATE,
    EDIT
}

export enum ItemAttribute {
    NAME,
    STATUS,
    COMMENTS,
    RELATIVE_SIZE,
    PRIORITY
}

export const ToDoTaskViewer: FunctionComponent = () => {
    //== attributes ===================================================================================================

    const [doRefresh, setDoRefresh] = useState<boolean>(true);
    const [toDoTasks, setToDoTasks] = useState<ToDoTask[]>([]);
    const [activeAction, setActiveAction] = useState<Action>(Action.NONE);
    const [activeItemDetails, setActiveItemDetails] = useState<ActiveItemDetails>(new ActiveItemDetails());
    const [itemBeingEdited, setItemBeingEdited] = useState<ToDoTask>();
    const [inRemovalModal, setInRemovalModal] = useState(false);

    //== methods ======================================================================================================

    async function loadAllTasks(): Promise<ToDoTask[]> {
        const response = await fetch('api/ToDoTask/GetAll');
        return await response.json();
    }

    function changeAction(activeItemType: Action): void {
        setActiveItemDetails({
            ...activeItemDetails,
            newName: '',
            newStatus: 'Ready',
            newComments: '',
            newRelativeSize: 1,
            newPriority: 1
        });

        setActiveAction(activeItemType);
    }

    function stopAction(): void {
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

        setAttributeValue(value, itemAttribute);
    }

    function setAttributeValue(value: any, itemAttribute: ItemAttribute): void {
        switch (itemAttribute) {
            case ItemAttribute.NAME:
                setActiveItemDetails({...activeItemDetails, newName: value as string});
                break;

            case ItemAttribute.STATUS:
                setActiveItemDetails({...activeItemDetails, newStatus: value as string});
                break;

            case ItemAttribute.COMMENTS:
                setActiveItemDetails({...activeItemDetails, newComments: value as string});
                break;

            case ItemAttribute.RELATIVE_SIZE:
                setActiveItemDetails({...activeItemDetails, newRelativeSize: Number.parseInt(value)});
                break;

            case ItemAttribute.PRIORITY:
                setActiveItemDetails({...activeItemDetails, newPriority: Number.parseInt(value)});
                break;
        }
    }

    function editItem(toDoTask: ToDoTask): void {
        changeAction(Action.EDIT);
        setActiveItemDetails({
            ...activeItemDetails,
            newName: toDoTask.name,
            newStatus: toDoTask.status,
            newComments: toDoTask.comments,
            newRelativeSize: toDoTask.relativeSize,
            newPriority: toDoTask.priority
        })
        setItemBeingEdited(toDoTask)
    }

    function createToDoTask(): void {
        if (!creating()) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log("We are not in edit mode, yet saveActiveToDoTask was called.");
            return;
        }

        const toDoTask = new ToDoTask(
            activeItemDetails.newName, activeItemDetails.newStatus,
            activeItemDetails.newComments, activeItemDetails.newRelativeSize, activeItemDetails.newPriority);

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

    function saveActiveToDoTask(): void {
        if (!editing() || !itemBeingEdited) {
            // TODO: This problem should be addressed with form validation, and appropriate error messages.
            console.log(`${editing() ? "No item is being edited" : "We are not in edit mode"}, yet saveActiveToDoTask was called.`);
            return;
        }

        const updatedTask: ToDoTask = {
            ...itemBeingEdited,
            name: activeItemDetails.newName,
            comments: activeItemDetails.newComments,
            relativeSize: activeItemDetails.newRelativeSize,
            priority: activeItemDetails.newPriority
        }

        saveToDoTask(updatedTask).then();
    }

    function saveToDoTask(toDoTask: ToDoTask): Promise<void> {
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(toDoTask)
        };

        return new Promise<void>((resolve) => {
            fetch('api/ToDoTask/Update', requestOptions)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    stopAction();
                    resolve();
                    refresh();
                });
        });
    }

    function removeToDoTask(toDoTask: ToDoTask): void {
        fetch(`api/ToDoTask/Remove/${toDoTask.id}`).then(() => {
            // Close the edit window if the item being edited was just removed.
            if (itemBeingEdited === toDoTask) {
                stopAction();
            }

            refresh();
        });
    }

    function refresh(): void {
        loadAllTasks().then(data => {
            console.log("Calling setToDoTasks...");
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

    let deleteButton;
    if (editing()) {
        deleteButton =
            <div>
                <DeleteForeverIcon
                    className="remove-item"
                    onClick={(event: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
                        setInRemovalModal(true);
                        event.stopPropagation();
                    }}>DeleteForever
                </DeleteForeverIcon>
            </div>
    }

    let modalElement;
    if (inRemovalModal) {
        modalElement =
            <ConfirmationModal
                acceptBehaviour={() => {
                    if (!itemBeingEdited) {
                        throw Error("Should not be shown removal modal if no item is being edited.");
                    }
                    setInRemovalModal(false);
                    setActiveAction(Action.NONE);
                    removeToDoTask(itemBeingEdited);
                }}
                rejectionBehaviour={() => {
                    setInRemovalModal(false)
                }}
                warningMessage={`Are you sure you want to remove the task '${itemBeingEdited?.name}'?`}
            />

    } else if (creating() || editing()) {
        modalElement =
            <LmModal handleClose={() => setActiveAction(Action.NONE)} widthPixels={400} heightPixels={575}>
                <div className="editing-item-modal-container">
                    <div>{editing() ? "Edit" : "Creat"}ing a To Do task...</div>

                    <div className="modal-field">
                        <LmInput id="active-todo-task-name"
                                 label="Name"
                                 value={activeItemDetails.newName}
                                 maxLength={80}
                                 onChange={(event) => activeItemAttributeChangeHandler(event, ItemAttribute.NAME)}/>
                    </div>

                    <div className="modal-field">
                        <label htmlFor="editing-todo-task-relative-size">Priority</label>
                        <PriorityPicker initialPriority={activeItemDetails.newPriority}
                                        prioritySelected={(priority) => setAttributeValue(priority, ItemAttribute.PRIORITY)}/>
                    </div>

                    <div className="modal-field">
                        <label htmlFor="editing-todo-task-relative-size">Size</label>
                        <SizePicker initialSize={activeItemDetails.newRelativeSize}
                                    sizeSelected={(size) => setAttributeValue(size, ItemAttribute.RELATIVE_SIZE)}/>
                    </div>

                    <div className="modal-field">
                        <LmInput id="editing-todo-task-comments"
                                 label="Comments"
                                 maxLength={2500}
                                 value={activeItemDetails.newComments}
                                 useTextArea={true}
                                 onChange={(event) => activeItemAttributeChangeHandler(event, ItemAttribute.COMMENTS)}/>
                    </div>

                    {deleteButton}

                    <div className="modal-buttons-container">
                        <button className="btn lm-button positive modal-button"
                                onClick={editing() ? saveActiveToDoTask : createToDoTask}>
                            Save
                        </button>

                        <button className="btn lm-button negative modal-button"
                                onClick={stopAction}>
                            Cancel
                        </button>
                    </div>
                </div>
            </LmModal>
    }

    return (
        <div>
            <h2 className="lm-text page-header">To-Do List</h2>

            <div style={{float: 'right', marginBottom: -25}}>
                <LmAddFab selected={() => changeAction(Action.CREATE)}/>
            </div>

            <div>
                <ToDoTaskTable
                    toDoTasks={toDoTasks}
                    taskSelected={editItem}
                    saveToDoTask={saveToDoTask}/>
            </div>
            {modalElement}
        </div>
    )
}