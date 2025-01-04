# Pulls the latest Amazon Linux 2023 AMI for desired architecture.
data "aws_ami" "amazonlinux_2023" {
  most_recent = true
  owners      = ["amazon"]
  filter {
    name   = "name"
    values = ["al2023-ami-*-kernel-6.1-x86_64"] # x86_64
    # values = [ "al2023-ami-*-kernel-6.1-arm64" ] # ARM
    # values = [ "al2023-ami-minimal-*-kernel-6.1-x86_64" ] # Minimal Image (x86_64)
    # values = [ "al2023-ami-minimal-*-kernel-6.1-arm64" ] # Minimal Image (ARM)
  }
}

# Pulls the latest Amazon Linux 2 AMI for desired architecture.
data "aws_ami" "amazonlinux_2" {
  most_recent = true
  owners      = ["amazon"]
  filter {
    name   = "name"
    values = ["amzn2-ami-hvm-*-x86_64-gp2"] # x86_64
    # values = [ "amzn2-ami-hvm-*-arm64-gp2" ] # ARM
    # values = [ "amzn2-ami-minimal-hvm-*-x86_64-ebs" ] # Minimal Image (x86_64)
    # values = [ "amzn2-ami-minimal-hvm-*-arm64-ebs" ] # Minimal Image (ARM)
  }
}