import React, { Component } from 'react';

interface TestDatabaseState {
    currentCount: number;
}

export class TestDatabase extends Component<{}, TestDatabaseState> {
    //== init =========================================================================================================
    
    constructor(props: any) {
        super(props);
        this.state = { currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    //== render =======================================================================================================

    render() {
        return (
            <div>
                <h1>Counter</h1>

                <p>This is a simple example of a React component.</p>

                <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

                <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
            </div>
        );
    }
    
    //== methods ======================================================================================================

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }
}
