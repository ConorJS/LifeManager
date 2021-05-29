import {StringTools} from "./string-tools";

export class ElementTools {
    /**
     * Creates an ID for a collection element.
     *
     * This creates a similar format ID to {@link makeListElementId}, sans member ID segment.
     *
     * The ID pattern is <Parent description>@<Parent id>
     * Example: MyContainer@MyNewTable
     *
     * @param parentName Describes the parent.
     * @param parentId   Identifies the parent (i.e. the collection container).
     *                   Should uniquely describe the use case of the parent. A hash would be safest,
     *                   but could also be a description of the use case.
     *                   Should be re-used for each member using this method.
     */
    public static makeContainerElementId(parentName: string, parentId: string): string {
        return `${parentName}@${StringTools.generateId()}`;
    }
    
    /**
     * TODO: This doesn't use 'parentId', but perhaps it should, if multiple methods call this with the same parentName?
     * Creates an ID for a member of a collection element.
     *
     * This achieves three goals:
     * 1) Guarantees (insofar as parentName is unique within the application) unique ids. Members of collections are encouraged to have unique IDs.
     * 2) Fully describes the collection member in an ID. Even out of context, it should be able to accurately describe the what/where of the item.
     * 3) Makes it easier to standardise the format of collection member IDs.
     *
     * The ID pattern is <Parent description>@<Parent id>-<Member index>
     * Example: MyContainer@MyNewTable-#5
     *
     * @param parentName Describes the parent.
     * @param parentId   Identifies the parent (i.e. the collection container).
     *                   Should uniquely describe the use case of the parent. A hash would be safest,
     *                   but could also be a description of the use case.
     *                   Should be re-used for each member using this method.
     * @param memberId   Identifies the member within the collection. A number would be the index, a string would be a (unique) descriptor.
     */
    public static makeListElementId(parentName: string, parentId: string, memberId: number | string): string {
        return `${parentName}@${StringTools.generateId()}-#${memberId}`;
    }

    /**
     * Get the width of text if it were to be rendered.
     * 
     * TODO: This doesn't allow choosing a font size, so the returned text width value will be based on the default font size for an 
     *  element created off the root node of the DOM.
     * 
     * @param inputText The text.
     */
    public static getTextWidth(inputText?: string): number {
        const container = document.createElement('canvas');
        
        let width = 0;
        let text = inputText ?? '';
        text = text.toString();

        let context = container.getContext('2d');

        if (context) {
            context.font = window
                .getComputedStyle(document.body)
                .getPropertyValue('font');
            width = context.measureText(text).width;
            return width;
            
        } else {
            throw Error(`Could not set up a canvas to test text width for: ${inputText}`);
        }
    }

    /**
     * Truncates text to fit within a certain width. The trimmed text is also passed back as a separate string.
     * 
     * @param inputText The text.
     * @param width The width (in pixels).
     * @param truncateOnWord If set, then instead of truncating in the middle of a word, we walk back to the most recent word, and truncate there.
     * @return A tuple consisting of the truncated string, and the remainder of the string which was cut.
     */
    public static truncateTextToFitInWidth(inputText: string, width: number, truncateOnWord?: boolean): { truncated: string, cut: string} {
        let truncatedText: string = inputText;
        while (true) {
            let textWidth: number = this.getTextWidth(truncatedText);
            
            if (textWidth < width) {
                if (truncateOnWord && inputText !== truncatedText) {
                    for (let i = truncatedText.length; i >= 0; i--) {
                        if (truncatedText.charAt(i) === ' ') {
                            return {truncated: inputText.substring(0, i), cut: inputText.substr(i)};
                        }
                    }
                }
                
                // If we aren't truncating at the end of a word (or we wanted to, but the string contained no whitespace i.e. one whole word).
                return {truncated: truncatedText, cut: inputText.substr(truncatedText.length)};
                
            } else {
                truncatedText = truncatedText.substring(0, truncatedText.length * width / textWidth);
            }
        }
    }
}