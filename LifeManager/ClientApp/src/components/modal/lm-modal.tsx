import Modal from "./modal";
import React, {CSSProperties} from "react";
import './lm-modal.scss';

interface LmModalProps {
    children: any;
    handleClose: Function;
    widthPixels?: number;
    heightPixels?: number;
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
        let dimensionsStyle: CSSProperties = {
            width: !this.props.widthPixels ? 400 : this.props.widthPixels,
            height: !this.props.heightPixels ? 500 : this.props.heightPixels
        }

        return (
            <Modal>
                <div className="wrapper">
                    <div className="inner">
                        <button className="close" onClick={() => this.props.handleClose()}>
                            X
                        </button>

                        <div className="modal-container" style={dimensionsStyle}>
                            {this.props.children}
                        </div>
                    </div>
                </div>
            </Modal>
        );
    }
}
