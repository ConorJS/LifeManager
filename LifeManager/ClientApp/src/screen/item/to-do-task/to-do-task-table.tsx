import React, {FunctionComponent, useEffect, useState} from "react";
import {Cell, Column, ColumnInstance, Row, useSortBy, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";

import MaUTable from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import SettingsIcon from '@material-ui/icons/Settings';

import './to-do-task-table.scss'
import {LmReactSelect, LmReactSelectOptions} from "../../../components/lm-react-select/lm-react-select";
import chroma from "chroma-js";
import {AppConstants} from "../../../app-constants";
import {ElementTools} from "../../../tools/element-tools";
import {SizeIndicator} from "../../../components/size-indicator/size-indicator";
import {PriorityIndicator} from "../../../components/priority-indicator/priority-indicator";
import {ExtraOnHover} from "../../../components/extra-on-hover/extra-on-hover";
import {StringTools} from "../../../tools/string-tools";
import {NumberTools} from "../../../tools/number-tools";
import {ObjectTools} from "../../../tools/object-tools";
import {Setting, SettingsWidget} from "../../../components/settings-widget/settings-widget";
import {ColumnSortOrder, ToDoTaskConfig} from "../../root";
import {StateTools} from "../../../tools/state-tools";

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
    config: ToDoTaskConfig;
    taskSelected: (task: ToDoTask) => void;
    saveToDoTask: (task: ToDoTask) => Promise<void>;
    saveConfig: (config: ToDoTaskConfig) => Promise<void>;
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
    //== attributes ===================================================================================================

    const HIDE_COMPLETE_CANCELLED: string = "Hide Completed & Cancelled";

    const [statusSettingsWidgetOpen, setStatusSettingsModalOpen] = useState(false);
    const [hideCompleteCancelledToggle, setHideCompleteCancelledToggle] = useState(false);
    const [config, setConfig] = useState(props.config);

    //== update effects ===============================================================================================

    useEffect(() => {
        console.log(`Pre-save of config at ${new Date()}`);

        setSortBy(config.columnSortOrderConfig
            .sort(function (a, b) {
                return a.precedence.valueOf() - b.precedence.valueOf();
            })
            .map(function (columnSortOrder) {
                return {id: columnSortOrder.columnName, desc: !columnSortOrder.isSortedAscending}
            }));
        setHideCompleteCancelledToggle(config.hideCompletedAndCancelled);

        props.saveConfig(config).then(() => console.log("Async config save completed successfully."));
    }, [config]);

    //== methods ======================================================================================================

    function blockEventPropagation(mouseEvent: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>): void {
        mouseEvent.stopPropagation();
    }

    function stopPropagationForSomeCells(mouseEvent: React.MouseEvent, cell: Cell<ToDoTask>): void {
        if (cell.column.id === 'actions' || cell.column.id === 'status') {
            mouseEvent.stopPropagation();
        }
    }

    /**
     * Updates a setting flag.
     *
     * @param setting The name of the setting (also the identifier).
     * @param setTo Whether or not the setting is being switched on (true) or off (false).
     */
    function updateSetting(setting: string, setTo: boolean): void {
        if (setting === HIDE_COMPLETE_CANCELLED) {
            setConfig({
                ...config,
                hideCompletedAndCancelled: setTo
            });

        } else {
            throw Error(`Unrecognized setting ${setting} changed.`);
        }
    }

    /**
     * Defines filtering that excludes items that isn't implemented within the framework/hooks offered by react-table (useFilters).
     *
     * These may be able to be performed by react-table (somehow), but aren't, likely due to lack of knowledge/understanding of react-table.
     *
     * @param rowItem The row.
     */
    function doesExternalFilteringExcludeItem(rowItem: ToDoTask): boolean {
        return hideCompleteCancelledToggle && ['Complete', 'Cancelled'].includes(rowItem.status);
    }

    function initialSortState(): { id: string, desc: boolean }[] {
        return config.columnSortOrderConfig
            .filter(sortOrderConfig => sortOrderConfig.columnName !== 'dummy').map(sortOrderConfig => {
                return {id: sortOrderConfig.columnName, desc: !sortOrderConfig.isSortedAscending};
            });
    }

    function toggleSort(column: ColumnInstance<ToDoTask>): void {
        const configId: Number = config.userConfigurationId;
        const columnId = column.id;
        const index: number = getOrDefaultColumnSortOrderIndex(column);
        if (column.isSorted && column.isSortedDesc) {
            // Descending sort -> No sort
            StateTools.updateArray(config, setConfig, 'columnSortOrderConfig', undefined, index);
            return;
        }

        // If we're not removing a sort order for a column (which is quite simple), we need to create/update one.        
        const otherSortOrderCount: number = config.columnSortOrderConfig
            .filter(sortOrderConfig => sortOrderConfig.columnName !== columnId)
            .length
        const columnSortOrder: ColumnSortOrder = index === -1 ?
            new ColumnSortOrder(configId, columnId, false, otherSortOrderCount) :
            config.columnSortOrderConfig[index];

        if (column.isSorted && !column.isSortedDesc) {
            // Ascending sort -> Descending sort
            columnSortOrder.isSortedAscending = false;
            StateTools.updateArray(config, setConfig, 'columnSortOrderConfig', columnSortOrder, index);

        } else {
            // No sort -> Ascending sort
            columnSortOrder.isSortedAscending = true;
            StateTools.updateArray(config, setConfig, 'columnSortOrderConfig', columnSortOrder, index);
        }
    }

    function getOrDefaultColumnSortOrderIndex(column: ColumnInstance<ToDoTask>): number {
        return config.columnSortOrderConfig
            .findIndex(sortOrderConfig => sortOrderConfig.columnName === column.id);
    }

    //== table state ==================================================================================================

    const columns: Column<ToDoTask>[] = React.useMemo(
        () => [
            {
                id: 'priority',
                Header: "Priority",
                accessor: row => <PriorityIndicator priorityNumber={row.priority}/>,
                width: 60,
                sortType: (rowA: Row<ToDoTask>, rowB: Row<ToDoTask>): number => NumberTools.compare(rowA.original.priority, rowB.original.priority)
            },
            {
                id: 'size',
                Header: "Size",
                accessor: row => <SizeIndicator outerDimensions={45} sizeNumber={row.relativeSize}/>,
                width: 60,
                sortType: (rowA: Row<ToDoTask>, rowB: Row<ToDoTask>): number => -NumberTools.compare(rowA.original.relativeSize, rowB.original.relativeSize)
            },
            {
                id: 'name',
                Header: 'Name',
                accessor: row => {
                    const maxCommentLength: number = 250;
                    const {truncated, cut} = ElementTools.truncateTextToFitInWidth(row.name, 250, true);
                    const truncatedComments: string = (row.comments && row.comments.length > maxCommentLength)
                        ? row.comments.substring(0, maxCommentLength)
                        : row.comments;

                    const smallerFontSize: number = 12;
                    const nameFontWeight: number = 600;
                    const nameStart = <span style={{fontWeight: nameFontWeight}}>{truncated}</span>
                    const nameEnd = !cut ? undefined :
                        <span style={{fontWeight: nameFontWeight, fontSize: smallerFontSize}}>{cut}</span>
                    return <ExtraOnHover
                        always={
                            <div style={{marginBottom: 8}}>
                                <div>
                                    {nameStart}
                                </div>
                                <div>
                                    {nameEnd}
                                </div>
                            </div>
                        }
                        extra={
                            <div style={{wordBreak: 'break-all'}}>
                                <span style={{fontSize: smallerFontSize}}>{truncatedComments}</span>
                            </div>}
                    />
                },
                sortType: (rowA: Row<ToDoTask>, rowB: Row<ToDoTask>): number => StringTools.compareBlanksLast(rowA.original.name, rowB.original.name),
                width: 250
            },
            {
                id: 'status',
                Header: "Status",
                accessor: row => (
                    <LmReactSelect
                        options={[
                            new LmReactSelectOptions('Ready', chroma('#888888')),
                            new LmReactSelectOptions('In Progress', chroma("#0ba5ec")),
                            new LmReactSelectOptions('Complete', chroma(AppConstants.LM_GREEN_STRONG)),
                            new LmReactSelectOptions('Cancelled', chroma(AppConstants.LM_RED_STRONG)),
                        ]}
                        valueChanged={(option: string) => {
                            return props.saveToDoTask({
                                ...row,
                                status: option
                            })
                        }}
                        selection={row.status}
                        widthPixels={150}
                    />
                ),
                sortType: (rowA: Row<ToDoTask>, rowB: Row<ToDoTask>): number => {
                    const statusPrecedence = new Map<string, number>([
                        ['In Progress', 0],
                        ['Ready', 1],
                        ['Complete', 2],
                        ['Cancelled', 3]
                    ]);

                    return NumberTools.compare(ObjectTools.getOrFail(statusPrecedence, rowA.original.status), ObjectTools.getOrFail(statusPrecedence, rowB.original.status));
                },
                width: 175
            }
        ], [props]);

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
        setSortBy
    } = useTable(
        {
            columns: columns,
            data: props.toDoTasks,
            initialState: {
                sortBy: initialSortState()
            },
            autoResetSortBy: false
        },
        useSortBy
    )

    let statusSettingsWidget =
        <div style={{display: (statusSettingsWidgetOpen ? "block" : "none")}}>
            <SettingsWidget options={[new Setting(HIDE_COMPLETE_CANCELLED, hideCompleteCancelledToggle)]}
                            optionChanged={updateSetting}
                            widgetClosed={() => setStatusSettingsModalOpen(false)}
            />
        </div>

    interface SortArrowProps {
        x1: boolean,
        y1: boolean,
        x2: boolean,
        y2: boolean,
        x3: boolean,
        y3: boolean,
        left: boolean,
        up: boolean
    }

    const SortArrow: React.FC<SortArrowProps> = ({...props}): any => {
        const x: number = 6;
        const y: number = 30.5;
        const polygonPoints: string =
            `${props.x1 ? x : 0},${props.y1 ? y : 0} ${props.x2 ? x : 0},${props.y2 ? y : 0} ${props.x3 ? x : 0},${props.y3 ? y : 0}`;
        return (
            <span className="arrow-down" style={{
                height: "100%",
                position: "absolute",
                left: props.left ? 0 : undefined, right: props.left ? undefined : 0,
                top: props.up ? 0.25 : 0.5
            }}>
                <svg height={y} width={x} fill={AppConstants.LM_GREEN_STRONG}>
                    <polygon points={polygonPoints} className="triangle"/>
                </svg>
            </span>
        )
    };
    const leftArrowUp = <SortArrow x1={false} y1={false} x2={true} y2={false} x3={false} y3={true} left={true} up={true}/>;
    const rightArrowUp = <SortArrow x1={false} y1={false} x2={true} y2={false} x3={true} y3={true} left={false} up={true}/>;
    const leftArrowDown = <SortArrow x1={false} y1={false} x2={false} y2={true} x3={true} y3={true} left={true} up={false}/>;
    const rightArrowDown = <SortArrow x1={false} y1={true} x2={true} y2={true} x3={true} y3={false} left={false} up={false}/>;

    function arrow(left: boolean, sorted: boolean, desc: boolean | undefined): JSX.Element | undefined {
        if (desc) {
            return left ? leftArrowDown : rightArrowDown;
        }
        if (sorted) {
            return left ? leftArrowUp : rightArrowUp;
        }
        return undefined;
    }

    //== render =======================================================================================================

    return (
        <MaUTable {...getTableProps()} className="to-do-task-table lm-shadowed">
            <TableHead>
                {headerGroups.map(headerGroup => (
                    <TableRow {...headerGroup.getHeaderGroupProps()}>
                        {headerGroup.headers.map(column => (
                            <TableCell {...column.getHeaderProps(column.getSortByToggleProps({title: undefined}))}
                                       width={column.width}
                                       style={{position: "relative"}}
                                       className={`lm-text no-select table-header-cell ${column.isSorted ? "table-header-cell-selected" : ""} column-with-dividers`}
                                       onClick={(event: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>) => {
                                           blockEventPropagation(event);
                                           toggleSort(column);
                                       }}>

                                {/* Left sorting arrow indicator */}
                                {arrow(true, column.isSorted, column.isSortedDesc)}

                                {/* The settings icon (should only show for the 'Status' column) */}
                                {column.id === 'status' ? statusSettingsWidget : <React.Fragment/>}
                                {column.id === 'status' ?
                                    <SettingsIcon className="status-settings-widget-button"
                                                  onClick={(event: React.MouseEvent<SVGSVGElement>) => {
                                                      setStatusSettingsModalOpen(true);
                                                      event.stopPropagation();
                                                  }}/>
                                    : <React.Fragment/>
                                }

                                {/* Draw the heading (the column name) */}
                                {column.render('Header')}

                                {/* Right sorting arrow indicator */}
                                {arrow(false, column.isSorted, column.isSortedDesc)}

                            </TableCell>
                        ))}
                    </TableRow>
                ))}

            </TableHead>

            <TableBody {...getTableBodyProps()}>
                {rows.map((row, i) => {
                    if (doesExternalFilteringExcludeItem(row.original)) {
                        return (<React.Fragment key={`excluded-row#${row.original.id}`}/>);

                    } else {
                        prepareRow(row)
                        return (
                            <TableRow {...row.getRowProps()}
                                      className="row-alternating-colors row-highlight-on-hover"
                                      onClick={() => props.taskSelected(row.original)}>

                                {row.cells.map(cell => {
                                    return (
                                        <TableCell {...cell.getCellProps()}
                                                   className="table-cell column-with-dividers lm-text"
                                                   onClick={(event: React.MouseEvent) => stopPropagationForSomeCells(event, cell)}>

                                            {cell.render('Cell')}

                                        </TableCell>
                                    )
                                })}

                            </TableRow>
                        )
                    }
                })}

            </TableBody>
        </MaUTable>
    );
} 