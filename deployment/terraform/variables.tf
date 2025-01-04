#####################################################
# Variables for the AWS provider
#####################################################

variable "aws_region" {
  type        = string
  description = "AWS region to deploy resources"
  default     = "eu-central-1"
}

#####################################################
# Variables for the EC2 instance
#####################################################

variable "instance_type" {
  type        = string
  description = "EC2 instance type."
  default     = "t2.micro"
}






