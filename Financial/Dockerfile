#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Financial/Financial.csproj", "Financial/"]
COPY ["Financial.App/Financial.Application.csproj", "Financial.App/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Financial.Domain/Financial.Domain.csproj", "Financial.Domain/"]
COPY ["Financial.Infrastructure/Financial.Infrastructure.csproj", "Financial.Infrastructure/"]
RUN dotnet restore "./Financial/Financial.csproj"
COPY . .
WORKDIR "/src/Financial"
RUN dotnet build "./Financial.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Financial.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Financial.dll"]