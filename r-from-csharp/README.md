# Call `R` from `C#` using `Microsoft.R.Host.Client.API`

It is a simple `C#` console app, which uses `Microsoft.R.Host.Client.API` (https://www.nuget.org/packages/Microsoft.R.Host.Client.API/) to call `R` from `C#`.

### Functionality covered:

- Gather output from R's console after code execution.
- Pass DataFrame from C# to R.
- Get a list of values from R. 

##### Warning - it seems that this API has a problem when list's values are of different types. That's why I'm only passing some numbers. 

### More resources.

- https://github.com/MikhailArkhipov/RTVS-cs - more examples describing the interface between R and .NET (creating plots).
