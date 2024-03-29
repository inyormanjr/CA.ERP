#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CA.Identity/CA.Identity.csproj", "CA.Identity/"]
COPY ["CA.ERP.Shared/CA.ERP.Shared.csproj", "CA.ERP.Shared/"]
COPY ["CA.ERP.Common/CA.ERP.Common.csproj", "CA.ERP.Common/"]
RUN dotnet restore "CA.Identity/CA.Identity.csproj"
COPY . .
WORKDIR "/src/CA.Identity"
RUN dotnet build "CA.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.Identity.dll"]