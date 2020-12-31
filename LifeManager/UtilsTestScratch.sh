#!/bin/bash

#
# Scratch space for testing bash scripts
#

# == Imports ==========================================================================================================

. Environment/Utils.sh
. Environment/TestUtils.sh

# == Scratch tests ====================================================================================================

exit_if_error_code $? 'test' '' 1 2 "3t s"