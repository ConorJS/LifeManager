DROP DATABASE IF EXISTS LifeManager;
DROP ROLE IF EXISTS lmadmin;
DROP TABLESPACE IF EXISTS environmenttablespace;

-- Create the LMAdmin user
CREATE ROLE lmadmin;

-- The tablespace location only accepts absolute paths
CREATE TABLESPACE environmenttablespace LOCATION :v1; --'c:/pg-db'; --'/LocalEnv/pg-db';

CREATE DATABASE lifemanager 
   WITH OWNER lmadmin 
   TEMPLATE template0 -- Not really using templating, but t1 and t0 are the same by default, so this doesn't matter
   ENCODING 'UTF8' -- What else? 
   TABLESPACE environmenttablespace
   LC_COLLATE  'C' 
   LC_CTYPE  'C' -- https://dba.stackexchange.com/questions/94887/what-is-the-impact-of-lc-ctype-on-a-postgresql-database
   CONNECTION LIMIT  -1;