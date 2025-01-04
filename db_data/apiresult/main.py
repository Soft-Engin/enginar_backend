import os
from PIL import Image

def resize_images_max_dimension(input_folder, output_folder, max_dimension=300):
    """
    Resizes all images in the input_folder so that the largest dimension is max_dimension pixels.
    Maintains the aspect ratio and saves the resized images to the output_folder.

    :param input_folder: Path to the folder containing original images.
    :param output_folder: Path to the folder where resized images will be saved.
    :param max_dimension: The maximum size for the largest dimension (width or height).
    """
    # Create the output folder if it doesn't exist
    if not os.path.exists(output_folder):
        os.makedirs(output_folder)
        print(f"Created output directory: {output_folder}")

    # Supported image extensions
    supported_extensions = ('.jpg', '.jpeg', '.png', '.bmp', '.gif', '.tiff')

    # Iterate over all files in the input folder
    for filename in os.listdir(input_folder):
        if filename.lower().endswith(supported_extensions):
            input_path = os.path.join(input_folder, filename)
            output_path = os.path.join(output_folder, filename)
            try:
                with Image.open(input_path) as img:
                    original_size = img.size  # (width, height)
                    
                    # Determine the scaling factor to maintain aspect ratio
                    scaling_factor = min(max_dimension / original_size[0], max_dimension / original_size[1])

                    # If the image is smaller than the max_dimension, keep it as is
                    if scaling_factor >= 1:
                        resized_size = original_size
                        img_copy = img.copy()
                    else:
                        resized_size = (int(original_size[0] * scaling_factor), int(original_size[1] * scaling_factor))
                        img_copy = img.resize(resized_size, Image.Resampling.LANCZOS)  # Updated line
                        
                    img_copy.save(output_path)
                    print(f"Resized {filename}: {original_size} -> {resized_size}")
            except Exception as e:
                print(f"Error processing {filename}: {e}")

if __name__ == "__main__":
    # Specify the input and output directories
    input_dir = "."    # Replace with your input folder path
    output_dir = "./out"  # Replace with your output folder path

    # Maximum dimension for the largest side
    max_dim = 100

    resize_images_max_dimension(input_dir, output_dir, max_dim)
