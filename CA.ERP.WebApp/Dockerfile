#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CA.ERP.WebApp/CA.ERP.WebApp.csproj", "CA.ERP.WebApp/"]
COPY ["CA.ERP.DataAccess/CA.ERP.DataAccess.csproj", "CA.ERP.DataAccess/"]
COPY ["CA.ERP.Domain/CA.ERP.Domain.csproj", "CA.ERP.Domain/"]
COPY ["CA.ERP.Domain.Core/CA.ERP.Domain.Core.csproj", "CA.ERP.Domain.Core/"]
COPY ["CA.ERP.Common/CA.ERP.Common.csproj", "CA.ERP.Common/"]
COPY ["CA.ERP.Infrastructure/CA.ERP.Infrastructure.csproj", "CA.ERP.Infrastructure/"]
COPY ["CA.ERP.Shared/CA.ERP.Shared.csproj", "CA.ERP.Shared/"]
COPY ["CA.ERP.Application/CA.ERP.Application.csproj", "CA.ERP.Application/"]
RUN dotnet restore "CA.ERP.WebApp/CA.ERP.WebApp.csproj"
COPY . .
WORKDIR "/src/CA.ERP.WebApp"
RUN dotnet build "CA.ERP.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
COPY ["certs/dev.citi-appliance.co.crt", "/usr/local/share/ca-certificates/"]
RUN update-ca-certificates
RUN dotnet publish "CA.ERP.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.ERP.WebApp.dll"]
