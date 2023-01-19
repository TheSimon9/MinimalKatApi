FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MinimalApiTest/MinimalApiTest.csproj", "MinimalApiTest/"]
RUN dotnet restore "MinimalApiTest/MinimalApiTest.csproj"
COPY . .
WORKDIR "/src/MinimalApiTest"
RUN dotnet build "MinimalApiTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiTest.dll"]
