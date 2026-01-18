from PIL import Image
import os
import shutil

def process_branding():
    source_img = "jidon.webp"
    
    if not os.path.exists(source_img):
        print(f"Error: {source_img} not found.")
        return

    print("Opening source image...")
    img = Image.open(source_img)
    
    # Save as PNG
    img.save("EDPStrap.png", "PNG")
    print("Saved EDPStrap.png")

    # Generate ICO
    # Standard Windows sizes
    sizes = [(256, 256), (128, 128), (64, 64), (48, 48), (32, 32), (16, 16)]
    img.save("EDPStrap.ico", "ICO", sizes=sizes)
    print("Generated EDPStrap.ico")

    # Paths to replace
    # 1. Main Application Icon (in root of project and resources)
    shutil.copy("EDPStrap.ico", "Bloxstrap/EDPStrap.ico")
    shutil.copy("EDPStrap.ico", "Bloxstrap/Resources/IconEDPStrap.ico")
    
    # 2. Bootstrapper Dialog Logos (Byfron style)
    # These are typically wide, but we'll fit the logo or stretch it?
    # Or just replace it centered. Let's just save the PNG as these for now.
    # Actually, ByfronDialog uses JPGs.
    rgb_img = img.convert('RGB')
    rgb_img.save("Bloxstrap/Resources/BootstrapperStyles/ByfronDialog/ByfronLogoDark.jpg", "JPEG")
    rgb_img.save("Bloxstrap/Resources/BootstrapperStyles/ByfronDialog/ByfronLogoLight.jpg", "JPEG")
    
    # Check if there are other places
    # IconBloxstrap.ico is used as fallback, let's overwrite that too just in case
    shutil.copy("EDPStrap.ico", "Bloxstrap/Resources/IconBloxstrap.ico")
    
    print("Branding assets updated successfully.")

if __name__ == "__main__":
    process_branding()
