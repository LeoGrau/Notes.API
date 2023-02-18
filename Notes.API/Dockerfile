FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Notes.API/Notes.API.csproj", "Notes.API/"]
RUN dotnet restore "Notes.API/Notes.API.csproj"
COPY . .
WORKDIR "/src/Notes.API"
RUN dotnet build "Notes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notes.API.dll"]
