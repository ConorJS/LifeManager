## == General

# Cleaning the terminal
alias c="clear"
alias cls="clear"

# Opening Explorer for an item, or in the current folder.
#alias oh="gio open ." #Linux
alias oh="start ." #Windows
#alias o="gio open" #Linux
alias o="start" #Windows

# Restarting the current terminal instance
alias r="exec bash"


## == Application

lmroot="~/LifeManager/Repository" #Linux
#lmroot="$USERPROFILE/Repositories/LifeManager" #Windows

# Go to LifeManager directory
alias lm="cd $lmroot"

# Running LifeManager setup (changing dir if needed)
#alias lms="cd $lmroot/LifeManager/ && sudo chmod 555 Setup.sh && ./Setup.sh" #Linux
alias lms="cd '$lmroot'/LifeManager && ./Setup.sh" #Windows

# Stops the LifeManager database
#alias stopdb="cd $lmroot/LifeManager/ && sudo chmod 555 StopDatabase.sh && ./StopDatabase.sh" #Linux
alias stopdb="cd '$lmroot'/LifeManager && ./StopDatabase.sh" #Windows

# Running LifeManager tests (simple assertions)
#alias t="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTest.sh && ./UtilsTest.sh" #Linux
alias t="cd '$lmroot'/LifeManager && ./UtilsTest.sh" #Windows

# Running LifeManager tests (manually verified tests)
#alias tm="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestManuallyVerified.sh && ./UtilsTestManuallyVerified.sh" #Linux
alias tm="cd '$lmroot'/LifeManager && ./UtilsTestManuallyVerified.sh" #Windows

# Running LifeManager tests (user input tests)
#alias tui="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestUserInput.sh && ./UtilsTestUserInput.sh" #Linux
alias tui="cd '$lmroot'/LifeManager && ./UtilsTestUserInput.sh" #Windows

# Running LifeManager tests (scratch)
#alias ts="cd $lmroot/LifeManager/ && sudo chmod 555 UtilsTestScratch.sh && ./UtilsTestScratch.sh" #Linux
alias ts="cd '$lmroot'/LifeManager && ./UtilsTestScratch.sh" #Windows

# Running LifeManager tests (all)
#alias ta="cd $lmroot/LifeManager/ && sudo chmod 555 RunAllTests.sh && ./RunAllTests.sh" #Linux
alias ta="cd '$lmroot'/LifeManager && ./RunAllTests.sh" #Windows
