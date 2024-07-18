resource "azurerm_eventhub_namespace" "terrorform" {
  name                = "TerrorformEventHubNS"
  location            = azurerm_resource_group.resource_group.location
  resource_group_name = azurerm_resource_group.resource_group.name
  sku                 = "Basic"
  capacity            = 1
}