import React, {FunctionComponent} from "react";
import {LmModal} from "../lm-modal";

interface ErrorModalProps {
    handleClose: () => void,
    errorMessage: string;
}

export const ErrorModal: FunctionComponent<ErrorModalProps> = (props: ErrorModalProps) => {
    return (
        <LmModal handleClose={props.handleClose} widthPixels={225} heightPixels={150}>
            <div className="confirmation-modal-elements-container">
                <div className="confirmation-modal-message">
                    {props.errorMessage}
                </div>

                <div className="confirmation-modal-buttons-container">
                    <button className="btn lm-button negative modal-button"
                            onClick={props.handleClose}>
                        Back
                    </button>
                </div>
            </div>
        </LmModal>
    )
}