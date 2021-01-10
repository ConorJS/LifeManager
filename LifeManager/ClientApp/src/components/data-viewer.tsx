import React, {FunctionComponent, SyntheticEvent, useState} from 'react';

interface DataViewerProps {
}

export const DataViewer: FunctionComponent<DataViewerProps> = props => {
    const [currentCount, setCurrentCount] = useState(0);
    const [dummyData, setDummyData] = useState<DummyData[]>([]);
    const [inputId, setInputId] = useState("");
    const [inputName, setInputName] = useState("");

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

    const saveDummyDataToDatabase = async () => {
        if (!inputId || !inputName) {
            return;
        }

        const dummyData = new DummyData(Number.parseInt(inputId), inputName);

        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(dummyData)
        };

        fetch('dummydata', requestOptions)
            .then(response => response.json())
            .then(data => console.log(data));
    }

    function idChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setInputId(target.value);
    }

    function nameChangeHandler(changeEvent: SyntheticEvent): void {
        const target = changeEvent.target as HTMLInputElement;
        setInputName(target.value);
    }

    let items: JSX.Element[] = [];
    dummyData.forEach((data, index) => {
        items.push(<div key={'dummy' + index}>{data.id} - {data.name}</div>);
    });

    return (
        <div>
            <h1>Database test screen only</h1>
            <p>Simple dummy data CRUD application.</p>

            <p aria-live="polite">
                Current count: <strong>{currentCount}</strong>
            </p>

            <button className="btn lm-button positive"
                    onClick={incrementCounter}>
                Increment
            </button>

            <button className="btn lm-button positive"
                    onClick={populateUiWithDatabaseDummyData}>
                Populate UI dummy data from database
            </button>

            <div>
                <button className="btn lm-button positive"
                        onClick={saveDummyDataToDatabase}>
                    Save dummy data in fields to database
                </button>
                <form>
                    <label htmlFor="dummy-id">Id</label>
                    <input id="dummy-id" type="number" value={inputId} onChange={idChangeHandler}/>

                    <label htmlFor="dummy-name">Name</label>
                    <input id="dummy-name" type="text" value={inputName} onChange={nameChangeHandler}/>
                </form>
            </div>

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