#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update -yq 
RUN apt-get install libc6-dev  -y
RUN apt-get install libgdiplus -y
RUN apt-get install -y libfontconfig1

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CA.ERP.Reporting/CA.ERP.Reporting.csproj", "CA.ERP.Reporting/"]
COPY ["CA.ERP.DataAccess/CA.ERP.DataAccess.csproj", "CA.ERP.DataAccess/"]
COPY ["CA.ERP.Domain/CA.ERP.Domain.csproj", "CA.ERP.Domain/"]
COPY ["CA.ERP.Domain.Core/CA.ERP.Domain.Core.csproj", "CA.ERP.Domain.Core/"]
COPY ["CA.ERP.Common/CA.ERP.Common.csproj", "CA.ERP.Common/"]
RUN dotnet restore "CA.ERP.Reporting/CA.ERP.Reporting.csproj"
COPY . .
WORKDIR "/src/CA.ERP.Reporting"
RUN dotnet build "CA.ERP.Reporting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.ERP.Reporting.csproj" -c Release -o /app/publish

FROM base AS final
COPY ["certs/dev.citi-appliance.co.crt", "/usr/local/share/ca-certificates/"]
RUN update-ca-certificates
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.ERP.Reporting.dll"]
