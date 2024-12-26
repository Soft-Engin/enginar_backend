#####################################################
# Variables for the AWS provider
#####################################################

variable "aws_region" {
  type        = string
  description = "AWS region to deploy resources"
  default     = "eu-central-1"
}

variable "aws_profile" {
  type        = string
  description = "AWS profile to use"
  default     = "default"
}

#####################################################
# Variables for the EC2 instance
#####################################################

variable "ami_id" {
  type        = string
  description = "AMI ID to use for the web server."
  default     = data.aws_ami.amazonlinux_2023.id
}

variable "instance_type" {
  type        = string
  description = "EC2 instance type."
  default     = "t2.micro"
}

variable "key_name" {
  type        = string
  description = "Name of the existing EC2 Key Pair to associate with the instance."
  default     = "instance-key"
}






