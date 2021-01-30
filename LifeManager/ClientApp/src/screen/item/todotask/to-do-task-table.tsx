import React, {FunctionComponent, useState} from "react";
import {Cell, Column, Row, useSortBy, UseSortByColumnProps, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";

import MaUTable from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import ArrowDropUpIcon from '@material-ui/icons/ArrowDropUp';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import SettingsIcon from '@material-ui/icons/Settings';

import './to-do-task-table.scss'
import {LmReactSelect, LmReactSelectOptions} from "../../../components/lmreactselect/lm-react-select";
import chroma from "chroma-js";
import {AppConstants} from "../../../app-constants";
import {ElementTools} from "../../../tools/element-tools";
import {SizeIndicator} from "../../../components/sizeindicator/size-indicator";
import {PriorityIndicator} from "../../../components/priorityindicator/priority-indicator";
import {ExtraOnHover} from "../../../components/extraonhover/extra-on-hover";
import {StringTools} from "../../../tools/string-tools";
import {NumberTools} from "../../../tools/number-tools";
import {ObjectTools} from "../../../tools/object-tools";
import {Setting, SettingsWidget} from "../../../components/settingswidget/settings-widget";

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
    taskSelected: (task: ToDoTask) => void
    saveToDoTask: (task: ToDoTask) => Promise<void>
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
    const [statusSettingsWidgetOpen, setStatusSettingsModalOpen] = useState(false);
    const [hideCompleteCancelledToggle, setHideCompleteCancelledToggle] = useState(false);

    const HIDE_COMPLETE_CANCELLED: string = "Hide Completed & Cancelled";

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
    const updateSetting = (setting: string, setTo: boolean): void => {
        if (setting === HIDE_COMPLETE_CANCELLED) {
            setHideCompleteCancelledToggle(setTo);

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
    const doesExternalFilteringExcludeItem = (rowItem: ToDoTask): boolean => {
        return hideCompleteCancelledToggle && ['Complete', 'Cancelled'].includes(rowItem.status);
    }

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
                    const {truncated, cut} = ElementTools.truncateTextToFitInWidth(row.name, 265, true);
                    const truncatedComments: string = (row.comments && row.comments.length > maxCommentLength)
                        ? row.comments.substring(0, maxCommentLength)
                        : row.comments;

                    const smallerFontSize: number = 12;
                    const nameFontWeight: number = 600;
                    const nameStart = <span style={{fontWeight: nameFontWeight}}>{truncated}{cut ? '...' : ''}</span>
                    const nameEnd = !cut ? undefined : <span style={{fontWeight: nameFontWeight, fontSize: smallerFontSize}}>{`...${cut}`}</span>
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
                        extra={<div style={{wordBreak: 'break-all'}}><span style={{fontSize: smallerFontSize}}>{truncatedComments}</span></div>}
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
        prepareRow
    } = useTable(
        {
            columns: columns,
            data: props.toDoTasks,
            initialState: {
                sortBy: [
                    {
                        id: 'name',
                        desc: false
                    }
                ]
            }
        },
        useSortBy
    )

    const toggleSort = (column: UseSortByColumnProps<ToDoTask>): void => {
        if (column.isSorted && column.isSortedDesc) {
            // Descending sort changes to no sort.
            column.clearSortBy();

        } else if (column.isSorted && !column.isSortedDesc) {
            // Ascending sort changes to descending sort.
            column.toggleSortBy(true);

        } else {
            // No sort changes to ascending sort.
            column.toggleSortBy(false);
        }
    }

    let statusSettingsWidget =
        <div style={{display: (statusSettingsWidgetOpen ? "block" : "none")}}>
            <SettingsWidget options={[new Setting(HIDE_COMPLETE_CANCELLED, hideCompleteCancelledToggle)]}
                            optionChanged={updateSetting}
                            widgetClosed={() => setStatusSettingsModalOpen(false)}
            />
        </div>

    return (
        <MaUTable {...getTableProps()} className="to-do-task-table lm-shadowed">
            <TableHead>
                {headerGroups.map(headerGroup => (
                    <TableRow {...headerGroup.getHeaderGroupProps()}>
                        {headerGroup.headers.map(column => (
                            <TableCell {...column.getHeaderProps(column.getSortByToggleProps({title: undefined}))}
                                       width={column.width}
                                       className={`lm-text no-select table-header-cell ${column.isSorted ? "table-header-cell-selected" : ""} column-with-dividers`}
                                       onClick={(event: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>) => {
                                           blockEventPropagation(event);
                                           toggleSort(column);
                                       }}>

                                {column.id === 'status' ? statusSettingsWidget : <React.Fragment/>}
                                {column.id === 'status' ?
                                    <SettingsIcon className="status-settings-widget-button" onClick={(event: React.MouseEvent<SVGSVGElement>) => {
                                        setStatusSettingsModalOpen(true);
                                        event.stopPropagation();
                                    }}/>
                                    : <React.Fragment/>
                                }

                                {column.render('Header')}

                                <span>
                                    {column.isSorted
                                        ? column.isSortedDesc
                                            ? <ArrowDropDownIcon/>
                                            : <ArrowDropUpIcon/>
                                        : ''}
                                  </span>

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