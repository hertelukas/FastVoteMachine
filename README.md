# FastVoteMachine
Create and share simple, public votes with real-time updates.

## Setup
### Database
Connect a mariadb or MySQL database by setting `ConnectionStrings:DefaultConnection` to `server=server;user=username;password=password;database=dbname`.
[More information](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/blob/master/README.md#2-services-configuration)

This can be done via a environment variable or the `secrets.json` file.

Run following command to setup your database:
```bash
dotnet ef database update
```
