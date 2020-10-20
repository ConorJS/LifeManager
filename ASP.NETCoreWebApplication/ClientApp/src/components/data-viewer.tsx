import React, {FunctionComponent, useState} from 'react';

interface DataViewerProps {
    incrementCounter: () => void;

    dummyData: DummyData[];

    populateUiWithClientSideData: () => void;

    populateUiWithDatabaseDummyData: () => void;
}

export const DataViewer: FunctionComponent<DataViewerProps> = props => {
    const [currentCount, setCurrentCount] = useState(0);
    const [dummyData, setDummyData] = useState<DummyData[]>([]);

    function addDummyData(newDummyData: DummyData): void {
        setDummyData(dummyData.concat(newDummyData));
    }

    function incrementCounter(): void {
        setCurrentCount(currentCount + 1);
    }

    const populateUiWithDatabaseDummyData = async () => {
        const response = await fetch('dummydata');
        const data = await response.json();

        addDummyData(new DummyData(data.id, data.name));
    }

    let items: JSX.Element[] = [];
    dummyData.forEach((data, index) => {
        items.push(<div key={'dummy' + index}>{data.id} - {data.name}</div>);
    });

    return (
        <div>
            <h1>Counter</h1>
            <p>This is a simple example of a React component.</p>

            <p aria-live="polite">
                Current count: <strong>{currentCount}</strong>
            </p>

            <button className="btn btn-primary"
                    onClick={incrementCounter}>
                Increment
            </button>

            <button className="btn btn-primary"
                    onClick={populateUiWithDatabaseDummyData}>
                Populate UI dummy data from database
            </button>

            {items}
        </div>
    )
}

//== types ============================================================================================================

export class DummyData {
    id: number;

    name: string;

    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}