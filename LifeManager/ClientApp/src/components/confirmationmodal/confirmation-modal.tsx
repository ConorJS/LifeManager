import React, {FunctionComponent} from "react";
import {LmModal} from "../modal/lm-modal";
import './confirmation-modal.scss';

interface ConfirmationModalProps {
    acceptBehaviour: () => void,
    rejectionBehaviour: () => void,
    warningMessage: string;
}

export const ConfirmationModal: FunctionComponent<ConfirmationModalProps> = (props: ConfirmationModalProps) => {
    return (
        <LmModal handleClose={props.rejectionBehaviour} widthPixels={350} heightPixels={275}>
            <div className="confirmation-modal-elements-container">
                <div className="confirmation-modal-message">
                    {props.warningMessage}
                </div>
                
                <div className="confirmation-modal-buttons-container">
                    <button className="btn lm-button positive modal-button"
                            onClick={props.acceptBehaviour}>
                        Yes
                    </button>

                    <button className="btn lm-button negative modal-button"
                            onClick={props.rejectionBehaviour}>
                        No
                    </button>
                </div>
            </div>
        </LmModal>
    )
}