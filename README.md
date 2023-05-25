## Test Coverage
### Running the Data Collector
The data collector runs the tests and collects the data into an XML file.  It can be run by navigating to the test project and then calling:
```
dotnet test --collect:"XPlat Code Coverage" 
```
It will generate an XML output file in a TestResults subfolder.

### Report Generation
In order to display the results of the tests run in a pretty format, we need to run a report generator. That can be done by navigating to the test project directory and then calling:
```
reportgenerator -reports:"C:\!Personal\ManagerHelper\ManagerHelperTests\TestResults\<Guid>\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

This will create an HTML page in a coveragereport subfolder.

Resource: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows