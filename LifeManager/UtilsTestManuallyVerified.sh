#!/bin/bash

#
# Manually verified BASH script tests
#

# == Imports ==========================================================================================================

. Environment/Utils.sh
. Environment/TestUtils.sh

# == General file handling ============================================================================================
# TODO: These could be in UtilsTest.sh

print_marker "hash_folder_contents: Should resolve to 759886CB"
hash_folder_contents './Environment/TestResources/HashThisFolder'

print_marker "folder_size: Should resolve to 9"
folder_size './Environment/TestResources/HashThisFolder'

# == download_and_extract_to ==========================================================================================

print_marker "download_and_extract_to: File should get extracted - the folder shouldn't exist."
test_zip_path="file://$(convert_mingw_path_to_windows "$(pwd)")/Environment/TestResources/CompressedFile.7z"
download_and_extract_to "$test_zip_path" './Environment/TestResources/FileTesting' 'Test file' '5E2EDEC1'
rm './Environment/TestResources/FileTesting/CompressedFile.txt'

print_marker "download_and_extract_to: File should get extracted - the folder should exist, but is empty and hashes to F7F7F4DD."
test_zip_path="file://$(convert_mingw_path_to_windows "$(pwd)")/Environment/TestResources/CompressedFile.7z"
download_and_extract_to "$test_zip_path" './Environment/TestResources/FileTesting' 'Test file' '5E2EDEC1'

print_marker "download_and_extract_to: File should already exist, nothing should happen."
test_zip_path="file://$(convert_mingw_path_to_windows "$(pwd)")/Environment/TestResources/CompressedFile.7z"
download_and_extract_to "$test_zip_path" './Environment/TestResources/FileTesting' 'Test file' '5E2EDEC1'
rm -r './Environment/TestResources/FileTesting'

# == print_marker ===================================================================================================== 

# Can manually verify these thoroughly by copying the terminal output, removing all \[# \r\n]\ matches, 
# and comparing with the string in the script (ignoring whitespace differences)
print_marker 'print_marker: Short message'
print_marker "Abc"

print_marker 'print_marker: Long message, with spaces'
print_marker "._|1._|2._|3._|4._|5._|6._|7._|8 ._|9._|10._|11._|12._|13._|14._|15 ._|16._|17._|18._|19._|20._|21 ._|22._|23._|24._|25._|26._|27._|28._|29._|30._|31._|32._|33._|34._|35._|36._|37._|38"

print_marker 'print_marker: Long message, with many spaces'
print_marker "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "

print_marker 'print_marker: Long message, with multi-spaces' 
print_marker "._|1._|2._|3._|4._|5._|6._|7._|8    ._|9._|10._|11._|12._|13._|14._|15    ._|16._|17._|18._|19._|20._|21    ._|22._|23._|24._|25._|26._|27._|28._|29._|30._|31._|32._|33._|34._|35._|36._|37._|38"

print_marker 'print_marker: Long message, without spaces' 
print_marker "._|1._|2._|3._|4._|5._|6._|7._|8._|9._|10._|11._|12._|13._|14._|15._|16._|17._|18._|19._|20._|21._|22._|23._|24._|25._|26._|27._|28._|29._|30._|31._|32._|33._|34._|35._|36._|37._|38"

# == End ============================================================================================================== 

print_marker "End of manually verified tests"