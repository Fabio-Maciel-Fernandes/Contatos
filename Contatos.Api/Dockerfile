#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Contatos.Api/Contatos.Api.csproj", "Contatos.Api/"]
COPY ["Contatos.Core/Contatos.Core.csproj", "Contatos.Core/"]
COPY ["Contatos.Shared/Contatos.Shared.csproj", "Contatos.Shared/"]
COPY ["Contatos.Infra/Contatos.Infra.csproj", "Contatos.Infra/"]
RUN dotnet restore "./Contatos.Api/Contatos.Api.csproj"
COPY . .
WORKDIR "/src/Contatos.Api"
RUN dotnet build "./Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contatos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contatos.Api.dll"]