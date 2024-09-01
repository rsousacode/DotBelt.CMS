FROM alpine:3.20 AS prepare
RUN apk add --no-cache libgcc gcc clang build-base zlib-dev
WORKDIR /app
RUN adduser -u 1000 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


FROM mcr.microsoft.com/dotnet/sdk:8.0.401-alpine3.20 AS build
RUN apk update && apk upgrade
RUN apk add --no-cache libgcc gcc clang build-base zlib-dev

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../../DotBelt/. .

RUN dotnet restore -r linux-musl-x64

WORKDIR "/src/DotBelt.CMS.API"
#RUN dotnet build "DotBelt.CMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DotBelt.CMS.API.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish  -r linux-musl-x64 --self-contained

FROM prepare AS final
COPY --chown=appuser --from=build /app/publish .
ENTRYPOINT ["./DotBelt.CMS.API"]
