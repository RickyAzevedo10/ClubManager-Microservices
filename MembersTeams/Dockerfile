#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MembersTeams/MembersTeams.csproj", "MembersTeams/"]
COPY ["MembersTeams.App/MembersTeams.Application.csproj", "MembersTeams.App/"]
COPY ["MembersTeams.Domain/MembersTeams.Domain.csproj", "MembersTeams.Domain/"]
COPY ["MembersTeams.Infrastructure/MembersTeams.Infrastructure.csproj", "MembersTeams.Infrastructure/"]
RUN dotnet restore "./MembersTeams/MembersTeams.csproj"
COPY . .
WORKDIR "/src/MembersTeams"
RUN dotnet build "./MembersTeams.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MembersTeams.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MembersTeams.dll"]