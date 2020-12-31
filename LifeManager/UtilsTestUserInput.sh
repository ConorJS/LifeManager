#!/bin/bash

#
# Automatic bash script tests
#

# == Imports ==========================================================================================================

. Environment/Utils.sh
. Environment/TestUtils.sh

# == Tests ============================================================================================================

# Example of what happens when too many arguments are provided to function handler
#assert_user_input_result 1 'y' 'prompt_yes_no' 'Handle bad number of arguments to function handler' "Testing prompt_yes_no..." "abc"

echo ''
echo 'Testing: prompt_yes_no'
assert_user_input_result 0 'y' 'prompt_yes_no' 'Yes (lower-case)' 'Testing prompt_yes_no...'
assert_user_input_result 1 'n' 'prompt_yes_no' 'No (lower-case)' 'Testing prompt_yes_no...'
assert_user_input_result 1 'n' 'prompt_yes_no' 'No (lower-case)' 'Testing prompt_yes_no...'

echo ''
echo 'Testing: prompt_options'
assert_user_input_result 1 'opt1' 'prompt_options' 'Two options only - first' 'Choose an option!' 'opt1' 'opt2'
assert_user_input_result 2 'opt2' 'prompt_options' 'Two options only - last' 'Choose an option!' 'opt1' 'opt2'
assert_user_input_result 5 'fi' 'prompt_options' 'Two character match - two minimum' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
assert_user_input_result 5 'fiv' 'prompt_options' 'Three character match - two minimum' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
assert_user_input_result 5 'five' 'prompt_options' 'All character match - two minimum' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
assert_user_input_result 9 'n' 'prompt_options' 'One character match - one minimum' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
assert_user_input_result 1 'o' 'prompt_options' 'Select first option' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
assert_user_input_result 10 'te' 'prompt_options' 'Select last option' 'Choose a number!' 'one' 'two' 'three' 'four' 'five' 'six' 'seven' 'eight' 'nine' 'ten'
