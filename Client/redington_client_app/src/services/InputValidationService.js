export const IsValidNumber = (input) =>{
    if (!input)
        return false;
    const result = parseFloat(input);
    return (result >= 0 && result <= 1);
}