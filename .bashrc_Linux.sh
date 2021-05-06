## == General

# Cleaning the terminal
alias c="clear"
alias cls="clear"

# Opening Explorer for an item, or in the current folder.
alias oh="gio open ."
alias o="gio open"

# Restarting the current terminal instance
alias r="exec bash"

# History grep
alias gh="history|grep"

# Show files, sorted by modification time
alias lsm="ls -t -l"

## == Git

alias gd="git diff"
alias gfet="git fetch"
alias gbr="git rev-parse --abbrev-ref HEAD"
alias go="git log --branches --not --remotes=origin"
alias gpul="git pull"
alias gpus="git push"
alias groll="git reset --soft @{u}"
alias gst="git status"
# TODO: gfloating/gmerged
alias gbr="git rev-parse --abbrev-ref HEAD"

gff() {
	is_git && echo Performing fast-forward of local branch $(gbr) to head of remote branch origin/$(gbr)...
}

gpusnew() {
	is_git && \
	if [ -z "$1" ]; then
    	echo "gpusnew.bat needs one argument: The name of the new branch, for which thgis will create a remote branch (e.g. feature_50)."
	else
		git push --set-upstream origin "$1"
  	fi
}

grevert() {
	is_git && \
	prompt_yes_no \
	  "This will clear ALL working directory changes (git reset --hard). Type '(Y)es' to continue:"\
	  && git reset --hard
}

gthis() {
	render_revision "$(git rev-parse HEAD)"
}

render_revision() {
	validate_arg_count $# "${FUNCNAME[0]}" 1 1

	# Revision details:
	git show "$1" --oneline --no-patch

	# Revision timestamp info
	git show -s --format=%Cred%cr "$1"
	git show -s --format=%Cgreen%cD "$1"
}

## == Application

lmroot="~/LifeManager/Repository" 

# Go to LifeManager directory
alias lm="cd $lmroot"

# Running LifeManager setup (changing dir if needed)
alias lms="cd $lmroot/LifeManager/ && sudo chmod 555 Setup.sh && ./Setup.sh"
alias lmsf="cd $lmroot/LifeManager/ && stopdb; sudo chmod 555 Setup.sh && ./Setup.sh" 

# Stops the LifeManager database
alias stopdb="cd $lmroot/LifeManager/ && sudo chmod 555 StopDatabase.sh && ./StopDatabase.sh" 

# Running LifeManager tests (simple assertions)
alias t="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTest.sh && ./UtilsTest.sh" 

# Running LifeManager tests (manually verified tests)
alias tm="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestManuallyVerified.sh && ./UtilsTestManuallyVerified.sh" 

# Running LifeManager tests (user input tests)
alias tui="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestUserInput.sh && ./UtilsTestUserInput.sh" 

# Running LifeManager tests (scratch)
alias ts="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestScratch.sh && ./UtilsTestScratch.sh" 

# Running LifeManager tests (all)
alias ta="cd $lmroot/LifeManager/ && sudo chmod 555 RunAllTests.sh && ./RunAllTests.sh" 
## == Private Utils (duplicates Utils.sh)

# Prompts the user for either 'Yes' or 'No', with a message.
# If the user provides an incorrect input, the prompt will reoccur.
#
# 1 The message to prompt with.
#
# Returns: 0 if 'Yes', 1 if 'No.'
function prompt_yes_no() {
  message=$1

  validate_arg_count $# "${FUNCNAME[0]}" 1 1

  while true; do
    read -r -p "$message: " user_input
    case $user_input in
    [Yy]*) return 0 ;;
    [Nn]*) return 1 ;;
    *) echo "Enter Y(es) or N(o)." ;;
    esac
  done

  echo "ERROR in '${FUNCNAME[0]}': - should not escape while loop."
}

# Checks if the current working directory is within a Git repository.
#
# Returns: 0 if within a Git repository, 1 if not.
is_git() {
	if [ -d .git ]; then
	  return 0;
	else
	  echo This is not a Git repository.
	  return 1;
	fi;
}

# Fails if an incorrect number of arguments are provided to a function
#
# 1 arguments_array_length:	The array of arguments provided to the function.
# 2 min_arguments:			The minimum number of arguments that can be provided to the function.
# 3 max_arguments:			The maximum number of arguments that can be provided to the function.
function validate_arg_count() {
  arguments_array_length=$1
  function_name=$2
  min_arguments=$3
  max_arguments=$4

  # Validate validate_arg_count's own arguments
  if [ -n "$5" ]; then
    echo "ERROR in '${FUNCNAME[0]}': More than 4 arguments ($#) provided to validate_arg_count"
    return 1
  fi

  if (($# < 4)); then
    echo "ERROR in '${FUNCNAME[0]}': Fewer than $min_arguments arguments ($#) provided to validate_arg_count"
    return 1
  fi

  # Validate the number of arguments suggested by arguments_array_length
  if ((arguments_array_length > max_arguments)); then
    echo "ERROR in '${FUNCNAME[0]}': More than $max_arguments arguments ($arguments_array_length) provided to $function_name"
    return 1
  fi

  if ((arguments_array_length < min_arguments)); then
    echo "ERROR in '${FUNCNAME[0]}': Fewer than $min_arguments arguments ($arguments_array_length) provided to $function_name"
    return 1
  fi
}

# Determines if a string contains a value.
#
# 1 The string.
# 2 The value.
#
# Example 1: string="abc" value="ab" returns true (0)
# Example 2: string="a" value="ab" returns false (1).
function string_contains() {
  string=$1
  value=$2

  if [[ -z $string ]]; then
    return 1
  fi

  if [[ $string == *"$value"* ]]; then
    return 0
  fi

  return 1
}
