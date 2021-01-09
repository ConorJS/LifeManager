import * as React from 'react';
import * as ReactDOM from "react-dom";
import {ObjectTools} from '../../tools/object-tools'
import * as PropTypes from "prop-types";

export default class Modal extends React.PureComponent {
    static propTypes = {
        children: PropTypes.node
    }

    private readonly container: HTMLDivElement = document.createElement('div');

    private readonly modalRoot: HTMLElement;

    public constructor(props: any) {
        super(props);

        this.modalRoot = ObjectTools.assignOrThrow(document.getElementById('root'),
            "Failed to initialise Modal component; DOM root reference could not be found.");
    }

    public componentDidMount(): void {
        this.modalRoot.appendChild(this.container);
    }

    public componentWillUnmount(): void {
        this.modalRoot.removeChild(this.container);
    }

    public render(): React.ReactElement {
        return ReactDOM.createPortal(this.props.children, this.container);
    }
}