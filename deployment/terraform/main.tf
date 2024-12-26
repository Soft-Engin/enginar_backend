terraform {
  required_providers {
    aws = {
      source = "hashicorp/aws"
      version = "5.82.2"
    }
  }
}

provider "aws" {
  region = var.aws_region
  profile = var.aws_profile
}

module "ec2_instance" {
  source  = "terraform-aws-modules/ec2-instance/aws"
  version = "5.7.1"

  name = "enginar-project"

  instance_type          = "t2.micro"
  ami = data.aws_ami.amazonlinux_2023.id
  key_name               = "enginar-key"
  vpc_security_group_ids = [aws_security_group.allow_access.id]
  associate_public_ip_address = true
}