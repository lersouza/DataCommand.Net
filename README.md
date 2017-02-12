
DataCommand.Net
===
[![Nuget](https://img.shields.io/badge/nuget-0.0.1-blue.svg)](https://www.nuget.org/packages/DataCommand.Core/)

A simple command-based data access library for .NET Core projects.

Applications often require data base access to perform their functionality. When building those applications, developers face many issues, like connection errors, connection timeouts, connection disposal and so on. Managing database connections is not such a trivial task.

Based on those observations, DataCommand.Net was created. It abstracts connection management, so developers can focus on implementing the funcionality while the library infrastructure handles some crucial non-functional features. It is based on [command pattern](https://www.martinfowler.com/bliki/CommandOrientedInterface.html), so it is very easy to implement new database functions and also calling them through a very simple API.

It does not provide any ORM functionality, but we have used this library together with [Dapper](https://github.com/StackExchange/Dapper) to make it easy to map data readers and parameters to the .NET objects.

## Quick start

To quick start using the API, developers must inherit from DataCommand to create their specific database commands for the application. Here is an exmaple: 

```c#
using DataCommand.Core;

public class GetPersonCommand : DataCommand<Person>
{
  private int _personId;

  public GetPersonCommand(int personId, DataCommandOptions options, ILoggerFactory loggerFactory)
    : base("GetPersonCommand", options, loggerFactory)
  {
    _personId = personId;
  }

  protected override Person Execute(IDbConnection connection, DataCommandOptions options)
  {
    Person person = null;
  
    // The connection parameter refers to an open connection
    // The options parameter contains some options that may be used, as command timeouts.
    
    // Perform so operations to retrieve the person from database by _personId
    
    return person;
  }
}
```

After creating your commands, calling them is straightforward:

```c#
DataCommandOptions options = new PostgresDataOptions() { ConnectionString = "your connection string", MaxRetries = 2 };
Person person = new GetPersonCommand(1, options, loggerFactory).Run();
```
