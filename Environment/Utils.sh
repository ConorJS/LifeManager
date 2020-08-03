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
		echo \'$command_string\' is not on the path.
		
		if [ ! -z "$additional_error_message" ]; then
			echo $additional_error_message
		fi
    exit
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
	if [[ ! " ${supported_formats[@]} " =~ " ${extension} " ]]; then
		echo "The file extension '.$extension' is not supported by download_and_extract_to"
		exit
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
			echo "The folder contents for $english_name were downloaded and extracted successfully."
			echo "However, the expected CRC32 hash was $expected_hash, and the actual hash is $hash"
			echo "If the downloaded hash is correct, the script should be updated before continuing."
			exit
		fi
	else
		echo "Downloading and extracting of $filename failed."
		echo "$english_name was not installed."
		exit
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
	
	fail_if_not_command 7z "hash_folder_contents needs 7-zip to calculate CRC hashes"
	
	hash_line=$(7z h $folder | grep "CRC32  for data and names")

	hash=$(split_string_and_get_last "$hash_line" " ")
	echo $hash
}

# Splits a string by a given delimiter, and echoes the split section with the nth index.
#
# 1 string: 	The string to split.
# 2 delimiter:	The delimiting character. 
# 3 n:			The section index.
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
		echo "String $string has only ${#ADDR[@]} sections (split by $delimiter); index $n is out of bounds."
	fi
	
	for i in "${ADDR[@]}"; do
		if [ ${ADDR[$i]} = n ]; then
			echo $i
		fi
	done
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
		echo "More than 4 arguments ($#) provided to validate_arg_count"
		exit
	fi
	
	if (( $# < 4 )); then
		echo "Fewer than $min_arguments arguments ($#) provided to validate_arg_count"
		exit
	fi
	
	# Validate the number of arguments suggested by arguments_array_length
	if (( $arguments_array_length > $max_arguments )); then
		echo "More than $max_arguments arguments ($arguments_array_length) provided to $function_name"
		exit
	fi
	
	if (( $arguments_array_length < $min_arguments )); then
		echo "Fewer than $min_arguments arguments ($arguments_array_length) provided to $function_name"
		exit
	fi
}





