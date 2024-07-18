resource "azurerm_eventhub" "terrorform" {
  name                = "TerrorformEventHub"
  namespace_name      = azurerm_eventhub_namespace.terrorform.name
  resource_group_name = azurerm_resource_group.resource_group.name
  partition_count     = 2
  message_retention   = 1
}