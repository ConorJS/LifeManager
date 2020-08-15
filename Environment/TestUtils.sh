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