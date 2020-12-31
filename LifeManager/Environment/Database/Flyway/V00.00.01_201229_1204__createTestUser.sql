SET search_path TO lifemanager;

INSERT INTO "User"("DisplayName", "DateTimeCreated", "DateTimeLastModified")
VALUES ('TestUser', now(), now());