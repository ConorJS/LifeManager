CREATE ROLE lmadmin WITH LOGIN PASSWORD 'lfemgr';

CREATE DATABASE lifemanager
    WITH OWNER lmadmin
    TEMPLATE template0
    ENCODING UTF8
    TABLESPACE pg_default
    LC_COLLATE 'C'
    LC_CTYPE 'C'
    CONNECTION LIMIT -1;
	