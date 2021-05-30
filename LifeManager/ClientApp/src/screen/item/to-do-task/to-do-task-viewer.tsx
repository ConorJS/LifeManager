import React, {FunctionComponent, SyntheticEvent, useState} from "react";
import {LmModal} from '../../../components/modal/lm-modal';
import './to-do-task-viewer.scss';
import {SizePicker} from "../../../components/size-picker/size-picker";
import {ToDoTaskTable} from "./to-do-task-table";
import {PriorityPicker} from "../../../components/priority-picker/priority-picker";
import {LmAddFab} from "../../../components/lm-add-fab/lm-add-fab";
import {LmInput} from "../../../components/lm-input/lm-input";
import DeleteForeverIcon from '@material-ui/icons/DeleteForever';
import {ConfirmationModal} from "../../../components/confirmation-modal/confirmation-modal";
import {ToDoTaskConfig} from "../../root";
import {Typeahead} from 'react-bootstrap-typeahead';
import {StringTools} from "../../../tools/string-tools";
import {StateTools} from "../../../tools/state-tools";
import {ElementTools} from "../../../tools/element-tools";

//== types ============================================================================================================

export class ActiveItemDetails {
    newName: string = '';

    newStatus: string = 'Ready';

    newComments: string = '';

    newRelativeSize: number = 1;

    newPriority: number = 1;

    newDependentTasks: Number[] = [];
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

    dependencies: Number[];
    
    constructor(name: string, status: string, comments: string, relativeSize: number, priority: number, 
                dependencies: Number[]) {
        this.name = name;
        this.status = status;
        this.comments = comments;
        this.relativeSize = relativeSize;
        this.priority = priority;
        this.dependencies = dependencies;
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

interface ToDoTaskViewerProps {
    config: ToDoTaskConfig;
}

export const ToDoTaskViewer: FunctionComponent<ToDoTaskViewerProps> = (props: ToDoTaskViewerProps) => {
    //== attributes ===================================================================================================

    const [doRefresh, setDoRefresh] = useState<boolean>(true);
    const [toDoTasks, setToDoTasks] = useState<ToDoTask[]>([]);
    const [activeAction, setActiveAction] = useState<Action>(Action.NONE);
    const [activeItemDetails, setActiveItemDetails] = useState<ActiveItemDetails>(new ActiveItemDetails());
    const [itemBeingEdited, setItemBeingEdited] = useState<ToDoTask>();
    const [dependentTaskBeingEdited, setDependentTaskBeingEdited] = useState<string[]>([]);
    const [inRemovalModal, setInRemovalModal] = useState(false);
    const [inAdvancedOptionsModal, setInAdvancedOptionsModal] = useState(false);

    //== methods ======================================================================================================

    async function loadAllTasks(): Promise<ToDoTask[]> {
        const response = await fetch('api/ToDoTask/GetAll');
        return await response.json();
    }

    function changeAction(activeItemType: Action): void {
        setActiveItemDetails({
            ...activeItemDetails,
            // All 'new' fields reset here.
            newName: '',
            newStatus: 'Ready',
            newComments: '',
            newRelativeSize: 1,
            newPriority: 1,
            newDependentTasks: []
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
            newPriority: toDoTask.priority,
            newDependentTasks: toDoTask.dependencies
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
            activeItemDetails.newComments, activeItemDetails.newRelativeSize, activeItemDetails.newPriority, 
            activeItemDetails.newDependentTasks);

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(toDoTask)
        };

        fetch('api/ToDoTask/Create', requestOptions)
            .then(response => response.json())
            .then(() => {
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
            priority: activeItemDetails.newPriority,
            dependencies: activeItemDetails.newDependentTasks
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
                .then(() => {
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

    function saveConfig(config: ToDoTaskConfig): Promise<void> {
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(config)
        };

        return new Promise<void>((resolve) => {
            fetch('api/ToDoTask/UpdateUserConfig', requestOptions)
                .then(response => response.json())
                .then(resolve);
        });
    }

    function tasksMappedToSearchableStrings(): string[] {
        return toDoTasks
            .filter(task => StringTools.isNotBlank(task.name))
            .filter(task => task.id !== undefined && activeItemDetails.newDependentTasks.indexOf(task.id) === -1)
            .map(task => task.name);
    }

    function toDoTaskIdFromName(toDoTaskName: string): Number {
        if (toDoTaskName === undefined) {
            // Selection cleared
            return -1;
        }
        const selectedDependentTaskArray: ToDoTask[] = toDoTasks.filter(task => task.name === toDoTaskName);
        if (selectedDependentTaskArray.length !== 1) {
            throw Error(`Task ${toDoTaskName} was selected, 
                    but this matched ${selectedDependentTaskArray.length} task(s): ${selectedDependentTaskArray}`);
        }

        const selectedDependentTask: ToDoTask = selectedDependentTaskArray[0];
        if (!selectedDependentTask.id) {
            throw Error(`Task ${toDoTaskName} was selected, but this task has no id (it hasn't been saved yet)`);
        }

        return selectedDependentTask.id;
    }

    function applyDependentTaskSelection(): void {
        // If length === 0, no task was selected. Length should never exceed 1.
        if (dependentTaskBeingEdited.length !== 0) {
            activeItemDetails.newDependentTasks.push(toDoTaskIdFromName(dependentTaskBeingEdited[0]));
            setDependentTaskBeingEdited([]);
        }
    }

    function removeDependentTask(task: ToDoTask): void {
        const index = activeItemDetails.newDependentTasks.findIndex(dependentTaskId => task.id === dependentTaskId);
        if (index === -1) {
            throw Error(`Can't remove task with id ${task.id}, it isn't in the dependencies list.`);
        }

        StateTools.updateArray(activeItemDetails, setActiveItemDetails, "newDependentTasks", undefined, index);
    }

    function refresh(): void {
        loadAllTasks().then(setToDoTasks);
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

    } else if (inAdvancedOptionsModal) {
        const dependenciesList: JSX.Element[] = activeItemDetails.newDependentTasks
            .map(function (taskId) {
                const matchingTaskArray: ToDoTask[] = toDoTasks.filter(task => task.id === taskId);
                if (matchingTaskArray.length === 0) {
                    throw Error(`Dependent task with id ${taskId} can't be found.`);
                }

                return matchingTaskArray[0];
            })
            .map(function (task, index) {
                const key: string = ElementTools.makeListElementId(
                    "DependenciesList", StringTools.generateId().toString(), index);
                return <div key={key}>
                    <span>{task.name} [{task.status}]</span>
                    <button onClick={() => removeDependentTask(task)}>-</button>
                </div>
            });

        modalElement =
            <LmModal handleClose={() => setInAdvancedOptionsModal(false)}>
                <div className="column-modal-container">
                    <div>
                        <div>Task: "{activeItemDetails.newName}"</div>
                        <div>Advanced Options</div>
                    </div>

                    <div>
                        {/*fixed height*/}
                        {/*start with one empty input*/}
                        {/*unlimited empty input fields, but becomes vertical scrolling @ max height*/}
                        <span>Dependencies</span>
                        <span>
                            <Typeahead id="dependenciesPicker"
                                       options={tasksMappedToSearchableStrings()}
                                       onChange={setDependentTaskBeingEdited}
                                       placeholder="Search for a task..."
                                       selected={dependentTaskBeingEdited}/>
                            {/*TODO: Disable if task id being edited is -1 (blank)*/}
                            <button onClick={applyDependentTaskSelection}>+</button>
                        </span>

                        {dependenciesList}
                    </div>

                    <div className="modal-buttons-container">
                        <button className="btn lm-button positive modal-button"
                                onClick={(event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
                                    setInAdvancedOptionsModal(false)
                                    event.stopPropagation();
                                }}>
                            Back
                        </button>
                    </div>
                </div>
            </LmModal>;

    } else if (creating() || editing()) {
        modalElement =
            <LmModal handleClose={() => setActiveAction(Action.NONE)} widthPixels={400} heightPixels={575}>
                <div className="column-modal-container">
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

                    <div className="advanced-options-link"
                         onClick={(event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
                             setInAdvancedOptionsModal(true);
                             event.stopPropagation();
                         }}>Advanced
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
                    config={props.config}
                    taskSelected={editItem}
                    saveToDoTask={saveToDoTask}
                    saveConfig={saveConfig}/>
            </div>
            {modalElement}
        </div>
    )
}