export function getLookupUrl(){
    return 'http://localhost:49626/api/Calculation/types';
}
export function getCalculationServiceUrl(method, input1, input2){
    return `http://localhost:49626/api/Calculation/calc/${method}/${input1}/${input2}`;
}