import TextField from "@material-ui/core/TextField";
import {MenuItem} from "@material-ui/core";
import React from "react";

export function Selector(props) {
    return <TextField variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      name="method"
                      label="Method"
                      value={props.selected}
                      onChange={props.onChange}
                      select>
        {props.items.map(item => <MenuItem value={item} key={item}>{item}</MenuItem>)}
    </TextField>;
}