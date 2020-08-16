#!/bin/bash

# ===== Imports
. Utils.sh

# ===== Constants
readonly FLYWAY_URL=https://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/6.5.1/flyway-commandline-6.5.1-windows-x64.zip 
readonly FLYWAY_URL_TEST=ftp://192.168.1.16:2121/DCIM/test.7z
readonly FLYWAY_INSTALLATION_DIR=../LocalEnv/FlywayInstallation
readonly FLYWAY_HASH=F2FB37C4
readonly FLYWAY_HASH_TEST=6A5B00CA

# ===== Execution
fail_if_not_command psql "PostgreSQL must be installed create/run the LifeManager database."

cd .. # Changing directory to: project/
windows_path=$(convert_mingw_path_to_windows "`pwd`/LocalEnv/postgres/data")
echo "Path to database: $windows_path"
cd Environment # Changing directory to: project/Environment

# Create the database if it doesn't exist
PGPASSWORD=lfemgr
PGUSER=conor
mkdir "$windows_path"
psql -d postgres -f ../Database/createDatabase.sql -v "ON_ERROR_STOP=1" -v v1="'$windows_path'"
PGPASSWORD=
exit_if_error_code $? 'Running the the LifeManager Postgres database creation SQL script'

echo
echo "Successfully created LifeManager Postgres database."
