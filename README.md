# W_Economerce
Thạc sỹ (tìm hiểu database Firebird)

#
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "FirebirdConnection": "User=SYSDBA;Password=dev;Database=D:\\ThacSy\\CacHeCSDL\\FireBird\\ECONOMERCE.fdb;DataSource=localhost;Port=3050;Dialect=3"
  },
  "AllowedHosts": "*"
}
//update migration
dotnet ef migrations add UpdateProductModel
dotnet ef database update