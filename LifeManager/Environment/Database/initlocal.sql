DROP DATABASE IF EXISTS LifeManager;
DROP ROLE IF EXISTS postgres;
DROP ROLE IF EXISTS lmadmin;
DROP TABLESPACE IF EXISTS environmenttablespace;

-- Create the LMAdmin user and default user
CREATE ROLE postgres;
CREATE ROLE lmadmin;
ALTER ROLE postgres WITH LOGIN;
ALTER ROLE lmadmin WITH LOGIN;

-- The tablespace location only accepts absolute paths
CREATE TABLESPACE environmenttablespace LOCATION :v1;

CREATE DATABASE lifemanager 
    WITH OWNER lmadmin
    TEMPLATE template0
    ENCODING UTF8
    TABLESPACE environmenttablespace
    LC_COLLATE 'C'
    LC_CTYPE 'C'
    CONNECTION LIMIT -1;