#!/bin/bash

#
# Runs all tests (manually verified tests last)
#

# ===== Imports
. Environment/Utils.sh

# ===== Test scripts

clear

print_marker "Tests (simple assertions)"
. UtilsTest.sh

print_marker "Tests (user input tests)"
. UtilsTestUserInput.sh

print_marker "Tests (manually verified tests)"
. UtilsTestManuallyVerified.sh
