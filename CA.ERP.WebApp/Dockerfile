#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get install -y nodejs
RUN apt-get install libc6-dev  -y
RUN apt-get install libgdiplus -y
RUN apt-get install -y libfontconfig1
RUN apt-get install -y --no-install-recommends libgconf-2-4 gnupg git curl wget ca-certificates libgconf-2-4 && \
    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
    apt-get update && \  
    apt-get install -y lsb-release google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst libxtst6 libxss1 --no-install-recommends

ENV chrome_launchOptions_executablePath google-chrome-stable
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage,--single-process,--no-zygote

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CA.ERP.WebApp/CA.ERP.WebApp.csproj", "CA.ERP.WebApp/"]
COPY ["CA.ERP.DataAccess/CA.ERP.DataAccess.csproj", "CA.ERP.DataAccess/"]
COPY ["CA.ERP.Domain/CA.ERP.Domain.csproj", "CA.ERP.Domain/"]
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