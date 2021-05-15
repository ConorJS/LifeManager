SET search_path TO lifemanager;

CREATE TABLE "UserConfigurationEntity"
(
    "UserConfigurationEntityId"         INT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "UserId"                            INT     NOT NULL,
    "DateTimeCreated"                   TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified"              TIMESTAMP WITHOUT TIME ZONE,
    "ToDoTaskHideCompletedAndCancelled" BOOLEAN NOT NULL DEFAULT FALSE
);

ALTER TABLE "UserConfigurationEntity"
    ADD CONSTRAINT "FK_UserId_UserConfigurationEntity" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

-- TODO: Remove
-- CREATE TABLE "UserConfiguration_ColumnSortOrder"
-- (
--     "TableName"           VARCHAR(255) NOT NULL,
--     "ColumnName"          VARCHAR(255) NOT NULL,
--     "Precedence"          INT          NOT NULL,
--     "UserConfigurationId" BIGINT       NOT NULL DEFAULT -1
-- );
-- TODO: Remove

-- ALTER TABLE "UserConfiguration_ColumnSortOrder"
--     ADD CONSTRAINT "FK_UserConfiguration_ColumnSortOrder" FOREIGN KEY ("UserConfigurationId") REFERENCES "UserConfiguration" ("Id");

INSERT INTO lifemanager."UserConfigurationEntity"("UserId",
                                                  "DateTimeCreated",
                                                  "DateTimeLastModified",
                                                  "ToDoTaskHideCompletedAndCancelled")
VALUES (1, now(), now(), false);
