import {StringTools} from "./string-tools";

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

    /**
     * Finds the key corresponding with the first value in a map matching the supplied value.
     * Sort of the reverse of Map.get(), except it won't work in a predictable manner for maps with duplicate values for different keys.
     *
     * @param findValue The value.
     * @param map The map to search in.
     */
    public static firstKeyWithValue<K, V>(findValue: V, map: Map<K, V>): K {
        if (map.size === 0) {
            throw Error(`Could not find number for string ${findValue} - empty map supplied.`);
        }

        // This never gets used (and must exist, given the map is not empty), 
        // it just initialises the value so the compiler knows we can always use it.
        let foundKey: K = map.keys().next().value;

        let searching: boolean = true;
        map.forEach((value, key) => {
            if (searching && findValue === value) {
                foundKey = key;
                searching = false;
            }
        });

        if (searching) {
            throw Error(`Could not find number for string ${findValue} in map ${StringTools.printMap(map)}`);

        } else {
            return foundKey;
        }
    }

    /**
     * Gets a value from a map with a key, guaranteeing a define value.
     * If the value does not exist, then throws an error.
     *
     * @param map The map.
     * @param key The key to access the map with.
     */
    public static getOrFail<K, V>(map: Map<K, V>, key: K): V {
        let value: V | undefined = map.get(key);

        if (value === undefined) {
            let extraWarning: string = map.has(key) ? '. The map seems to have this key though, but the value is undefined.' : '';
            throw Error(`No key '${key}' in map ${StringTools.printMap(map)}${extraWarning}`);
        }

        return value;
    }
}