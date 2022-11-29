az group delete --name Garnek
az group create --name Garnek --location "West Europe" 
az appservice plan create --name garnekServicePlan --resource-group Garnek --sku F1 --is-linux 
az webapp create --resource-group Garnek  --plan garnekServicePlan --name garnekwebapi --deployment-container-image-name bartlomiejpierog/garnek-webapi:latest
az webapp create --resource-group Garnek  --plan garnekServicePlan --name garnek --deployment-container-image-name bartlomiejpierog/front-end-webapi:latest


