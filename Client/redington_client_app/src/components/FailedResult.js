import React from "react";
import {Alert, AlertTitle} from "@material-ui/lab";

export function FailedResult(props) {
    return (
        props.visible &&
        <Alert severity="error">
            <AlertTitle>Error</AlertTitle>
            Something went wrong: <strong>{props.errorCode}</strong><br />
            {props.errorMessage}
        </Alert>
    );
}