import Typography from "@material-ui/core/Typography";
import React from "react";
import {Alert, AlertTitle} from "@material-ui/lab";

export function Result(props) {
    return (
        props.visible &&
        <Alert severity="success">
            <AlertTitle>Result</AlertTitle>
            The result of {props.method} of {props.firstInput} and {props.secondInput} is <strong>{props.result}</strong>
        </Alert>
    );
}