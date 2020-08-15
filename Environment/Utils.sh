# Attempts to run a command. 
#
# Fails (exits the program) with an error message if the command can't be resolved.
#
# 1 command_string: 			The name of the command
# 2 additional_error_message: 	The error message to display after the default error. Optional.
#
function fail_if_not_command {
	command_string=$1
	additional_error_message=$2
	
	validate_arg_count $# ${FUNCNAME[0]} 1 2
	
	if ! (type $command_string) &>/dev/null; then
		echo "ERROR in '${FUNCNAME[0]}': \'$command_string\' is not on the path."
		
		if [ ! -z "$additional_error_message" ]; then
			echo $additional_error_message
		fi
		
		exit 1
	fi
}

# Downloads and extracts a compressed file to a directory.
# If any aspect of this operation fails, the program will exit.
#
# The URL must contain a file at the end formatted like a file (e.g. https://something.com/file.zip)
#
# Supported formats: .zip, .rar and .7z
#
# TODO: Add support for .tar.gz
#
# 1 url:						The URL to download the compressed file from.
# 2 output_directory: 			The output directory (relative or absolute) to extract the contents to.
# 3 english_name:				The English name of what is being downloaded. 
# 4 expected_hash:				The expected CRC32 hash of the extracted contents of the zip.
# 5 additional_error_message:	The error message to display after the default error. Optional.
#
function download_and_extract_to {
	url=$1
	output_directory=$2
	english_name=$3
	expected_hash=$4
	additional_error_message=$5
	
	validate_arg_count $# ${FUNCNAME[0]} 3 4
	
	supported_formats=('zip' 'rar' '7z')
	
	fail_if_not_command 7z
	
	# See if the directory already exists
	if [ -d "$output_directory" ]; then
		hash=$(hash_folder_contents $output_directory)
		if [ ! "$hash" = "$expected_hash" ]; then 
			echo "The directory for $english_name exists, but its contents have a CRC32 hash of $hash."
			echo "We expected a hash of $expected_hash."
			rm -r $output_directory
		else 
			echo "$english_name is already installed locally in $output_directory. Continuing."
			return
		fi
	else
		echo "$english_name is not installed locally in $output_directory."
		echo "Downloading and unpacking first."
	fi
	
	mkdir $output_directory
	
	# Get the file extension.
	filename=$(split_string_and_get_last $url '/')
	extension=$(split_string_and_get_last $filename '.')
	
	# See if the extracting this kind of file is supported 
	if [[ ! " ${supported_formats[@]} " == " ${extension} " ]]; then
		echo "ERROR in '${FUNCNAME[0]}': The file extension '.$extension' is not supported by download_and_extract_to"
		exit 1
	fi
	
	#-s hides download progress bar
	curl $url -o "$output_directory/$filename"
	7z x "$output_directory/$filename" "-o$output_directory" >> /dev/null
	rm "$output_directory/$filename"
	
	# Output a warning if the produced directory *still* doesn't have the same hash as what we expect
	# (likely this means we haven't updated the expected CRC32 hash in the script calling this)
	if [ -d "$output_directory" ]; then
		hash=$(hash_folder_contents $output_directory)
		if [ ! "$hash" = "$expected_hash" ]; then 
			echo "ERROR in '${FUNCNAME[0]}':"
			echo "The folder contents for $english_name were downloaded and extracted successfully."
			echo "However, the expected CRC32 hash was $expected_hash, and the actual hash is $hash"
			echo "If the downloaded hash is correct, the script should be updated before continuing."
			exit 1
		fi
	else
		echo "ERROR in '${FUNCNAME[0]}': Downloading and extracting of $filename failed."
		echo "$english_name was not installed."
		exit 1
	fi 
}

# Calculates the CRC hash of the contents of a folder (including the names)
#
# 1 folder:	The relative or absolute path to the folder.
#
# Returns: 		The 8 hex character CRC32 hash of the whole folder, names included
# Example IN: 	/path/to/file
# Example OUT: 	FB34CA12
function hash_folder_contents {
	folder=$1
	
	validate_arg_count $# ${FUNCNAME[0]} 1 1
	
	fail_if_not_command 7z "hash_folder_contents needs 7-zip to calculate CRC hashes"
	
	hash_line=$(7z h $folder | grep "CRC32  for data and names")

	hash=$(split_string_and_get_last "$hash_line" " ")
	echo $hash
}

# Calculates the CRC hash of the contents of a folder (including the names)
#
# 1 folder:	The relative or absolute path to the folder.
#
# Returns: 		The 8 hex character CRC32 hash of the whole folder, names included
# Example IN: 	/path/to/file
# Example OUT: 	FB34CA12
function folder_size {
	folder=$1
	
	validate_arg_count $# ${FUNCNAME[0]} 1 1
	
	fail_if_not_command 7z "folder_size needs 7-zip to see folder size"
	
	size_line=$(7z h $folder | grep "Size:")

	hash=$(split_string_and_get_last "$size_line" " ")
	echo $hash
}

# Splits a string by a given delimiter, and echoes the split section with the nth index.
#
# 1 string: 	The string to split.
# 2 delimiter:	The delimiting character. 
# 3 n:			The section index (0 indexed, so when 5 sections are found, a value higher than 4 is invalid)
#
# Returns:		The position n section of the string, when sectioned by the given delimiter.
# Example IN: 	1: Something55to55see55here 2: 55 3: 1
# Example OUT: 	to
function split_string_and_get_nth {
	string=$1
	delimiter=$2
	n=$3
	
	validate_arg_count $# ${FUNCNAME[0]} 3 3
	
	temp=IFS
	IFS=$delimiter
	read -ra ADDR <<< $string
	IFS=$temp
	
	if (( ${#ADDR[@]} <= n )); then
		echo "ERROR in '${FUNCNAME[0]}': String $string has only ${#ADDR[@]} sections (split by $delimiter); index $n is out of bounds."
		exit 1
	fi
	
	echo "${ADDR[$n]}"
	# for i in "${ADDR[@]}"; do
		# if [ ${ADDR[$i]} = n ]; then
			# echo $i
		# fi
	# done
}

# Splits a string by a given delimiter, and echoes the last section.
#
# 1 string: 	The string to split.
# 2 delimiter:	The delimiting character. 
#
# Returns:		The last section of the string, when sectioned by the given delimiter.
# Example IN: 	1: Something55to55see55here 2: 55
# Example OUT: 	here
function split_string_and_get_last {
	string=$1
	delimiter=$2
	
	validate_arg_count $# ${FUNCNAME[0]} 2 2
	
	temp=IFS
	IFS=$delimiter
	read -ra ADDR <<< $string
	IFS=$temp
	
	last_index=$((${#ADDR[@]}-1 ))
	echo "${ADDR[$last_index]}"
}

# Fails if an incorrect number of arguments are provided to a function
#
# 1 arguments_array_length:	The array of arguments provided to the function.
# 2 min_arguments:			The minimum number of arguments that can be provided to the function.
# 3 max_arguments:			The maximum number of arguments that can be provided to the function.
function validate_arg_count {
	arguments_array_length=$1
	function_name=$2
	min_arguments=$3
	max_arguments=$4
	
	# Validate validate_arg_count's own arguments
	if [ ! -z $5 ]; then
		echo "ERROR in '${FUNCNAME[0]}': More than 4 arguments ($#) provided to validate_arg_count"
		exit 1
	fi
	
	if (( $# < 4 )); then
		echo "ERROR in '${FUNCNAME[0]}': Fewer than $min_arguments arguments ($#) provided to validate_arg_count"
		exit 1
	fi
	
	# Validate the number of arguments suggested by arguments_array_length
	if (( $arguments_array_length > $max_arguments )); then
		echo "ERROR in '${FUNCNAME[0]}': More than $max_arguments arguments ($arguments_array_length) provided to $function_name"
		exit 1
	fi
	
	if (( $arguments_array_length < $min_arguments )); then
		echo "ERROR in '${FUNCNAME[0]}': Fewer than $min_arguments arguments ($arguments_array_length) provided to $function_name"
		exit 1
	fi
}

# Converts a MinGW path to a Windows path.
#
# 1 The MinGW (or Windows) path. 
#
# Returns:		The path, in UNIX form.
# Example IN: 	1: /c/my/path
# Example OUT: 	2: C:/my/path
function convert_mingw_path_to_windows {
	mingw_path=$1
	
    validate_arg_count $# ${FUNCNAME[0]} 1 1
	
	if [[ "${mingw_path:0:1}" == "/" ]] && [[ "${mingw_path:2:1}" == "/" ]]; then 
		# Convert a path like '/c/my/path'
		echo "${mingw_path:1:1}:${mingw_path:2:${#mingw_path}}"
	elif [[ "${mingw_path:1:1}" == "/" ]]; then
		# Convert a path like 'c/my/path'
		echo "${mingw_path:0:1}:${mingw_path:1:${#mingw_path}}"
	elif [[ "${mingw_path:1:2}" == ":/" ]]; then
		# Return a path like 'C:/my/path'
		echo "${mingw_path:0:1}:${mingw_path:2:${#mingw_path}}"
	else
		echo "ERROR in '${FUNCNAME[0]}': '$mingw_path' is not a valid UNIX or Windows path."
		exit 1
	fi
}

# Exits if an error code is set, with an accompanying message.
#
# 1 The name of the step in the script which 
function exit_if_error_code {
	error_code=$1
	step_descriptor=$2
	
	if [ $error_code -ne 0 ]; then
		echo ""
		echo "Step '$2' failed."
		echo "Exiting..."
		exit 1
	fi
}

# Prompts the user for either 'Yes' or 'No', with a message.
# If the user provides an incorrect input, the prompt will reoccur. 
#
# 1 The message to prompt with.
#
# Returns: 0 if 'No', 1 if 'Yes.'
function prompt_yes_no {
    message=$1
    
    validate_arg_count $# ${FUNCNAME[0]} 1 1
    
    while true; do
        read -p "$message: " user_input
        case $user_input in 
            [Yy]* ) return 0;;
            [Nn]* ) return 1;;
            * ) echo "Enter Y(es) or N(o).";;
        esac
    done
    
    echo "ERROR in '${FUNCNAME[0]}': - should not escape while loop."
}

# Prompts the user for n options (max 254). 
#
# Options will be selected based on the shortest unique leading substring.
# 
# 1 	A message string to prompt with, describing the selection being made by the user.
# 2...n Between one and n options for the user to choose from.
#
# Returns an exit code 1...n
function prompt_options {
	prompt_message=$1
	
	validate_arg_count $# ${FUNCNAME[0]} 3 255
	
	# Fail if there are duplicate strings
	i=0
	for var_i in "$@"
	do
		j=0
		for var_j in "$@"
		do
			if [[ ! $i == $j ]] && [[ $var_i == $var_j ]]; then
				echo "ERROR in '${FUNCNAME[0]}': - Argument '$var_i' was provided more than once."
				echo "ERROR in '${FUNCNAME[0]}': Args: $@"
				exit 255
			fi
			
			j=$(($j+1))
		done
		i=$(($i+1))
	done
	
	# Determine the minimal unique leading substring for each option string
	# For each option string...
	minimal_unique_leading_substring_lengths=()
	i=0
	for var_i in "$@"
	do
		# Skip the first argument (this is not an option string)
		if [[ $i -eq 0 ]]; then
			i=$(($i+1))
			continue
		fi
		
		minimum_length_required_to_be_unique=1
	
		# Compare to each other option string...
		j=0
		for var_j in "$@"
		do
			if [[ ! $var_i == $var_j ]]; then
				# Find the shorter string, we will iterate across both strings in parallel
				# until we reach the end of the shorter.
				short_len=0
				if [[ "${#var_i}" -ge "${#var_j}" ]]; then
					short_len="${#var_j}"
				else
					short_len="${#var_i}"
				fi
				
				for index in $(eval echo "{1..$short_len}")
				do
					if [[ "${var_i:0:$index}" == "${var_j:0:$index}" ]] && [[ $index -ge $minimum_length_required_to_be_unique ]]; then
						# If two leading substrings are unique, we know the substring must take at least 
						# n+1 characters to ever be a unique leading substring (it isn't unique at n characters!)
						minimum_length_required_to_be_unique=$(($index+1))
					fi
				done
			fi
			j=$(($j+1))
		done
		
		minimal_unique_leading_substring_lengths[$i]=$minimum_length_required_to_be_unique
		
		i=$(($i+1))
	done
	
	all_options_prompt="[Select"
	if [[ $# -gt 3 ]]; then
		all_options_prompt+=" one of] "
	else
		all_options_prompt+="] "
	fi
	
	# Set up the prompting string with all the options, with 
	# parentheses placement indicating the minimal input require per option
	index=-1
	for var in "$@"
	do
		index=$(($index+1))
		
		# Skip the first argument (this is not an option string)
		if [[ $index -eq 0 ]]; then
			continue
		fi
		
		substring=${var:0:minimal_unique_leading_substring_lengths[$index]}
		substring_length=${#substring}
		all_options_prompt+='('
		all_options_prompt+="$substring"
		all_options_prompt+=')'
		all_options_prompt+=${var:$substring_length:$((${#var}-$substring_length))}
		
		if [[ ! $index -eq $(($#-1)) ]]; then
			if [[ $index -eq $(($#-2)) ]]; then
				all_options_prompt+=" or "
			else 
				all_options_prompt+=", "
			fi
		fi
	done
	all_options_prompt+=": "
	
	# Prompt the user for an input
	while true; do
		echo ""
		echo $prompt_message
		
        read -p "$all_options_prompt" user_input
		user_input_length=${#user_input}
		
		# See if the user input matches any substrings
		index=-1
		for var in "$@"
		do
			index=$(($index+1))
			
			# Skip the first argument (this is not an option string)
			if [[ $index -eq 0 ]]; then
				continue
			fi
			
			substring_length=${minimal_unique_leading_substring_lengths[$index]}
			
			# Can't do anything if the user input is shorter than the substring
			if [[ $user_input_length -lt $substring_length ]]; then
				continue
			fi
			
			# If the user input matches the substring, the option is considered selected. (case insensitive)
			comparison_string=${var:0:$user_input_length}
			if [[ $(upper_case $comparison_string) == $(upper_case $user_input) ]]; then
				#echo "Is $var? $(upper_case $comparison_string) == $(upper_case $user_input)" # DEBUG
				return $index
			fi					
		done
    done
}

# Converts a string to upper case.
#
# In BASH >=4.0, the ${string^^} syntax can be used, instead.
#
# 1 The string
#
# Returns the string, in upper case.
function upper_case {
	string=$1
	
	validate_arg_count $# ${FUNCNAME[0]} 1 1
	
	echo $(tr '[:lower:]' '[:upper:]' <<< ${string})
}



