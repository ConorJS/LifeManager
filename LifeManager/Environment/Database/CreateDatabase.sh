#!/bin/bash

# ===== Imports
. ../Utils.sh

# ===== Execution
fail_if_not_command psql "PostgreSQL must be installed create/run the LifeManager database."

cd ../.. # Changing directory to: project

database_path=
if platform_is_linux; then
  database_path="$(pwd)/LocalEnv/postgres/data"
elif platform_is_windows; then
  database_path=$(convert_mingw_path_to_windows "$(pwd)/LocalEnv/postgres/data")
else 
  error_if_unknown_platform 'Determining PostgreSQL database location'
fi
echo "Path to database: $database_path"

cd Environment/Database || exit # Changing directory to: project/Environment/Database

# Create the database if it doesn't exist
PGPASSWORD=lfemgr
PGUSER=lmadmin
mkdir "$database_path"
psql -d postgres -f ../Database/init.sql -v "ON_ERROR_STOP=1" -v v1="'$database_path'" || exit_if_error_code $? 'Running the the LifeManager Postgres database creation SQL script' 
PGPASSWORD=

echo
echo "Successfully created LifeManager Postgres database."
