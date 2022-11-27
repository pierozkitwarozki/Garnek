FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8080"
ENV ASPNETCORE_ENVIRONMENT="Production"
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Garnek.Application/Garnek.Application.csproj", "Garnek.Application/"]
COPY ["Garnek.Model/Garnek.Model.csproj", "Garnek.Model/"]
COPY ["Garnek.Infrastructure/Garnek.Infrastructure.csproj", "Garnek.Infrastructure/"]
COPY ["Garnek.Test/Garnek.Test.csproj", "Garnek.Test/"]
COPY ["Garnek.WebAPI/Garnek.WebAPI.csproj", "Garnek.WebAPI/"]

RUN dotnet test "Garnek.Test/Garnek.Test.csproj"
RUN dotnet restore "Garnek.WebAPI/Garnek.WebAPI.csproj"
COPY . .
WORKDIR "/src/Garnek.WebAPI"
RUN dotnet build "Garnek.WebAPI.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "Garnek.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Garnek.WebAPI.dll"]