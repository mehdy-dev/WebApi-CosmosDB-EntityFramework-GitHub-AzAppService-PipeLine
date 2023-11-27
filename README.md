---
page_type: sample
description: "Automated pipeline to deploy web api FE and cosmodb BE using arm template"
products:
- GitHub Actions
- Azure Web Apps
- Azure Cosmo DB NoSQL API
languages:
- dotnet
---
# technologies used :

* .netCore web api and swagger
* CosmoDB
* EntityFramework API for CosmoDB
* ARM Template to create and deploy web app
* Azure App service
* Customized DBContext to initialize and seed cosmo db collections

---
# Porposes :
* CI/CD simplifies development pipeline using git actions to deploy app to app service
   -> using azure app service deployment slots are recommended when pipeline triggered by git action
* CosmoDB Entity framework to elaborate faster with data access layer
* Using a seperate logic and db context when cosmodb model changes (newly container added to Cosmo DB) then seed the container with new items
   -> change the initializer db context class and update the data rather than using the dbcontext serving the api services
* useage application initialization to create and seed new model IHostedService of Microsoft.Extensions.Hosting

---
# Steps :
* 1- by cloning the git repo you will have :
  * a) web api
  * b) infrastructure service to create db services create db and collections then if model changed seed containers using json file
  * c) Core project which contains models and api services

* 2- Register an app in azure 
* 3- create a RG 
* 4- Add RBAC for the app to RG (we will use RG Scope during ARM Deployment)
* 5- create a federation between github subject repo and azure app (github action->subjects to authenticate using microsoft graph -> acces RG -> deploy app)

```
az ad app create --display-name myApp
az group create --name {resource-group-name} --location {resource-group-location}
az role assignment create --role contributor --scope /subscriptions/$subscriptionId/resourceGroups/$resourceGroupName --subscription $subscriptionId --assignee-object-id  $assigneeObjectId --assignee-principal-type ServicePrincipal
az rest --method POST --uri 'https://graph.microsoft.com/beta/applications/<APPLICATION-OBJECT-ID>/federatedIdentityCredentials' --body '{"name":"<CREDENTIAL-NAME>","issuer":"https://token.actions.githubusercontent.com","subject":"repo:organization/repository:ref:refs/heads/main","description":"Testing","audiences":["api://AzureADTokenExchange"]}'
```
find the detailed ateps here - > https://learn.microsoft.com/en-us/azure/app-service/app-service-sql-asp-github-actions

6 - Add Arm Template to your project


# ASP.NET Core and Azure Cosmo DB NoSQL Api for GitHub Actions

This repo contains a sample ASP.NET Core web application which uses an Azure Cosmo DB NoSQL as a backend. The web app can be deployed to Azure Web Apps by using GitHub Actions. The workflow for this is already present in the [.github](.github) folder.

## Setup an end-to-end CI/CD workflow

License

Acknowledgments
Good Article how to use entity frame work and cosmosDB : https://medium.com/@kevinwilliams.dev/ef-core-cosmos-db-3da250b47d6c
Use Git action and ARM template to deploy webapp into azure app service 

