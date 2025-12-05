## Assumptions
- using exceptions rather than result pattern because of time
- I'm using my personal library MiWrap for REPR pattern :)
- endpoint is exposed by the API at stories/{numberOfStories} to keep in minimalistic without SwaggerUI
- caching implemented on output and client level
- feature based approach of organizing repo
- a little bit of extension method I use on my other projects 
- I like to keep related things close together like exception and exception handler or request and handler 
- default handling of param binding is enough in my opinion - invalid number ill return empty list

## How to run this app:
Requirements:
- latest .NET 10 installed

Steps:
- you can edit cache configuration in appsettings.json 
- go to the folder with BestStories.csproj file in your terminal
- type "dotnet run"
- terminal will prompt you link for the main page (empty)
- edit url by adding stories/{numberOfStories} eg. localhost:5248/stories/3


## TODO (not in requirements)
- containerization with devcontainers and docker 
- rate limiting 
- monitoring with open telemetry 
- auth
- tests :)
- get configuration form external provider
