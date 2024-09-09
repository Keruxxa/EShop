FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EShop.Web/EShop.Web.csproj", "EShop.Web/"]
COPY ["EShop.Application/EShop.Application.csproj", "EShop.Application/"]
COPY ["EShop.Domain/EShop.Domain.csproj", "EShop.Domain/"]
COPY ["EShop.Infrastructure/EShop.Infrastructure.csproj", "EShop.Infrastructure/"]
RUN dotnet restore "./EShop.Web/EShop.Web.csproj"
COPY . .
WORKDIR "/src/EShop.Web"
RUN dotnet build "EShop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShop.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShop.Web.dll"]
