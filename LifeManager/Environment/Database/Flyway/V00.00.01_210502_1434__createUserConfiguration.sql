SET search_path TO lifemanager;

CREATE TABLE "UserConfiguration"
(
    "Id"                                BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "UserId"                            BIGINT  NOT NULL,
    "DateTimeCreated"                   TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified"              TIMESTAMP WITHOUT TIME ZONE,
    "ToDoTaskHideCompletedAndCancelled" BOOLEAN NOT NULL DEFAULT FALSE
);

ALTER TABLE "UserConfiguration"
    ADD CONSTRAINT "FK_UserId_UserConfiguration" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

CREATE TABLE "UserConfiguration_ColumnSortOrder"
(
    "UserConfigurationId" BIGINT       NOT NULL,
    "TableName"           VARCHAR(255) NOT NULL,
    "ColumnName"          VARCHAR(255) NOT NULL,
    "IsSortedAscending"   BOOLEAN      NOT NULL,
    "Precedence"          INT          NOT NULL
);

ALTER TABLE "UserConfiguration_ColumnSortOrder"
    ADD CONSTRAINT "FK_UserConfiguration_ColumnSortOrder" FOREIGN KEY ("UserConfigurationId") REFERENCES "UserConfiguration" ("Id");


-- Dummy values only. These should be removed when Users are fully implemented (i.e. when multiple may exist)
INSERT INTO lifemanager."UserConfiguration"("UserId", "DateTimeCreated", "DateTimeLastModified",
                                            "ToDoTaskHideCompletedAndCancelled")
VALUES (1, now(), now(), false);

