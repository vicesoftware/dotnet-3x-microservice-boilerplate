FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.1-alpine3.10 AS base
RUN apk add curl
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101-alpine3.10 AS build
WORKDIR /src
# run nuget restore in seperate layers so we don't have to run these on every build
COPY ["api/api.csproj", "api/"]
RUN dotnet restore "api/api.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]