import React, {FunctionComponent} from "react";
import {Column, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";
import {SizePickerTools} from "../../../components/sizepicker/size-picker";

import MaUTable from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
    taskSelected: (task: ToDoTask) => void
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
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
            <TableHead>
                {headerGroups.map(headerGroup => (
                    <TableRow {...headerGroup.getHeaderGroupProps()}>
                        {headerGroup.headers.map(column => (
                            <TableCell {...column.getHeaderProps()}>{column.render('Header')}</TableCell>
                        ))}
                    </TableRow>
                ))}
            </TableHead>

            <TableBody {...getTableBodyProps()}>
                {rows.map((row, i) => {
                    prepareRow(row)
                    return (
                        <TableRow {...row.getRowProps()} onClick={(event: any) => props.taskSelected(row.original)}>
                            {row.cells.map(cell => {
                                return <TableCell {...cell.getCellProps()}>{cell.render('Cell')}</TableCell>
                            })}
                        </TableRow>
                    )
                })}
            </TableBody>
        </MaUTable>
    );
} 