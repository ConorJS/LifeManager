#!/bin/bash

#
# Automatic bash script tests 
#

# ===== Imports
. Environment/Utils.sh

# ===== Methods

# Asserts two values are the same.
#
# If they are not, the program will exit with an appropriate error message.
#
# 1 The expected value.
# 2 The actual value.
# 3 The name of the test.
function assert_equals {
	expected=$1
	actual=$2
	test_name=$3
	
    validate_arg_count $# ${FUNCNAME[0]} 2 3
	
	if [ -z "$test_name" ]; then
	    test_name='Unnamed test'
	fi
	
	if [[ ! "$expected" == "$actual" ]]; then 
	    echo ""
		echo "[assert_equals] Assertion failure in '$test_name':" 
		echo "Expected: '$expected'" 
		echo "  Actual: '$actual'"
		exit
	fi
}

# ===== Tests

echo 'Testing: split_string_and_get_nth'
assert_equals '123123' "$(split_string_and_get_nth 'a b c d 123123' ' ' 4)" "Nth is last"

echo 'Testing: split_string_and_get_last'
assert_equals '123123' "$(split_string_and_get_last 'a b c d 123123' ' ')" "Split by space"
assert_equals '123123' "$(split_string_and_get_last 'a/b/c/d/123123' '/')" "Split by forward slash"

echo 'Testing: convert_mingw_path_to_windows'
assert_equals 'c:/whatever' "$(convert_mingw_path_to_windows 'c/whatever')" "MinGW path no leading forward slash"
assert_equals 'c:/whatever' "$(convert_mingw_path_to_windows '/c/whatever')" "MinGW path with leading forward slash"
assert_equals 'c:/whatever' "$(convert_mingw_path_to_windows 'c:/whatever')" "Already Windows-style path"

echo 'All tests passed.'