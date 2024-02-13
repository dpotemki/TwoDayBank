dotnet restore ".\src\TwoDayDemoBank.Service.Core\TwoDayDemoBank.Service.Core.csproj"
dotnet restore ".\src\TwoDayDemoBank.Worker.Core\TwoDayDemoBank.Worker.Core.csproj"
dotnet restore ".\src\TwoDayDemoBank.Worker.Notifications\TwoDayDemoBank.Worker.Notifications.csproj"

cd ".\src\TwoDayDemoBank.Service.Core"
start cmd /k dotnet run --project ".\TwoDayDemoBank.Service.Core.csproj"
cd ..\..

cd ".\src\TwoDayDemoBank.Worker.Core"
start cmd /k dotnet run --project ".\TwoDayDemoBank.Worker.Core.csproj"
cd ..\..

cd ".\src\TwoDayDemoBank.Worker.Notifications"
start cmd /k dotnet run --project ".\TwoDayDemoBank.Worker.Notifications.csproj"
cd ..\..