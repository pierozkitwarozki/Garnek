FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Garnek.Application/Garnek.Application.csproj", "Garnek.Application/"]
COPY ["Garnek.Model/Garnek.Model.csproj", "Garnek.Model/"]
COPY ["Garnek.Infrastructure/Garnek.Infrastructure.csproj", "Garnek.Infrastructure/"]
COPY ["Garnek.WorkerService/Garnek.WorkerService.csproj", "Garnek.WorkerService/"]
RUN dotnet restore "Garnek.WorkerService/Garnek.WorkerService.csproj"
COPY . .
WORKDIR "/src/Garnek.WorkerService"
RUN dotnet build "Garnek.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Garnek.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final