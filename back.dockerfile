FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY /back .

RUN dotnet publish -c Release -p:PublishDir=out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY --from=build-env /app/out/ .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "SGE.dll"]
