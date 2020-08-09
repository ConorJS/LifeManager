#!/bin/bash

# ===== Imports
. Environment/Utils.sh

# ===== Execution

# %PROJECT%/Environment
cd Environment
./CreateDatabase.sh
exit_if_error_code $? 'Setting up the LifeManager PostgreSQL database'
