FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY HH.Web/HH.Web.csproj HH.Web/
RUN dotnet restore HH.Web/HH.Web.csproj
COPY . .
WORKDIR /src/HH.Web
RUN dotnet build HH.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HH.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HH.Web.dll"]
