FROM alpine:3.20 AS prepare
WORKDIR /app
RUN adduser -u 1000 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


FROM mcr.microsoft.com/dotnet/sdk:9.0-preview-alpine3.20 AS build
RUN apk update && apk upgrade
RUN apk add --no-cache clang build-base zlib-dev

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../../BoilerPlate.SSR/. .

RUN dotnet restore -r linux-musl-x64

WORKDIR "/src/BoilerPlate.Api"
#RUN dotnet build "BoilerPlate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BoilerPlate.Api.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish  -r linux-musl-x64

FROM prepare AS final
COPY --chown=appuser --from=build /app/publish .
ENTRYPOINT ["./BoilerPlate.Api.dll"]
