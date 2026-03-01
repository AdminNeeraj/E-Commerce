#  dotnet --info
# mkdir skinet


# Create web api Project 
dotnet new webapi -n API --use-controllers
dotnet new webapi -n API

# Create Sln file
dotnet sln add/ API
code .

# Dot net Run 
1. cd api
2. api dotnet run

2️⃣ Clean & restore
dotnet clean
dotnet restore



# ssl
dotnet dev-certs https
# Clean any 
dotnet dev-certs https --Clean

dotnet dev-certs https --trust

# Install swagger
dotnet add package Swashbuckle.AspNetCore
`Program.cs Configration and LounchSetting.json file "launchUrl": "swagger", "launchBrowser": true`

# Hot Reload
cd api
dotnet watch --no-hot-reload

# Pro tip (recommended structure)

For an E-Commerce solution, this is a clean setup:

E-Commerce
│
├── api        ← ASP.NET Core Web API
│   └── api.csproj
│
├── client     ← React / Angular / frontend
│
└── E-Commerce.sln

# dotnet watch --project api

Entity Framework.Slient
Entity Framework.Designe

## https://www.nuget.org/packages/dotnet-ef

# dotnet tool list -g
# dotnet tool install --global dotnet-ef --version
# dotnet tool update --global dotnet --version 

# dotnet ef 
# cd api
# dotnet ef migrations add initialcrete -o Data/Migrations
 # dotnet ef database update


 # dotnet tool install --global dotnet-ef --version 10.0.2

# dotnet dev-certs https --trust 

 "ConnectionStrings": {
     "DefaultConnection" :"Data source=Skinet.db"
  }

//"DefaultConnection" : "Data source=DESKTOP-2EAKJ09\\SQLEXPRESS;Database=PaymentDetailDB;user id=sa;password=1234;TrustServerCertificate=True;MultipleActiveResultSets=True;"

 //"DefaultConnection" : "Data source=DESKTOP-2EAKJ09\\SQLEXPRESS;Database=PaymentDetailDB;user id=sa;password=1234;TrustServerCertificate=True;MultipleActiveResultSets=True;"

 # dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  # dotnet add package Microsoft.EntityFrameworkCore.Tools

#  Stop the proccess
Get-Process -Name API | Stop-Process
# OR if your project name is different
Get-Process -Name dotnet | Where-Object {$_.MainWindowTitle -like "*API*"} | Stop-Process


# DROP DATABASE
dotnet ef database drop -p Core.Infrastructure -s API
# remove migration
dotnet ef migrations remove `
  -p .\Core.Infrastructure\Core.Infrastructure.csproj `
  -s .\WebApi\WebApi.csproj

# Run Project 
  D:\Neeraj Project\E-Commerce\api>  dotnet watch --no-hot-reload
 dotnet watch --no-hot-reload

# lacture 28 Till



