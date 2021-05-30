export class StateTools {
    /**
     * Updates a specific element of an array, where the array is a field of a state object.
     *
     * Adapted from https://stackoverflow.com/a/49502115.
     *
     * @param object     The state object.
     * @param setState   The setter for the state object.
     * @param fieldName  The name of the array field on the state object.
     * @param newElement The new, replacement element. Optional - if not provided, we just remove the element.
     * @param index      The index of the element to be replaced. Optional - if not provided, we just add to the array.
     */
    public static updateArray<T>(object: T, setState: (t: T) => void, fieldName: string,
                                 newElement: any, index?: number): void {
        // @ts-ignore
        const expectedArray: any = object[fieldName];
        if (expectedArray === undefined || !Array.isArray(expectedArray)) {
            throw Error(`${object} does not have a array attribute named '${fieldName}'`);
        }
        const array: any[] = expectedArray as any[];

        // Make a shallow copy of the items
        const items: any[] = [...array];

        // Make a shallow copy of the item you want to mutate
        if (index !== undefined && index !== -1) {
            if (!!newElement) {
                items[index] = newElement;
            } else {
                items.splice(index, 1);
            }
            
        } else if (!!newElement) {
            items.push(newElement);
        }

        // Set the state to our new copy, with the replaced element.
        
        const newObjectState: T = {...object};
        // @ts-ignore
        newObjectState[fieldName] = items;
        
        setState(newObjectState);
    }
}