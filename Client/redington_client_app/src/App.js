import React, {useEffect, useState} from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import {makeStyles} from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import {Result} from "./components/Result";
import {getCalculationResult, getCalculationTypes} from "./services/CalculationServices";
import {IsValidNumber} from "./services/InputValidationService";
import {FailedResult} from "./components/FailedResult";
import {Selector} from "./components/Selector";
import {NumberInput} from "./components/NumberInput";

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%',
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

export default function App() {
    const classes = useStyles();
    const [methods, setMethods] = useState(["methods loading..."]);
    const [selectedMethod, setSelectedMethod] = useState(methods[0]);
    const [firstInput, setFirstInput] = useState({focused: true, value: 0.5, error: false});
    const [secondInput, setSecondInput] = useState({focused: false, value: 0.5, error: false});
    const [result, setResult] = useState({method:"", firstInput:"0.0", secondInput:"0.0", result:"0.0", visible:false});
    const [failedResult, setFailedResult] = useState({errorCode: 0, errorMessage: "", visible: false});
    const [submitButtonEnable, setSubmitButtonEnable] = useState(false);

    async function populateMethods(){
        const result = await getCalculationTypes();
        if (result.successful) {
            setMethods(result.data);
            setSelectedMethod(result.data[0]);
        }
    }
    async function onSubmit(event){
        event.preventDefault();
        const result = await getCalculationResult(selectedMethod, firstInput.value, secondInput.value);
        if (result.successful) {
            setResult({method: selectedMethod, firstInput: firstInput.value, secondInput: secondInput.value, result: result.data.result.value, visible: true});
            setFailedResult({...failedResult, visible: false});
        }
        else {
            setResult({visible:false});
            setFailedResult({errorCode: result.status, errorMessage: result.data, visible: true});
        }
    }
    function selectedMethodChanged(event){
        setSelectedMethod(event.target.value);
        enableOrDisableSubmitButton();
    }

    function enableOrDisableSubmitButton(){
        const shouldEnable = IsValidNumber(firstInput.value) && IsValidNumber(secondInput.value) && methods && methods.length > 0 && selectedMethod;
        setSubmitButtonEnable(!!shouldEnable);
    }

    function inputChanged(setMethod, event){
        if (IsValidNumber(event.target.value)){
            setMethod({focused: true, value: event.target.value, error: false});
        } else {
            setMethod({focused: true, value: event.target.value, error: true});
        }
    }

    function firstInputChanged(event){
        inputChanged(setFirstInput, event);
    }

    function secondInputChanged(event){
        inputChanged(setSecondInput, event);
    }
    useEffect( () => {
        populateMethods().then(r => enableOrDisableSubmitButton());
    }, []);
    useEffect(() => {
        enableOrDisableSubmitButton();
    }, [firstInput, secondInput, methods]);
    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline/>
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon/>
                </Avatar>
                <Typography component="h1" variant="h5">
                    Probability Calculator
                </Typography>
                <form className={classes.form} noValidate>
                    <NumberInput focused={firstInput.focused} value={firstInput.value} error={firstInput.error} onChange={firstInputChanged}/>
                    <NumberInput focused={secondInput.focused} value={secondInput.value} error={secondInput.error} onChange={secondInputChanged}/>
                    <Selector items={methods} selected={selectedMethod} onChange={selectedMethodChanged}/>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="secondary"
                        className={classes.submit}
                        disabled={!submitButtonEnable}
                        onClick={onSubmit}
                    >
                        Calculate
                    </Button>
                </form>
            </div>
            <Box mt={2}>
                <Result {...result}/>
                <FailedResult {...failedResult} />
            </Box>
        </Container>
    );
}
