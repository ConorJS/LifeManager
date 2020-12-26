#!/bin/bash

# ===== Imports
. ../Utils.sh

# ===== Execution
fail_if_not_command psql "PostgreSQL must be installed create/run the LifeManager database."

cd ../.. # Changing directory to: project
windows_path=$(convert_mingw_path_to_windows "$(pwd)/LocalEnv/postgres/data")
echo "Path to database: $windows_path"
cd Environment/Database || exit # Changing directory to: project/Environment/Database

# Create the database if it doesn't exist
PGPASSWORD=lfemgr
PGUSER=conor
mkdir "$windows_path"
psql -d postgres -f ../Database/createDatabase.sql -v "ON_ERROR_STOP=1" -v v1="'$windows_path'" || exit_if_error_code $? 'Running the the LifeManager Postgres database creation SQL script' 
PGPASSWORD=

echo
echo "Successfully created LifeManager Postgres database."
