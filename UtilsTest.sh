#!/bin/bash

#
# Automatic bash script tests 
#

# ===== Imports
. Environment/Utils.sh
. Environment/TestUtils.sh

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

echo 'Testing: upper_case'
assert_equals 'A' "$(upper_case 'a')" "Single-character uppercase conversion"
assert_equals 'AZ' "$(upper_case 'az')" "Multi-character uppercase conversion"
assert_equals 'AB' "$(upper_case 'aB')" "Handle uppercase letters"
assert_equals 'AB' "$(upper_case 'AB')" "Uppercase already"
assert_equals 'A B C #' "$(upper_case 'A b c #')" "Handle spaces and symbols"

echo 'Testing: lower_case'
assert_equals 'a' "$(lower_case 'A')" "Single-character lowercase conversion"
assert_equals 'az' "$(lower_case 'AZ')" "Multi-character lowercase conversion"
assert_equals 'ab' "$(lower_case 'Ab')" "Handle lowercase letters"
assert_equals 'ab' "$(lower_case 'ab')" "Lowercase already"
assert_equals 'a b c #' "$(lower_case 'a B C #')" "Handle spaces and symbols"

echo 'Testing: pad_string'
assert_equals 'ABC#' "$(pad_string 'ABC' '#' 4)" "Pad one character"
assert_equals 'ABC###' "$(pad_string 'ABC' '#' 6)" "Pad some characters"
assert_equals 'ABC' "$(pad_string 'ABC' '#' 3)" "Pad no characters (equal length to pad to)"
assert_equals 'ABC' "$(pad_string 'ABC' '#' 2)" "Pad no characters (smaller length to pad to)"

echo 'All tests passed.'