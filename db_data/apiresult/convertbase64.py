import base64
from pathlib import Path

# Configure this path as needed
INPUT_DIRECTORY = "./downsampled"  # Change this to your input directory
OUTPUT_FILE = "./downsampled/converted_images.txt"  # Change this if you want a different output filename

def convert_jpg_to_base64(file_path):
    """
    Convert a JPG/JPEG file to base64 string
    
    Args:
        file_path (str): Path to the JPG file
        
    Returns:
        str: Base64 encoded string
        
    Raises:
        FileNotFoundError: If the file doesn't exist
        ValueError: If the file isn't a JPG/JPEG
    """
    file_path = Path(file_path)
    
    if not file_path.exists():
        raise FileNotFoundError(f"File not found: {file_path}")
    
    if file_path.suffix.lower() not in ['.jpg', '.jpeg']:
        raise ValueError(f"File must be a JPG/JPEG: {file_path}")
    
    try:
        with open(file_path, 'rb') as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode('utf-8')
            return encoded_string
    except Exception as e:
        raise Exception(f"Error converting file: {str(e)}")

def process_directory():
    """
    Process all JPG/JPEG files in the input directory and its subdirectories
    and save results in the specified format
    """
    input_dir = Path(INPUT_DIRECTORY)
    successful = []
    failed = []
    
    with open(OUTPUT_FILE, 'w') as f:
        # Walk through directory recursively
        for file_path in input_dir.rglob('*'):
            if file_path.suffix.lower() in ['.jpg', '.jpeg']:
                try:
                    # Convert the image
                    base64_string = convert_jpg_to_base64(file_path)
                    
                    # Write in the specified format
                    f.write(f'Convert.FromBase64String("{base64_string}"),\n')
                    
                    successful.append(file_path)
                    
                except Exception as e:
                    failed.append((file_path, str(e)))
    
    # Print summary to console
    print(f"Successfully converted: {len(successful)} files")
    if failed:
        print("\nFailed conversions:")
        for path, error in failed:
            print(f"  {path}: {error}")

def main():
    try:
        print(f"Starting conversion of JPG files from: {INPUT_DIRECTORY}")
        process_directory()
        print(f"Conversion complete. Results saved to: {OUTPUT_FILE}")
    except Exception as e:
        print(f"Error: {str(e)}")

if __name__ == "__main__":
    main()