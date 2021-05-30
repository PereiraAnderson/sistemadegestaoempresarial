FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

WORKDIR /sqlserver/

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=KQSXJVSnjCQHFvxaePD5p5cey5wyzn3F

ENV PATH="/opt/mssql-tools/bin:${PATH}"

COPY . .

EXPOSE 1433
