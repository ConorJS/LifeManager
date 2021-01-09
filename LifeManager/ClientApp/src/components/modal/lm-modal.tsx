import Modal from "./modal";
import React from "react";
import './lm-modal.scss';

interface LmModalProps {
    children: any;
    handleClose: Function;
}

export class LmModal extends React.Component<LmModalProps, any> {
    constructor(props: LmModalProps) {
        super(props);
        this.escFunction = this.escFunction.bind(this);
    }

    escFunction(event: KeyboardEvent) {
        if (event.code === 'Escape') {
            this.props.handleClose();
        }
    }

    componentDidMount() {
        document.addEventListener("keydown", this.escFunction, false);
    }

    componentWillUnmount() {
        document.removeEventListener("keydown", this.escFunction, false);
    }

    render() {
        return (
            <Modal>
                <div className="wrapper">
                    <div className="inner">
                        <button className="close" onClick={() => this.props.handleClose()}>
                            X
                        </button>

                        {this.props.children}
                    </div>
                </div>
            </Modal>
        );
    }
}
