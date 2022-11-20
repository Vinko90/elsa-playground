FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Elsa.Server/Elsa.Server.csproj", "Elsa.Server/"]
RUN dotnet restore "src/Elsa.Server/Elsa.Server.csproj"
COPY . .
WORKDIR "/src/Elsa.Server"
RUN dotnet build "Elsa.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elsa.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elsa.Server.dll"]
