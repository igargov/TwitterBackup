FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["TwitterBackup.Web/TwitterBackup.Web.csproj", "TwitterBackup.Web/"]
COPY ["TwitterBackup.Data/TwitterBackup.Data.csproj", "TwitterBackup.Data/"]
COPY ["TwitterBackup.Data.Models/TwitterBackup.Data.Models.csproj", "TwitterBackup.Data.Models/"]
COPY ["TwitterBackup.Providers/TwitterBackup.Providers.csproj", "TwitterBackup.Providers/"]
COPY ["TwitterBackup.Services/TwitterBackup.Services.csproj", "TwitterBackup.Services/"]
COPY ["TwitterBackup.TwitterApiClient/TwitterBackup.TwitterApiClient.csproj", "TwitterBackup.TwitterApiClient/"]
RUN dotnet restore "TwitterBackup.Web/TwitterBackup.Web.csproj"
COPY . .
WORKDIR "/src/TwitterBackup.Web"
RUN dotnet build "TwitterBackup.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TwitterBackup.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TwitterBackup.Web.dll"]