provider "azurerm" {
  version = "=1.20.0"
}

data "azurerm_client_config" "scout_config" {}

data "azurerm_resource_group" "scout-rg" {
  name = "scout"
}

resource "azurerm_cosmosdb_account" "db" {
  name                              = "scoutdb"
  location                          = "${data.azurerm_resource_group.scout-rg.location}"
  resource_group_name               = "${data.azurerm_resource_group.scout-rg.name}"
  offer_type                        = "Standard"
  kind                              = "MongoDB"
  is_virtual_network_filter_enabled = "true"

  enable_automatic_failover = true

  consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 10
    max_staleness_prefix    = 200
  }

  geo_location {
    location          = "${data.azurerm_resource_group.scout-rg.location}"
    failover_priority = 0
  }

  virtual_network_rule = {
    id = "${azurerm_subnet.scout-subnet-cosmosdb.id}"
  }
}

resource "azurerm_app_service_plan" "scout-asp" {
  name                = "scout-asp"
  location            = "${data.azurerm_resource_group.scout-rg.location}"
  resource_group_name = "${data.azurerm_resource_group.scout-rg.name}"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "scout-appservice" {
  name                    = "scout-appsvc"
  location                = "${data.azurerm_resource_group.scout-rg.location}"
  resource_group_name     = "${data.azurerm_resource_group.scout-rg.name}"
  app_service_plan_id     = "${azurerm_app_service_plan.scout-asp.id}"
  https_only              = "true"
  client_affinity_enabled = "true"

  site_config {
    dotnet_framework_version = "v4.0"
    scm_type                 = "LocalGit"
    virtual_network_name     = "${azurerm_virtual_network.scout-vnet.name}"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_key_vault" "scout_keyvault" {
  name                = "scout-keyvault"
  resource_group_name = "${data.azurerm_resource_group.scout-rg.name}"

  sku {
    name = "Premium"
  }

  access_policy {
    tenant_id = "${data.azurerm_client_config.scout_config.tenant_id}"
    object_id = "${data.azurerm_client_config.scout_config.service_principal_object_id}"

    # backup, create, decrypt, delete, encrypt, get, import, list, purge, recover, restore, sign, unwrapKey, update, verify and wrapKey
    key_permissions = [
      "backup",
      "create",
      "delete",
      "get",
      "import",
      "list",
      "purge",
      "recover",
      "restore",
      "update",
    ]

    # create, delete, deleteissuers, get, getissuers, import, list, listissuers, managecontacts, manageissuers, purge, recover, setissuers and update
    certificate_permissions = [
      "create",
      "delete",
      "get",
      "import",
      "list",
      "purge",
      "recover",
      "update",
    ]

    # backup, delete, get, list, purge, recover, restore and set
    secret_permissions = [
      "backup",
      "delete",
      "get",
      "list",
      "purge",
      "recover",
      "restore",
      "set",
    ]
  }

  access_policy {
    tenant_id = "${azurerm_app_service.scout-appservice.identity.tenant_id}"
    object_id = "${azurerm_app_service.scout-appservice.identity.principal_id}"

    # backup, create, decrypt, delete, encrypt, get, import, list, purge, recover, restore, sign, unwrapKey, update, verify and wrapKey
    key_permissions = [
      "get",
      "wrapKey",
      "encrypt",
      "decrypt",
      "unwrapKey",
      "sign",
    ]

    # create, delete, deleteissuers, get, getissuers, import, list, listissuers, managecontacts, manageissuers, purge, recover, setissuers and update
    certificate_permissions = [
      "get",
    ]

    # backup, delete, get, list, purge, recover, restore and set
    secret_permissions = [
      "get",
    ]
  }

  network_acls {
    bypass                     = "AzureServices"
    virtual_network_subnet_ids = ["${azurerm_subnet.scout-subnet-keyvault.id}"]
    default_action             = "Deny"
    ip_rules                   = ["70.166.83.0/24"]
  }
}

resource "azurerm_key_vault_certificate" "signing_cert" {
  name      = "scout-signing-certificate"
  vault_uri = "${azurerm_key_vault.scout_keyvault.vault_uri}"

  certificate_policy {
    issuer_parameters {
      name = "Self"
    }

    key_properties {
      exportable = true
      key_size   = 2048
      key_type   = "RSA"
      reuse_key  = false
    }

    lifetime_action {
      action {
        action_type = "AutoRenew"
      }

      trigger {
        days_before_expiry = 30
      }
    }

    x509_certificate_properties {
      # Server Authentication = 1.3.6.1.5.5.7.3.1
      # Client Authentication = 1.3.6.1.5.5.7.3.2
      extended_key_usage = ["1.3.6.1.5.5.7.3.2"]

      key_usage = [
        "cRLSign",
        "dataEncipherment",
        "digitalSignature",
        "keyAgreement",
        "keyCertSign",
        "keyEncipherment",
      ]

      subject_alternative_names {
        dns_names = ["www.scout.cloud", "domain.hello.world"]
      }

      subject            = "CN=scout-signing-certificate"
      validity_in_months = 12
    }
  }
}
