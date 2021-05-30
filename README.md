## LifeManager
A tool to assist with general planning and time management.

### Steps to set up development environment
1. Run setup.sh. 

### Development troubleshooting

##### See generated SQL output
Add `"Microsoft.EntityFrameworkCore.Database.Command": "Information"` to appsettings.Development.json, under the Logging -> LogLevel node.

##### More sensitive database logging
In LifeManagerDatabaseContext#_OnConfiguring_, uncomment: `optionsBuilder.EnableSensitiveDataLogging()`.