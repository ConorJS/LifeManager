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
	    echo
		echo "[assert_equals] Assertion failure in '$test_name':" 
		echo "Expected: '$expected'" 
		echo "  Actual: '$actual'"
		exit
	fi
}

# Asserts that a function accepting a user input completes with a specific error code (return value of function).
#
# If the error code does not return as expected, the program will exit with an appropriate error message.
#
# 1 	The expected error code.
# 2 	The user input to emulate when the method is called.
# 3 	The name of the function to be called.
# 4 	The name of the test
# 5..n	All of the arguments to the function. 
function assert_user_input_result {
    expected=$1
    user_input=$2
    function_handler=$3
    test_name=$4
	# Parameters to function handler = $5..$n 
	
	validate_arg_count $# ${FUNCNAME[0]} 4 255
    
    echo "Running user input test: $test_name"
    printf $user_input | $("$function_handler" "${@:5}" >> /dev/null)
    
    status=$?
    if [[ $status -eq 127 ]]; then
		echo
        echo "ERROR in '${FUNCNAME[0]}': Function call to '$function_handler' failed."
		echo "Args supplied: "
		
		index=0
		for arg_supplied in "${@:5}"
		do
			index=$(($index+1))
			echo "$index: $arg_supplied"
		done
        exit 1
    fi
    
    assert_equals $expected $status "$test_name"
}