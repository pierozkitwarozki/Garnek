FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Garnek.WorkerService/Garnek.WorkerService.csproj", "Garnek.WorkerService/"]
RUN dotnet restore "Garnek.WorkerService/Garnek.WorkerService.csproj"
COPY . .
WORKDIR "/src/Garnek.WorkerService"
RUN dotnet build "Garnek.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Garnek.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Garnek.WorkerService.dll"]
