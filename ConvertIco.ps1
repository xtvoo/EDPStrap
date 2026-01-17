
Add-Type -AssemblyName System.Drawing

function Convert-PngToIco {
    param (
        [string]$SourcePng,
        [string]$DestinationIco
    )

    $srcImage = [System.Drawing.Bitmap]::FromFile($SourcePng)
    $sizes = @(16, 32, 48, 64, 128, 256)
    
    # We need to construct the ICO file manually because System.Drawing.Icon.Save() doesn't support high-quality multi-size creation easily without external libraries, 
    # BUT, we can use a simpler approach: just create a single-size icon or try to use a stream approach. 
    # Actually, the most robust way without external tools in pure PS/dotnet is tricky.
    # Let's try to just create a single 256x256 icon first, or standard 48x48 if 256 fails.
    # However, for ApplicationIcon, we really want multiple sizes.
    
    # Let's try a proven simple method: resizing the bitmap and saving as Icon.
    # Note: Bitmap.Save(stream, ImageFormat.Icon) produces low-quality or invalid icons often.
    # Using specific header writing is safer.

    $memStream = New-Object System.IO.MemoryStream
    $bw = New-Object System.IO.BinaryWriter($memStream)

    # Write ICO Header
    $bw.Write([int16]0)   # Reserved
    $bw.Write([int16]1)   # Type 1 = Icon
    $bw.Write([int16]$sizes.Count) # Count of images

    $imageBuffers = @()
    $offset = 6 + (16 * $sizes.Count)

    foreach ($size in $sizes) {
        $resized = new-object System.Drawing.Bitmap($size, $size)
        $g = [System.Drawing.Graphics]::FromImage($resized)
        $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
        $g.DrawImage($srcImage, 0, 0, $size, $size)
        $g.Dispose()

        $imgStream = New-Object System.IO.MemoryStream
        $resized.Save($imgStream, [System.Drawing.Imaging.ImageFormat]::Png)
        $buffer = $imgStream.ToArray()
        $imageBuffers += ,$buffer
        
        # Write Icon Directory Entry
        $width = if ($size -eq 256) { 0 } else { $size }
        $height = if ($size -eq 256) { 0 } else { $size }
        
        $bw.Write([byte]$width)
        $bw.Write([byte]$height)
        $bw.Write([byte]0) # Colors (0 means >=8bpp)
        $bw.Write([byte]0) # Reserved
        $bw.Write([int16]0) # Color planes
        $bw.Write([int16]32) # Bit count
        $bw.Write([int]$buffer.Length) # Size in bytes
        $bw.Write([int]$offset) # Offset of data
        
        $offset += $buffer.Length
    }

    foreach ($buffer in $imageBuffers) {
        $bw.Write($buffer)
    }
    
    $bw.Flush()
    [System.IO.File]::WriteAllBytes($DestinationIco, $memStream.ToArray())
    
    $srcImage.Dispose()
    $memStream.Dispose()
    Write-Host "Generated $DestinationIco"
}

# Convert the source logo to ICO
Convert-PngToIco -SourcePng "c:\Users\hayde\.gemini\antigravity\scratch\EDPStrap\Images\Bloxstrap.png" -DestinationIco "c:\Users\hayde\.gemini\antigravity\scratch\EDPStrap\Bloxstrap\EDPStrap.ico"
Convert-PngToIco -SourcePng "c:\Users\hayde\.gemini\antigravity\scratch\EDPStrap\Images\Bloxstrap.png" -DestinationIco "c:\Users\hayde\.gemini\antigravity\scratch\EDPStrap\Bloxstrap\Resources\IconEDPStrap.ico"
