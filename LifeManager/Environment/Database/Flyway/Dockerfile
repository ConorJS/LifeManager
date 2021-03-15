FROM flyway/flyway:7.7.0-alpine

COPY ./event-db /flyway/sql

ENTRYPOINT [ "sh" ]
CMD ["-c", "flyway -url=jdbc:postgresql://localhost/lifemanager -user=lmadmin -password=lfemgr -schemas=lifemanager migrate"]