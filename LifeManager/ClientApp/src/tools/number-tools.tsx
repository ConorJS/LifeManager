export class NumberTools {
    /**
     * Compares two numbers.
     *
     * @param a
     * @param b
     */
    public static compare(a: number, b: number): number {
        if (a > b) {
            return 1;
        }

        if (b > a) {
            return -1;
        }

        return 0;
    }
}