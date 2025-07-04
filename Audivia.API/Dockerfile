# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
# USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Audivia.API/Audivia.API.csproj", "Audivia.API/"]
COPY ["Audivia.Application/Audivia.Application.csproj", "Audivia.Application/"]
COPY ["Audivia.Infrastructure/Audivia.Infrastructure.csproj", "Audivia.Infrastructure/"]
COPY ["Audivia.Domain/Audivia.Domain.csproj", "Audivia.Domain/"]
RUN dotnet restore "./Audivia.API/Audivia.API.csproj"
COPY . .
WORKDIR "/src/Audivia.API"
RUN dotnet build "./Audivia.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Audivia.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Audivia.API.dll"]