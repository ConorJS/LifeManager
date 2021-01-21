export class StringTools {
    /**
     * Generates an ID using random alphanumeric characters.
     *
     * @param idLength The length the ID should be. Optional, if not provided, then 8 will be used.
     * @return The generated ID.
     */
    public static generateId(idLength?: number): string {
        const useIdLength = idLength ?? 8;
        const characters: string = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        const charactersLength: number = characters.length;

        let result: string = '';
        for (let i = 0; i < useIdLength; i++) {
            result += characters.charAt(Math.floor(Math.random() * charactersLength));
        }

        return result;
    }

    /**
     * Formats an array as a single string.
     * 
     * e.g. A map with values 1 -> 'A' and 2 -> 'B' would show as [1, A][2, B]
     * 
     * @param map The map.
     * @return The formatted map, as a string.
     */
    public static printMap<K, V>(map: Map<K, V>): string {
        let output = '';
        map.forEach((key, value) => {
            output += `[${key}, ${value}]`;
        });
        return output;
    }
}