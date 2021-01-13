import React, {FunctionComponent} from "react";
import {Cell, Column, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";
import {SizePickerTools} from "../../../components/sizepicker/size-picker";

import MaUTable from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import DeleteForeverIcon from '@material-ui/icons/DeleteForever';

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
    taskSelected: (task: ToDoTask) => void
    taskDeleted: (task: ToDoTask) => void
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
    function blockEventPropagation(mouseEvent: React.MouseEvent<HTMLTableHeaderCellElement, MouseEvent>): void {
        mouseEvent.stopPropagation();
    }

    function stopPropagationForActionCell(mouseEvent: React.MouseEvent, cell: Cell<ToDoTask>): void {
        if (cell.column.id === 'actions') {
            mouseEvent.stopPropagation();
        }
    }

    const columns: Column<ToDoTask>[] = React.useMemo(
        () => [
            {
                id: 'name',
                Header: 'Name',
                accessor: "name"
            },
            {
                id: 'size',
                Header: "Size",
                accessor: row => SizePickerTools.sizeStringFromNumber(row.relativeSize)
            },
            {
                id: 'actions',
                Header: "",
                accessor: row => (
                    <DeleteForeverIcon
                        onClick={(event: any) => {
                            props.taskDeleted(row);
                            blockEventPropagation(event);
                        }}>DeleteForever
                    </DeleteForeverIcon>
                )
            }
        ], []);

    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow
    } = useTable(
        {
            columns: columns,
            data: props.toDoTasks
        }
    )

    return (
        <MaUTable {...getTableProps()}>
            <TableHead className="table-header">

                {headerGroups.map(headerGroup => (
                    <TableRow {...headerGroup.getHeaderGroupProps()}>
                        {headerGroup.headers.map(column => (
                            <TableCell {...column.getHeaderProps()}
                                       className="table-header-cell column-with-dividers"
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
                                               className="column-with-dividers" 
                                               onClick={(event: React.MouseEvent) => stopPropagationForActionCell(event, cell)}>

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