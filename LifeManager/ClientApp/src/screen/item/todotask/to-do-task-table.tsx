import React, {FunctionComponent} from "react";
import {Column, useTable} from 'react-table'
import {ToDoTask} from "./to-do-task-viewer";

interface ToDoTaskTableProps {
    toDoTasks: ToDoTask[];
}

export const ToDoTaskTable: FunctionComponent<ToDoTaskTableProps> = (props: ToDoTaskTableProps) => {
    const columns = React.useMemo(
        () => [
            {
                Header: 'Name',
                accessor: (tasks: ToDoTask[], index: number): string => {
                    return tasks[index]?.name;
                }
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
            data: [props.toDoTasks]
        }
    )

    return (
        <table {...getTableProps()}>
            <thead>

            {headerGroups.map(headerGroup => (
                <tr {...headerGroup.getHeaderGroupProps()}>
                    {headerGroup.headers.map(column => (
                        <th {...column.getHeaderProps()}>{column.render('Header')}</th>
                    ))}
                </tr>
            ))}
            </thead>
            <tbody {...getTableBodyProps()}>
            {rows.map((row, i) => {
                prepareRow(row)
                return (
                    <tr {...row.getRowProps()}>
                        {row.cells.map(cell => {
                            return <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                        })}
                    </tr>
                )
            })}
            </tbody>
        </table>
    );
} 