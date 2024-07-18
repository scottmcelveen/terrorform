# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.112.0"
    }
    azuread = {
      source = "hashicorp/azuread"
      version = "~> 2.53.1"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  features {}
}
provider "azuread" {
  # Configuration options
}