import {getCalculationServiceUrl, getLookupUrl} from "./ApiUrlResolver";

export async function getCalculationTypes(){
    try{
        const response = await fetch(getLookupUrl());
        if (response.ok)
            return new ApiResult(true, response.status, await response.json());
        else
            return new ApiResult(false, response.status, await response.text());
    } catch (ex){
        return new ApiResult(false,"", ex);
    }

}

export async function getCalculationResult(method, input1, input2){
    try{
        const response = await fetch(getCalculationServiceUrl(method, input1, input2));
        const jsonResponse = await response.json();
        if (response.ok)
            return new ApiResult(true, response.status, jsonResponse)
        else
            return new ApiResult(false, response.status, jsonResponse.errorMessage)
    } catch (ex){
        return new ApiResult(false,"", ex);
    }

}

export class ApiResult{
    constructor(success, status, data) {
        this.successful = success;
        this.status = status;
        this.data = data;
    }
}