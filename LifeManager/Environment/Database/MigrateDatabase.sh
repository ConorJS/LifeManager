# == Imports ==========================================================================================================

. ../Utils.sh

# == Constants ========================================================================================================

readonly FLYWAY_VERSION_STRING="7.3.1"
readonly FLYWAY_URL=https://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/"$FLYWAY_VERSION_STRING"/flyway-commandline-"$FLYWAY_VERSION_STRING"-windows-x64.zip
readonly FLYWAY_INSTALLATION_DIR=../../LocalEnv/FlywayInstallation
readonly FLYWAY_HASH=9C8E6B32

# == Execution ========================================================================================================

# Download and extract Flyway.
download_and_extract_to "$FLYWAY_URL" "$FLYWAY_INSTALLATION_DIR" "Flyway" "$FLYWAY_HASH"

# Migrate PostgreSQL database
"$FLYWAY_INSTALLATION_DIR""/flyway-""$FLYWAY_VERSION_STRING""/flyway.cmd" -configFiles=flywayConfiguration.conf migrate
