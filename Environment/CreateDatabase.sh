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

# Create the database if it doesn't exist
PGPASSWORD=lfemgr
psql -U postgres -f ../Database/createDatabase.sql

# Download and extract Flyway. 

echo "$wd"
download_and_extract_to $FLYWAY_URL $FLYWAY_INSTALLATION_DIR Flyway $FLYWAY_HASH


