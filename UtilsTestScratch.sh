#!/bin/bash

#
# Scratch space for testing bash scripts
#

# == Imports ==========================================================================================================

. Environment/Utils.sh
. Environment/TestUtils.sh

# == Scratch tests ====================================================================================================

exit_if_error_code 1 'test' '' "Check task manager. A PostgreSQL instance may already be running, potentially owned by another user."