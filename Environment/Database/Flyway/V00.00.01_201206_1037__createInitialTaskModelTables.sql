SET search_path TO lifemanager;

CREATE TABLE "Appointment"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "Chore"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "LeisureActivity"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE
);

CREATE TABLE "Principle"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE
);

CREATE TABLE "RecurringTask"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "RelativeSize"         INT          NOT NULL
);

CREATE TABLE "ToDoTask"
(
    "Id"                   BIGINT GENERATED ALWAYS AS IDENTITY,
    "Name"                 VARCHAR(255) NOT NULL,
    "DateTimeCreated"      TIMESTAMP WITHOUT TIME ZONE,
    "DateTimeLastModified" TIMESTAMP WITHOUT TIME ZONE,
    "RelativeSize"         INT          NOT NULL
);
