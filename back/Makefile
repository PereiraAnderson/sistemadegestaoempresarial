ENV:=Docker
MIGRATION:=Initial
SQLSERVER_VOLUME_NAME:=sge-sqlserver-volume

###################
# SQL SERVER
###################

stop-sqlserver:
	docker stop sge-sqlserver || exit 0
	docker rm -f sge-sqlserver || exit 0

build-sqlserver:
	docker build \
		-f Docker/sqlserver.dockerfile \
		-t sge/sqlserver:latest .

run-sqlserver: stop-sqlserver create-volume
	docker run --network sge \
		--name sge-sqlserver -d \
		-v $(SQLSERVER_VOLUME_NAME):/var/opt/mssql \
		-p 1433:1433 \
		sge/sqlserver:latest

run-sqlserver-it: stop-sqlserver create-volume
	docker run --network sge \
		--name sge-sqlserver -it \
		-v $(SQLSERVER_VOLUME_NAME):/var/opt/mssql \
		-p 1433:1433 \
		sge/sqlserver:latest

###################
# API
###################

stop-api:
	docker stop sge-api || exit 0
	docker rm -f sge-api || exit 0

build-api:
	docker build \
		-f Docker/api.dockerfile \
		-t sge/api:latest .

run-api: stop-api
	docker run --network sge \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name sge-api -d \
		-p 5000:80 \
		sge/api:latest

run-api-it: stop-api
	docker run --network sge \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name sge-api -it \
		-p 5000:80 \
		sge/api:latest

###################
# EXTRA
###################

create-network:
	docker network create sge || exit 0
	
create-volume:
	docker volume create $(SQLSERVER_VOLUME_NAME)

remove-volume:
	docker volume remove $(SQLSERVER_VOLUME_NAME)

build: create-network create-volume build-sqlserver build-api
run: run-sqlserver run-api
stop: stop-sqlserver stop-api
restart: stop build run

# LOGS
show-sqlserver: 
	docker logs -f sge-sqlserver
show-api: 
	docker logs -f sge-api

# DATABASE
create-migration:
	export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations add $(MIGRATION) -o ./Context/Migrations

revert-migration:
	export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef database update $(MIGRATION)

remove-migration:
	export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations remove