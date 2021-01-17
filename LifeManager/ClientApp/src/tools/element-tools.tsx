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
}