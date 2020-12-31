SET search_path TO lifemanager;

CREATE TABLE "User"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DisplayName"          VARCHAR(1000) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE
);

ALTER TABLE "Appointment"
    ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "Appointment"
    ADD CONSTRAINT "FK_OwnedByUserId_Appointment" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");

ALTER TABLE "Chore"
    ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "Chore"
    ADD CONSTRAINT "FK_OwnedByUserId_Chore" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");

ALTER TABLE "LeisureActivity"
    ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "LeisureActivity"
    ADD CONSTRAINT "FK_OwnedByUserId_LeisureTask" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");

ALTER TABLE "RecurringTask"
    ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "RecurringTask"
    ADD CONSTRAINT "FK_OwnedByUserId_RecurringTask" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");

ALTER TABLE "Principle"
    ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "Principle"
    ADD CONSTRAINT "FK_OwnedByUserId_Principle" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");

ALTER TABLE "ToDoTask"
ADD COLUMN "OwnedByUserId" BIGINT NOT NULL DEFAULT -1;

ALTER TABLE "ToDoTask"
ADD CONSTRAINT "FK_OwnedByUserId_ToDoTask" FOREIGN KEY ("OwnedByUserId") REFERENCES "User" ("Id");