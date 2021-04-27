FROM flyway/flyway:7.7.0-alpine

COPY ./ /flyway/sql

ENTRYPOINT [ "sh" ]
CMD ["-c", "flyway -url=jdbc:postgresql://lifemanager-postgres-service:5432/lifemanager -user=lmadmin -password=lfemgr -defaultSchema=public -schemas=lifemanager,public -validateMigrationNaming=true migrate"]