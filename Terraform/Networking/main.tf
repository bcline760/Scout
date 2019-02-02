provider "azurerm" {
  version = "=1.20.0"
}

data "azurerm_client_config" "scout_config" {}

resource "azurerm_resource_group" "scout-rg" {
  name     = "scout"
  location = "West US"
}

resource "azurerm_virtual_network" "scout-vnet" {
  name                = "scout-vnet"
  resource_group_name = "${azurerm_resource_group.scout-rg.name}"
  address_space       = ["10.0.0.0/16"]
  location            = "West US"

  tags {
    Environment = "Production"
  }
}

resource "azurerm_subnet" "scout-subnet-app" {
  name                 = "scout-subnet-app"
  resource_group_name  = "${azurerm_resource_group.scout-rg.name}"
  virtual_network_name = "${azurerm_virtual_network.scout-vnet.name}"
  address_prefix       = "10.0.1.0/28"
}

resource "azurerm_subnet" "scout-subnet-cosmosdb" {
  name                 = "scout-subnet-cosmosdb"
  resource_group_name  = "${azurerm_resource_group.scout-rg.name}"
  virtual_network_name = "${azurerm_virtual_network.scout-vnet.name}"
  address_prefix       = "10.0.2.0/28"
  service_endpoints    = ["Microsoft.AzureCosmosDB"]
}

resource "azurerm_subnet" "scout-subnet-keyvault" {
  name                 = "scout-subnet-keyvault"
  resource_group_name  = "${azurerm_resource_group.scout-rg.name}"
  virtual_network_name = "${azurerm_virtual_network.scout-vnet.name}"
  address_prefix       = "10.0.3.0/28"
  service_endpoints    = ["Microsoft.KeyVault"]
}

resource "azurerm_subnet" "scout-subnet-firewall" {
  name                 = "AzureFirewallSubnet"
  resource_group_name  = "${azurerm_resource_group.scout-rg.name}"
  virtual_network_name = "${azurerm_virtual_network.scout-vnet.name}"
  address_prefix       = "10.0.4.0/24"
}

resource "azurerm_network_security_group" "scout-nsg-database" {
  name                = "scout-nsg-database"
  location            = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name = "${azurerm_resource_group.scout-rg.name}"

  security_rule {
    name                       = "AllowOnlyVnetTrafficIn"
    description                = "Allow only internet traffic in"
    source_port_range          = "*"
    destination_port_range     = "443"
    source_address_prefix      = "10.0.1.0/28"
    destination_address_prefix = "10.0.2.0/28"
    direction                  = "Inbound"
    protocol                   = "Tcp"
    priority                   = "766"
    access                     = "Allow"
  }

  tags {
    Environment = "Production"
  }
}

resource "azurerm_network_security_group" "scout-nsg-app" {
  name                = "scout-nsg-app"
  location            = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name = "${azurerm_resource_group.scout-rg.name}"

  security_rule {
    name                       = "AllowHttpsOnlyApp"
    description                = "Allow only internet traffic in"
    source_port_range          = "*"
    destination_port_range     = "443"
    source_address_prefix      = "Internet"
    destination_address_prefix = "10.0.1.0/28"
    direction                  = "Inbound"
    protocol                   = "Tcp"
    priority                   = "256"
    access                     = "Allow"
  }

  tags {
    Environment = "Production"
  }
}

resource "azurerm_network_security_group" "scout-nsg-keyvault" {
  name                = "scout-nsg-app"
  location            = "${azurerm_resource_group.scout-rg.location}"
  resource_group_name = "${azurerm_resource_group.scout-rg.name}"

  security_rule {
    name                       = "AllowVirtualNetwork"
    description                = "Allow only internet traffic in"
    source_port_range          = "*"
    destination_port_range     = "*"
    source_address_prefix      = "VirtualNetwork"
    destination_address_prefix = "10.0.3.0/28"
    direction                  = "Inbound"
    protocol                   = "Tcp"
    priority                   = "256"
    access                     = "Allow"
  }

  tags {
    Environment = "Production"
  }
}

resource "azurerm_subnet_network_security_group_association" "protect-subnet-db" {
  subnet_id                 = "${azurerm_subnet.scout-subnet-cosmosdb.id}"
  network_security_group_id = "${azurerm_network_security_group.scout-nsg-database.id}"
}

resource "azurerm_subnet_network_security_group_association" "protect-subnet-app" {
  subnet_id                 = "${azurerm_subnet.scout-subnet-app.id}"
  network_security_group_id = "${azurerm_network_security_group.scout-nsg-app.id}"
}

resource "azurerm_subnet_network_security_group_association" "protect-subnet-keyvault" {
  subnet_id                 = "${azurerm_subnet.scout-subnet-keyvault.id}"
  network_security_group_id = "${azurerm_network_security_group.scout-nsg-keyvault.id}"
}