DROP DATABASE LifeManager;
DROP ROLE lmadmin;

-- Create the LMAdmin user
CREATE ROLE lmadmin;

CREATE DATABASE lifemanager 
   WITH OWNER lmadmin 
   TEMPLATE template0 -- Not really using templating, but t1 and t0 are the same by default, so this doesn't matter
   ENCODING 'UTF8' -- What else? 
   TABLESPACE  pg_default -- Not so important when everything is on SSDs; this is for controlling physical location of data on the disk
   LC_COLLATE  'C' 
   LC_CTYPE  'C' -- https://dba.stackexchange.com/questions/94887/what-is-the-impact-of-lc-ctype-on-a-postgresql-database
   CONNECTION LIMIT  -1;