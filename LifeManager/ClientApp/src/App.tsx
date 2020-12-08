import React, {FunctionComponent} from 'react';
import {DataViewer} from './components/data-viewer';

import './custom.css'

interface AppProps {
}

export const App: FunctionComponent<AppProps> = props => {
    return (
        <DataViewer/>
    );
}
