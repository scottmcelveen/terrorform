resource "azurerm_service_plan" "terrorform" {
  name                = "terrorform"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  os_type             = "Linux"
  sku_name            = "F1"
}