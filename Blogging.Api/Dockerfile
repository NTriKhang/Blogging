# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Blogging.Api/Blogging.Api.csproj", "Blogging.Api/"]
COPY ["Blogging.Common.Infrastructure/Blogging.Common.Infrastructure.csproj", "Blogging.Common.Infrastructure/"]
COPY ["Blogging.Common.Application/Blogging.Common.Application.csproj", "Blogging.Common.Application/"]
COPY ["Blogging.Common.Domain/Blogging.Common.Domain.csproj", "Blogging.Common.Domain/"]
COPY ["Blogging.Common.Presentation/Blogging.Common.Presentation.csproj", "Blogging.Common.Presentation/"]
COPY ["Blogging.Modules.User.Infrastructure/Blogging.Modules.User.Infrastructure.csproj", "Blogging.Modules.User.Infrastructure/"]
COPY ["Blogging.Modules.User.Application/Blogging.Modules.User.Application.csproj", "Blogging.Modules.User.Application/"]
COPY ["Blogging.Modules.Users.Domain/Blogging.Modules.User.Domain.csproj", "Blogging.Modules.Users.Domain/"]
COPY ["Blogging.Modules.User.Presentation/Blogging.Modules.User.Presentation.csproj", "Blogging.Modules.User.Presentation/"]
RUN dotnet restore "./Blogging.Api/Blogging.Api.csproj"
COPY . .
WORKDIR "/src/Blogging.Api"
RUN dotnet build "./Blogging.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blogging.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blogging.Api.dll"]