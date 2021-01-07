import Modal from "../modal";
import React from "react";
import './modal-impl.scss';

interface ModalImplProps {
    children: any;
    handleClose: Function;
}

export class ModalImpl extends React.Component<ModalImplProps, any> {
    constructor(props: ModalImplProps){
        super(props);
        this.escFunction = this.escFunction.bind(this);
    }
    
    escFunction(event: KeyboardEvent) {
        if (event.code === 'Escape') {
            this.props.handleClose();
        }
    }
    
    componentDidMount(){
        document.addEventListener("keydown", this.escFunction, false);
    }
    
    componentWillUnmount(){
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
