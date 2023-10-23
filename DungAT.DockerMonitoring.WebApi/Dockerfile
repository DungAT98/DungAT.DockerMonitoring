FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DungAT.DockerMonitoring.WebApi/DungAT.DockerMonitoring.WebApi.csproj", "DungAT.DockerMonitoring.WebApi/"]
RUN dotnet restore "DungAT.DockerMonitoring.WebApi/DungAT.DockerMonitoring.WebApi.csproj"
COPY . .
WORKDIR "/src/DungAT.DockerMonitoring.WebApi"
RUN dotnet build "DungAT.DockerMonitoring.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DungAT.DockerMonitoring.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DungAT.DockerMonitoring.WebApi.dll"]
