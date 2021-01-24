import React, {FunctionComponent} from "react";
import {Cell, Column, useSortBy, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";

import MaUTable from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";

import './to-do-task-table.scss'
import {LmReactSelect, LmReactSelectOptions} from "../../../components/lmreactselect/lm-react-select";
import chroma from "chroma-js";
import {AppConstants} from "../../../app-constants";
import {ElementTools} from "../../../tools/element-tools";
import {SizeIndicator} from "../../../components/sizeindicator/size-indicator";
import {PriorityIndicator} from "../../../components/priorityindicator/priority-indicator";

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
    taskSelected: (task: ToDoTask) => void
    saveToDoTask: (task: ToDoTask) => Promise<void>
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
    function blockEventPropagation(mouseEvent: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>): void {
        mouseEvent.stopPropagation();
    }

    function stopPropagationForSomeCells(mouseEvent: React.MouseEvent, cell: Cell<ToDoTask>): void {
        if (cell.column.id === 'actions' || cell.column.id === 'status') {
            mouseEvent.stopPropagation();
        }
    }

    const columns: Column<ToDoTask>[] = React.useMemo(
        () => [
            {
                id: 'priority',
                Header: "Priority",
                accessor: row => <PriorityIndicator priorityNumber={row.priority}/>,
                width: 60
            },
            {
                id: 'size',
                Header: "Size",
                accessor: row => <SizeIndicator outerDimensions={45} sizeNumber={row.relativeSize}/>,
                width: 60
            },
            {
                id: 'name',
                Header: 'Name',
                accessor: row => {
                    const {truncated, cut} = ElementTools.truncateTextToFitInWidth(row.name, 265);

                    return <span>{truncated}{cut ? '...' : ''}</span>
                },
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
                    />
                ),
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

    return (
        <MaUTable {...getTableProps()} className="to-do-task-table">
            <TableHead className="table-header">

                {headerGroups.map(headerGroup => (
                    <TableRow {...headerGroup.getHeaderGroupProps()}>
                        {headerGroup.headers.map(column => (
                            <TableCell {...column.getHeaderProps()}
                                       width={column.width}
                                       className="lm-text table-header-cell column-with-dividers"
                                       onClick={blockEventPropagation}>

                                {column.render('Header')}

                            </TableCell>
                        ))}
                    </TableRow>
                ))}

            </TableHead>

            <TableBody {...getTableBodyProps()}>

                {rows.map((row, i) => {
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
                })}

            </TableBody>
        </MaUTable>
    );
} 