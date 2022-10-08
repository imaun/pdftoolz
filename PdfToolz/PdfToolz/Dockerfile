FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PdfToolz/PdfToolz.csproj", "PdfToolz/"]
RUN dotnet restore "PdfToolz/PdfToolz.csproj"
COPY . .
WORKDIR "/src/PdfToolz"
RUN dotnet build "PdfToolz.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdfToolz.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfToolz.dll"]
