import React from "react";
import TextField from "@material-ui/core/TextField";
import {Alert} from "@material-ui/lab";

export function NumberInput(props) {
    return <React.Fragment>
        <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            label="Enter First Probability"
            name="email"
            type="number"
            inputProps={{ min: "0", max: "1", step: "0.1" }}
            autoFocus={props.focused}
            onChange={props.onChange}
            error={props.error}
            value={props.value}/>
        {
            props.error &&
            <Alert severity="error">The value should be in the the range of 0 to 1</Alert>
        }
    </React.Fragment>
}