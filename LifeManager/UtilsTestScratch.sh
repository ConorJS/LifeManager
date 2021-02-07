#!/bin/bash

#
# Scratch space for testing bash scripts
#

# == Imports ==========================================================================================================

. Environment/Utils.sh
. Environment/TestUtils.sh

# == Scratch tests ====================================================================================================

"convert_mingw_path_to_windows '$(pwd)'"
