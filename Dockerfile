FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Cv.Api/Cv.Api.csproj Cv.Api/
RUN dotnet restore Cv.Api/Cv.Api.csproj

COPY Cv.Api/ Cv.Api/
RUN dotnet publish Cv.Api/Cv.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Cv.Api.dll"]
