SET search_path TO lifemanager;

CREATE TABLE "Appointment"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535),
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "Chore"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535),
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "LeisureActivity"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535)
);

CREATE TABLE "Principle"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535)
);

CREATE TABLE "RecurringTask"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535),
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "ToDoTask"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY UNIQUE,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "Active"               BOOL         NOT NULL,
    "Name"                 VARCHAR(255) NOT NULL,
    "Status"               VARCHAR(255) NOT NULL,
    "Comments"             VARCHAR(65535),
    "RelativeSize"         INT          NOT NULL,
    "Priority"             INT          NOT NULL
);
