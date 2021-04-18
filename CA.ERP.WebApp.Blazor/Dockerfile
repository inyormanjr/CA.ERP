#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM nginx:alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CA.ERP.WebApp.Blazor/CA.ERP.WebApp.Blazor.csproj", "CA.ERP.WebApp.Blazor/"]
COPY ["CA.ERP.Shared/CA.ERP.Shared.csproj", "CA.ERP.Shared/"]
COPY ["CA.ERP.Common/CA.ERP.Common.csproj", "CA.ERP.Common/"]
RUN dotnet restore "CA.ERP.WebApp.Blazor/CA.ERP.WebApp.Blazor.csproj"
COPY . .
WORKDIR "/src/CA.ERP.WebApp.Blazor"
RUN dotnet build "CA.ERP.WebApp.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.ERP.WebApp.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["CA.ERP.WebApp.Blazor/nginx/nginx.conf", "/etc/nginx/nginx.conf"] 

