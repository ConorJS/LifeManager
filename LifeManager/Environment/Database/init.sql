DO $$
    BEGIN
        CREATE ROLE lmadmin WITH LOGIN PASSWORD 'lfemgr';
    EXCEPTION WHEN DUPLICATE_OBJECT THEN
        RAISE NOTICE 'Not creating role lmadmin - it already exists.';
    END
$$;

CREATE DATABASE lifemanager
    WITH OWNER lmadmin
    TEMPLATE template0
    ENCODING UTF8
    TABLESPACE pg_default
    LC_COLLATE 'C'
    LC_CTYPE 'C'
    CONNECTION LIMIT -1;
	