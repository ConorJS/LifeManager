#!/bin/bash

# ===== Imports =======================================================================================================
. Environment/Utils.sh


# ===== Variables =====================================================================================================
db_directory="$(pwd)/LocalEnv/postgres"
postgres_log_file="LocalEnv/postgres.log"


# ===== Create/Start PostgreSQL database ==============================================================================
status_output=$(pg_ctl status "-D$db_directory")
db_running=0
if [[ "${status_output}" =~ "pg_ctl: server is running" ]]; then
    print_marker "The database is already running."
    db_running=1
else
    print_marker "The database is not running."
fi

if [[ db_running -eq 0 ]]; then
    create_database=1
    if [[ -d $db_directory ]] && [[ ! $(folder_size "$db_directory") == "0" ]]; then
        prompt_options "The database directory already exists, and is not empty. " "Keep and use contents" "Delete and re-create" "Exit"
        selection=$?
        
        if [[ $selection -eq 1 ]]; then
            print_marker "Will continue, and use database already created in $db_directory."
            echo
            create_database=0
            
        elif [[ $selection -eq 2 ]]; then
            echo "Deleting contents of $db_directory..."
            rm -rf "$db_directory"
            
        elif [[ $selection -eq 3 ]]; then
            echo "Exiting..."
            exit 0
            
        else
            echo "Unknown selection value: $selection"
            exit 1
        fi
    fi
    
    if [[ create_database -eq 1 ]]; then
        print_marker "Initialising the database"
        
        pg_ctl initdb -U conor -D "$db_directory"
    fi
    
    print_marker "Starting the database server"
    
    echo "Logging Postgres server output to: $postgres_log_file"
    pg_ctl start -D "$db_directory" >> $postgres_log_file || exit_if_error_code $? 'Starting the LifeManager PostgreSQL database'
    echo "Successfully started Postgres database server."
    
    if [[ create_database -eq 1 ]]; then
        print_marker "Creating the database"
        
        cd Environment/Database || exit # Changing directory to: project/Environment/Database
        # If this fails due to a permission error on Windows, assign Edit permissions on the LocalEnv folder to 'NETWORK_SERVICE'.
        ./CreateDatabase.sh
        exit_if_error_code $? 'Setting up the LifeManager PostgreSQL database'
        
        cd ../.. # Changing directory to: /project/
    fi
fi

print_marker "Running Flyway migrations"
cd Environment/Database || exit # Changing directory to: project/Environment/Database
./MigrateDatabase.sh
cd ../.. # Changing directory to: /project/