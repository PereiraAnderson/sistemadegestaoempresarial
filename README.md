# sge

An ASP.NET Core API REST Application and Angular 10 project.

This repo have the code already done, but the instructions below will guide you throw all the steps required to set up the application.

## Requirements

Before we start, you'll need the following packages:
- Docker
- Make*

## Preparing the environment
```
bash 

# With Make
make build && make run

# Without Make - See more commands in Makefile
docker build -f sqlserver.dockerfile -t sge/sqlserver:latest .
docker build -f back.dockerfile -t sge/back:latest .
docker build -f front.dockerfile -t sge/front:latest .
docker network create sge
docker volume create sge-sqlserver-volume
docker run --network sge --name sge-sqlserver -it -v sge-sqlserver-volume:/var/opt/mssql -p 1433:1433 sge/sqlserver:latest
docker run --network sge --name sge-sqlserver -d -v sge-sqlserver-volume:/var/opt/mssql -p 1433:1433 sge/sqlserver:latest
docker run --network sge -e ASPNETCORE_ENVIRONMENT=Docker --name sge-back -d -p 5000:80 sge/back:latest
docker run --network sge --name sge-front -d -p 80:80 sge/front:latest

# Server will start at http://localhost:5000 and http://localhost
```

Try on https://sge.pereiraanderson.com/
