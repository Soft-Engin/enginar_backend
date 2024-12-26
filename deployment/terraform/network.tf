resource "aws_security_group" "allow_access" {
  name        = "allow_access"
  description = "Allow incoming HTTP and SSH traffic"
}

# Allow SSH access from anywhere.
resource "aws_vpc_security_group_ingress_rule" "allow_ssh" {
  security_group_id = aws_security_group.allow_access.id

  description = "Allow SSH"
  from_port   = 22
  to_port     = 22
  ip_protocol = "tcp"
  cidr_ipv4   = ["0.0.0.0/0"]
}

# Allow HTTP access from anywhere.
resource "aws_vpc_security_group_ingress_rule" "allow_https" {
  security_group_id = aws_security_group.allow_access.id

  description = "Allow HTTP"
  from_port   = 80
  to_port     = 80
  ip_protocol = "tcp"
  cidr_ipv4   = ["0.0.0.0/0"]
}

# Allow HTTPS access from anywhere.
resource "aws_vpc_security_group_ingress_rule" "allow_https" {
  security_group_id = aws_security_group.allow_access.id

  description = "Allow HTTPS"
  from_port   = 443
  to_port     = 443
  ip_protocol = "tcp"
  cidr_ipv4   = ["0.0.0.0/0"]
}

# Allow all outbound traffic.
resource "aws_vpc_security_group_egress_rule" "allow_all" {
  security_group_id = aws_security_group.allow_access.id

  from_port   = 22
  to_port     = 22
  ip_protocol = "-1"
  cidr_ipv4   = ["0.0.0.0/0"]
}
