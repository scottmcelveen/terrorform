variable "aws_region" {
    description = "The AWS region in which the resources will be created."
    type    = string
    default = "ukwest"
}

variable "resource_group_name" {
    description = "The name of the resource group in which the resources will be created."
    type    = string
    default = "terrorform"
}