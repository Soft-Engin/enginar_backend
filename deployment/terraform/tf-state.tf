# resource "aws_s3_bucket" "enginar_tf_state" {
#   bucket        = "enginar-tf-state-bucket"
#   force_destroy = true
# }

# resource "aws_s3_bucket_versioning" "enginar_tf_state" {
#   bucket = aws_s3_bucket.enginar_tf_state.bucket
#   versioning_configuration {
#     status = "Enabled"
#   }
# }

# resource "aws_s3_bucket_server_side_encryption_configuration" "enginar_tf_state" {
#   bucket = aws_s3_bucket.enginar_tf_state.bucket
#   rule {
#     apply_server_side_encryption_by_default {
#       sse_algorithm = "AES256"
#     }
#   }
# }

# resource "aws_dynamodb_table" "enginar_tf_state_lock" {
#   name         = "enginar-tf-state-lock"
#   billing_mode = "PAY_PER_REQUEST"
#   hash_key     = "LockID"

#   attribute {
#     name = "LockID"
#     type = "S"
#   }
# }