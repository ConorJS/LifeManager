# == Imports ==========================================================================================================

. ../Utils.sh

# == Constants ========================================================================================================

# Different download URL/hash between platforms
x64_platform_string=
hash=
extension=
if platform_is_windows; then
  x64_platform_string="windows"
  hash="E65027FE"
  extension="zip"
elif platform_is_linux; then
  x64_platform_string="linux"
  hash="739B1884"
  extension="tar.gz"
else
	error_if_unknown_platform "Determining Flyway download URL"
fi

readonly FLYWAY_VERSION_STRING="7.3.1"
readonly FLYWAY_URL=https://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/"$FLYWAY_VERSION_STRING"/flyway-commandline-"$FLYWAY_VERSION_STRING"-"$x64_platform_string"-x64."$extension"
readonly FLYWAY_INSTALLATION_DIR=LocalEnv/FlywayInstallation
readonly FLYWAY_HASH="$hash"

# == Execution ========================================================================================================

starting_directory=$(pwd)
flyway_directory="$FLYWAY_INSTALLATION_DIR""/flyway-""$FLYWAY_VERSION_STRING"
flyway_config_file="Environment/Database/flywayConfiguration.conf"
cd ../..

# Download and extract Flyway.
download_and_extract_to "$FLYWAY_URL" "$FLYWAY_INSTALLATION_DIR" "Flyway" "$FLYWAY_HASH"

# Migrate PostgreSQL database
if platform_is_linux; then	
	# Workaround for Flyway 7.3.1 to make JRE bundled with Flyway executable (otherwise the script fails in a cryptic manner)
	bundled_jre_executable="$FLYWAY_INSTALLATION_DIR""/flyway-""$FLYWAY_VERSION_STRING""/jre/bin/java"
	sudo chmod 774 $bundled_jre_executable
	execute_script_hardened "$(pwd)"'/'"$flyway_directory""/flyway" -configFiles="$flyway_config_file" migrate
	sudo chmod 554 $bundled_jre_executable
	
elif platform_is_windows; then
	"$FLYWAY_INSTALLATION_DIR""/flyway-""$FLYWAY_VERSION_STRING""/flyway.cmd" -configFiles="$flyway_config_file" migrate
fi

cd $starting_directory