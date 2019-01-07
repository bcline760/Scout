terraform {
  backend "azurerm" {
    storage_account_name = "bclinesharedstorage"
    container_name       = "terraformstate"
    key                  = "scout.tfstate"
  }
}

provider "azurerm" {
  version = "=1.20.0"
}

resource "azurerm_resource_group" "scout-rg" {
  name     = "scout-westus-rg"
  location = "West US"
}

resource "azurerm_cosmosdb_account" "db" {
  name                              = "scoutdb"
  location                          = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name               = "${azurerm_resource_group.scout-rg.name}"
  offer_type                        = "Standard"
  kind                              = "MongoDB"
  is_virtual_network_filter_enabled = "false"

  enable_automatic_failover = true

  consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 10
    max_staleness_prefix    = 200
  }

  geo_location {
    location          = "${azurerm_resource_group.scout-rg.location}"
    failover_priority = 0
  }
}

resource "azurerm_app_service_plan" "scout-asp" {
  name                = "scout-asp"
  location            = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name = "${azurerm_resource_group.scout-rg.name}"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "scout-appservice" {
  name                    = "scout-appsvc"
  location                = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name     = "${azurerm_resource_group.scout-rg.name}"
  app_service_plan_id     = "${azurerm_app_service_plan.scout-asp.id}"
  https_only              = "true"
  client_affinity_enabled = "true"

  site_config {
    dotnet_framework_version = "v4.0"
    scm_type                 = "LocalGit"
  }
}

#9a3e2a5b-881f-4cc6-83ac-01596615e6b0