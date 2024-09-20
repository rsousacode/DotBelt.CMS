using System.Text;
using SkiaSharp;
using Path = System.IO.Path;

namespace DotBelt.CMS.Shared.CMS.Media;

public static class CropProcessor
{
    public static string GetCropFileName(int? width, int? height, string fileName)
    {
        var sb = new StringBuilder();

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

        if (width != null && height != null)
        {
            sb.Append($"_{width}_{height}");
        }

        else if (width != null)
        {
            sb.Append($"_{width}px_any");
        }

        else if (height != null)
        {
            sb.Append($"_{height}px_any");
        }

        return $"{fileNameWithoutExtension}{sb}.webp";
        
    }

public static string CropImage(Stream imageStream, int? width, int? height, string fileName)
{
    fileName = GetCropFileName(width, height, fileName);

    using (var input = SKBitmap.Decode(imageStream))
    {
        if (width == null)
            throw new NotImplementedException("Crop image with no width not implemented");

        if (height == null)
            throw new NotImplementedException("Crop image with no height not implemented");

        var frameWidth = width.Value;
        var frameHeight = height.Value;

        // Calculate the aspect ratio of the frame and the input image
        float frameAspectRatio = (float)frameWidth / frameHeight;
        float inputAspectRatio = (float)input.Width / input.Height;

        // Calculate the cropping bounds
        int cropWidth, cropHeight;
        if (frameAspectRatio > inputAspectRatio)
        {
            // Frame is wider than the input image; crop height
            cropWidth = input.Width;
            cropHeight = (int)(input.Width / frameAspectRatio);
        }
        else
        {
            // Frame is taller than the input image; crop width
            cropWidth = (int)(input.Height * frameAspectRatio);
            cropHeight = input.Height;
        }

        // Calculate the cropping offset
        int cropOffsetX = (input.Width - cropWidth) / 2;
        int cropOffsetY = (input.Height - cropHeight) / 2;

        // Define the cropping rectangle
        var cropRect = new SKRectI(cropOffsetX, cropOffsetY, cropOffsetX + cropWidth, cropOffsetY + cropHeight);

        // Create cropped and resized bitmap
        var croppedBitmap = new SKBitmap(frameWidth, frameHeight);
        var paint = new SKPaint
        {
            FilterQuality = SKFilterQuality.High,
            IsAntialias = true
        };

        using (var canvas = new SKCanvas(croppedBitmap))
        {
            canvas.Clear(SKColors.Transparent); // Optional: Clear with white or any background color
            var destRect = new SKRect(0, 0, frameWidth, frameHeight);
            canvas.DrawBitmap(input, cropRect, destRect, paint);
        }

        var pathToSave = UploadsController.GetFullPath(fileName);

        using (var image = SKImage.FromBitmap(croppedBitmap))
        using (var data = image.Encode(SKEncodedImageFormat.Webp, 100))
        using (var stream = File.OpenWrite(pathToSave))
        {
            data.SaveTo(stream);
        }

        return fileName;
    }
}    
}