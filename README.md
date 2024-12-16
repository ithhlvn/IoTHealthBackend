# Create Database
## Solution 1

### Using Package Manager Console
- PM> Add-Migration InitialCreate
- PM> Update-Database

## Solution 2
### Step 1: 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

### Step 2: 
dotnet ef migrations add InitialCreate

### Step 3: 
dotnet clean

### Step 4: 
dotnet build

### Step 5: 
dotnet ef database update

