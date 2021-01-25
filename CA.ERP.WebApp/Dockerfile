#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -y nodejs
RUN apt-get install libc6-dev  -y
RUN apt-get install libgdiplus -y
RUN apt-get install -y libfontconfig1


WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CA.ERP.WebApp/CA.ERP.WebApp.csproj", "CA.ERP.WebApp/"]
COPY ["CA.ERP.DataAccess/CA.ERP.DataAccess.csproj", "CA.ERP.DataAccess/"]
COPY ["CA.ERP.Domain/CA.ERP.Domain.csproj", "CA.ERP.Domain/"]
COPY ["CA.ERP.Common/CA.ERP.Common.csproj", "CA.ERP.Common/"]
RUN dotnet restore "CA.ERP.WebApp/CA.ERP.WebApp.csproj"
COPY . .
WORKDIR "/src/CA.ERP.WebApp"
RUN dotnet build "CA.ERP.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -y nodejs
RUN dotnet publish "CA.ERP.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.ERP.WebApp.dll"]