FROM flyway/flyway:7.7.0-alpine

COPY ./ /flyway/sql

ENTRYPOINT [ "sh" ]
CMD ["-c", "flyway -url=jdbc:postgresql://lifemanager-db/lifemanager -user=lmadmin -password=lfemgr -schemas=lifemanager migrate"]