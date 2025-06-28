# SWD_Gr6_PreOJT-RiskAlert_BE
For BE APId

### Code First: 
Start CLI in solution
#### 1. Code
Delete file in the ./Repositories/Migration/...
#### 2. Migration
`dotnet ef migrations add InitCreate --project .\Repositories  --startup-project .\SWD_Gr6_PreOJT-RiskAlert\SWD_Gr6_PreOJT-RiskAlert.csproj`
#### 3. Database
`dotnet ef database update --project .\Repositories\  --startup-project .\SWD_Gr6_PreOJT-RiskAlert\SWD_Gr6_PreOJT-RiskAlert.csproj`