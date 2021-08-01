ENV:=Docker
MIGRATION:=Initial
SQLSERVER_VOLUME_NAME:=sge-sqlserver-volume
TEST_GUID:=null

###################
# SQL SERVER
###################

stop-sqlserver:
	docker stop sge-sqlserver || exit 0
	docker rm -f sge-sqlserver || exit 0

build-sqlserver:
	docker build \
		-f sqlserver.dockerfile \
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
# Back
###################

stop-back:
	docker stop sge-back || exit 0
	docker rm -f sge-back || exit 0

build-back:
	docker build \
		-f back.dockerfile \
		-t sge/back:latest .

run-back: stop-back
	docker run --network sge \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name sge-back -d \
		-p 5000:80 \
		sge/back:latest

run-back-it: stop-back
	docker run --network sge \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name sge-back -it \
		-p 5000:80 \
		sge/back:latest

###################
# Front
###################

stop-front:
	docker stop sge-front || exit 0
	docker rm -f sge-front || exit 0

build-front:
	docker build \
		-f front.dockerfile \
		-t sge/front:latest .

run-front: stop-front
	docker run --network sge \
		--name sge-front -d \
		-p 80:80 \
		sge/front:latest

run-front-it: stop-front
	docker run --network sge \
		--name sge-front -it \
		-p 80:80 \
		sge/front:latest

###################
# EXTRA
###################

run-test:
	cd ./xUnitTest && dotnet test --collect:"XPlat Code Coverage"

generate-test-report:
	reportgenerator \
		-reports:"/home/anderson/Documents/unifei/ultimo/lina/projeto/xUnitTest/TestResults/$(TEST_GUID)/coverage.cobertura.xml" \
		-targetdir:"coveragereport" \
		-reporttypes:Html

open-test-report:
	firefox coveragereport/index.html

create-network:
	docker network create sge || exit 0
	
create-volume:
	docker volume create $(SQLSERVER_VOLUME_NAME)

remove-volume:
	docker volume remove $(SQLSERVER_VOLUME_NAME)

build: create-network create-volume build-sqlserver build-back build-front
run: run-sqlserver run-back run-front
stop: stop-sqlserver stop-back stop-front
restart: stop build run

#LOGS
show-sqlserver: 
	docker logs -f sge-sqlserver
show-back: 
	docker logs -f sge-back
show-front: 
	docker logs -f sge-front

#DATABASE
create-migration:
	cd ./back && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations add $(MIGRATION) -o ./Context/Migrations

revert-migration:
	cd ./back && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef database update $(MIGRATION)

remove-migration:
	cd ./back && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations remove


#LOCAL
run-local: run-sqlserver run-local-back run-local-front

run-local-back:
	cd ./back &&  export ASPNETCORE_ENVIRONMENT=$(ENV) && \
	gnome-terminal -- bash -c "dotnet run  1>../back 2>../back.err"

run-local-front:
	cd ./front && npm start