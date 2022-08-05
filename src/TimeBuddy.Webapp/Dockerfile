FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TimeBuddy.Webapp/TimeBuddy.Webapp.csproj", "TimeBuddy.Webapp/"]
RUN dotnet restore "src/TimeBuddy.Webapp/TimeBuddy.Webapp.csproj"
COPY . .
WORKDIR "/src/TimeBuddy.Webapp"
RUN dotnet build "TimeBuddy.Webapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeBuddy.Webapp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeBuddy.Webapp.dll"]
