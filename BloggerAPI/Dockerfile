FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BloggerAPI/BloggerAPI.csproj", "BloggerAPI/"]
RUN dotnet restore "BloggerAPI/BloggerAPI.csproj"
COPY . .
WORKDIR "/src/BloggerAPI"
RUN dotnet build "BloggerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BloggerAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloggerAPI.dll"]
