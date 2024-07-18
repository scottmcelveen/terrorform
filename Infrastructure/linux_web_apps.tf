resource "azurerm_linux_web_app" "terrorform" {
  name                = "terrorform"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_service_plan.terrorform.location
  service_plan_id     = azurerm_service_plan.terrorform.id

  site_config {
    always_on = false
  }
}