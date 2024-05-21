# Introduction
This reposityry is 1 of 5 microservices in the organization "FadeFactory," which is a barber shop project for the "Development of Large Systems" (DLS) 2024 exam

# Continouse deployment
Github workflow automatically builds and deploys when the main branch is changed

# Hosting and observability
A Windows Web App is running at: https://fadefactorybookings.azurewebsites.net/swagger/index.html
The project is a REST API with swagger documentation

# Secrets
Secrets are stored in Github secrets

#Local development
Set up a local .env file with Connectionstring, Databasename and Containername from a database where you wish to store data.
Change directory to the root of the project -> cd .\BookingApi\ 
Type "Dotnet run" in your terminal
