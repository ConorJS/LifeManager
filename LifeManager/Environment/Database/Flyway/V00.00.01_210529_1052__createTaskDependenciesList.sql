SET search_path TO lifemanager;

CREATE TABLE "ToDoTask_Dependency"
(
    "ToDoTaskEntityId"     BIGINT NOT NULL,
    "ToDoTaskDependencyId" BIGINT NOT NULL,
    PRIMARY KEY ("ToDoTaskEntityId", "ToDoTaskDependencyId")
);

ALTER TABLE "ToDoTask_Dependency"
    ADD CONSTRAINT "UK_ToDoTask_Dependency" UNIQUE ("ToDoTaskEntityId", "ToDoTaskDependencyId"),
    ADD CONSTRAINT "FK_ToDoTask_Dependency_Owner" FOREIGN KEY ("ToDoTaskEntityId") REFERENCES "ToDoTask" ("Id"),
    ADD CONSTRAINT "FK_ToDoTask_Dependency_Dependency" FOREIGN KEY ("ToDoTaskDependencyId") REFERENCES "ToDoTask" ("Id");
