- [x] Create CRUD Endpoints for Students
- [x] Use EF Core to connect to DB
- [x] Create another repository to use Stored Procedures.

- [x] Create two new tables: User and UserStatus. The User table needs to have 3 fields, UserID (string) and Password (string), Status (int)
- [x] Create one endpoint: Register a User. This will take in a UserId and password. The UserID can be saved as-is, but the password needs to be hashed and salted.
- [x] Create the second endpoint: Login. This will authenticate the user based on the input credentials.
- [x] Create a third endpoint: Soft delete a user. Check what to do?

- [x] Implement API Layers
- [x] Implement Custom Exceptions
- [x] Implement logging middleware
- [x] Support different MediaTypes

- [x] Create a log file where everything is logged
- [x] Add the MediaType support globally in Program.cs

- [ ] Add a config file, which gives the log level info to the logger in Program.cs
- [x] Implement in-mem Caching
- [x] Refactored controllers to use a service

