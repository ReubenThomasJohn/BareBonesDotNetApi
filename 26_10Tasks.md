### Q1. Differences between REST and Web API

#### REST APIs

1. REST ( Representational State Transfer) is an architecture. REST API is a particular architecture style used to design networked applications. 

2. REST APIs used only HTTP/HTTPS methods to perform CRUD (Create, Read, Update, Delete) operations on resources. 

#### Web APIs
1. Web API is a general term used to describe any API that is accessible over HTTP/s protocol. 

2. They can be implemented using many architectural styles, including REST, SOAP, GraphQL etc. Hence, A REST API is also a Web API. 

Therefore, REST APIs are a particular kind of Web API that follows the principles of REST architecture. 

Web APIs are a broader term used to describe many kinds of APIs, with different architectures and technologies. 

### Q2. In ASP.Net Core, there is one SQL Server DB that has 5 migrations applied on it. There 5 migrations need to be applied on a second DB. How will the second DB know which migrations to apply?

1. Update the connection string to connect to the new DB.
2. Run `dotnet ef database update`

Entity Framework core will check which migrations are missing, and apply them. 

I have tested this on my system, and the [`MigrationLogs.txt`](./MigrationLogs.txt) file has the logs, showing that it has been applied. 

### Q3. Github Scenario
#### Lets say there are two developers who have taken a clone of the dev branch from Github which was at HEAD no. 20. Developer 1 makes 100 lines of changes and then pushes his code back creating a HEAD 21, while Developer 2 is still working on HEAD 20. When he tries to commit, he will encounter merge conflicts. How can these be resolved, and a new HEAD 22 created? 

1. Developer 2 should first fetch the latest changes from the remote repo (Github) to update his local to HEAD 21 which the Dev 1 made. 
`git fetch origin`. At this point, Git does not change anything in Developer 2's local branch, but only updates the remote tracking branches.

2. Now, `git rebase origin/dev` needs to be run. This helps in resolving the merge conflicts in a clean and linear fashion. Each of Dev 2's commits will be replayed, allowing him to fix any merge conflicts manually.

3. After the conflicts have been fixed, `git add .` needs to be run to stage the corrected files. 

4. Then `git rebase --continue` needs to be run, to resolve conflicts in the next commit. 

5. Once all the conflicts have been resolved, `git push origin dev` can be run to create HEAD 22. 


### Notes

Definition:

What Makes an API RESTful:

To be RESTful, an API must follow the principles and constraints mentioned above.
a. It should use standard HTTP methods for CRUD (Create, Read, Update, Delete) operations on resources.
b. Resources should be uniquely identified by URLs.
It should follow stateless communication, meaning each request must contain all the necessary information.
c. Responses should include representations of resources, allowing clients to work with the data.
d. The API should have a consistent, uniform interface for all resources.