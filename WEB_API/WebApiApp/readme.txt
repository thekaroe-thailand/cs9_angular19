# migrate command
dotnet ef migrations add InitialCreate
dotnet ef database update

# run
dotnet watch run