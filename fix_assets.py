from PIL import Image
import shutil
import os

def fix_assets():
    print("Fixing assets...")
    
    # 1. Fix Icons by using known good Icon2022.ico
    good_icon = "Bloxstrap/Resources/Icon2022.ico"
    targets = [
        "Bloxstrap/EDPStrap.ico",
        "Bloxstrap/Resources/IconEDPStrap.ico",
        "Bloxstrap/Resources/IconBloxstrap.ico"
    ]
    
    if os.path.exists(good_icon):
        for target in targets:
            try:
                shutil.copy(good_icon, target)
                print(f"Restored {target} from {good_icon}")
            except Exception as e:
                print(f"Failed to copy icon to {target}: {e}")
    else:
        print("Error: Icon2022.ico not found!")

    # 2. Fix JPGs by generating fresh valid JPEGs (simple gray square)
    # This avoids any issues with the user's webp file
    try:
        img = Image.new('RGB', (500, 300), color = (50, 50, 50))
        
        jpg_targets = [
            "Bloxstrap/Resources/BootstrapperStyles/ByfronDialog/ByfronLogoDark.jpg",
            "Bloxstrap/Resources/BootstrapperStyles/ByfronDialog/ByfronLogoLight.jpg"
        ]
        
        for target in jpg_targets:
            img.save(target, "JPEG")
            print(f"Generated safe placeholder for {target}")
            
    except Exception as e:
        print(f"Failed to generate JPGs: {e}")

if __name__ == "__main__":
    fix_assets()
