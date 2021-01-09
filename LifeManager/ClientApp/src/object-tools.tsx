export class ObjectTools {
    /**
     * Produces the supplied object, only if it is defined. Otherwise, produces an {@link Error}. 
     * 
     * @param object The object.
     * @param message The message to use with the Error, if the object is not defined.
     */
    public static assignOrThrow<T>(object: T | undefined, message?: string): any {
        if (!object) {
            throw new Error(message ?? "Object was undefined.");
        }

        return object;
    }
}