#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CA.ERP.WebApp/CA.ERP.WebApp.csproj", "CA.ERP.WebApp/"]
COPY ["CA.ERP.Lib/CA.ERP.Lib.csproj", "CA.ERP.Lib/"]
RUN dotnet restore "CA.ERP.WebApp/CA.ERP.WebApp.csproj"
COPY . .
WORKDIR "/src/CA.ERP.WebApp"
RUN dotnet build "CA.ERP.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.ERP.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.ERP.WebApp.dll"]