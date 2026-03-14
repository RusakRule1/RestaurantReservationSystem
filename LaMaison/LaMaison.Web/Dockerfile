FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LaMaison.Web/LaMaison.Web.csproj", "LaMaison.Web/"]
COPY ["LaMaison.Core/LaMaison.Core.csproj", "LaMaison.Core/"]
COPY ["LaMaison.Infrastructure/LaMaison.Infrastructure.csproj", "LaMaison.Infrastructure/"]
RUN dotnet restore "LaMaison.Web/LaMaison.Web.csproj"
COPY . .
WORKDIR "/src/LaMaison.Web"
RUN dotnet build "LaMaison.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LaMaison.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaMaison.Web.dll"]
