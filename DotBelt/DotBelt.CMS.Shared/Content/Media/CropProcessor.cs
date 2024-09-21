using System.Text;
using SkiaSharp;
using Path = System.IO.Path;

namespace DotBelt.CMS.Shared.CMS.Media;

public enum CropPositionX
{
    Left,
    Center,
    Right
}

public enum CropPositionY
{
    Top,
    Center,
    Bottom
}

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

    public static string CropImage(
        Stream imageStream,
        string fileName,
        int width,
        int? height = null,
        bool softCrop = true,
        CropPositionX cropX = CropPositionX.Center,
        CropPositionY cropY = CropPositionY.Center)
    {
        fileName = GetCropFileName(width, height, fileName);

        using (var input = SKBitmap.Decode(imageStream))
        {
            int finalWidth = width > 0 ? width : input.Width;
            int finalHeight = height.HasValue && height > 0
                ? height.Value
                : (int)(input.Height * ((float)finalWidth / input.Width));

            SKBitmap outputBitmap;

            if (softCrop)
            {
                // SOFT CROP: Scale the image to fit within the specified dimensions
                outputBitmap = new SKBitmap(finalWidth, finalHeight);

                using (var canvas = new SKCanvas(outputBitmap))
                {
                    canvas.Clear(SKColors.Transparent); // Optional: Clear with white or another background color

                    float scale = Math.Min((float)finalWidth / input.Width, (float)finalHeight / input.Height);
                    int newWidth = (int)(input.Width * scale);
                    int newHeight = (int)(input.Height * scale);

                    int offsetX = (finalWidth - newWidth) / 2;
                    int offsetY = (finalHeight - newHeight) / 2;

                    var destRect = new SKRect(offsetX, offsetY, offsetX + newWidth, offsetY + newHeight);
                    canvas.DrawBitmap(input, new SKRect(0, 0, input.Width, input.Height), destRect,
                        new SKPaint { FilterQuality = SKFilterQuality.High });
                }
            }
            else
            {
                // HARD CROP: Resize and crop the image to fill the specified dimensions
                float widthRatio = (float)finalWidth / input.Width;
                float heightRatio = (float)finalHeight / input.Height;
                float scale = Math.Max(widthRatio, heightRatio);

                int scaledWidth = (int)(input.Width * scale);
                int scaledHeight = (int)(input.Height * scale);

                using (var resizedBitmap =
                       input.Resize(new SKImageInfo(scaledWidth, scaledHeight), SKFilterQuality.High))
                {
                    outputBitmap = new SKBitmap(finalWidth, finalHeight);

                    int offsetX = 0, offsetY = 0;

                    switch (cropX)
                    {
                        case CropPositionX.Left:
                            offsetX = 0;
                            break;
                        case CropPositionX.Center:
                            offsetX = (scaledWidth - finalWidth) / 2;
                            break;
                        case CropPositionX.Right:
                            offsetX = scaledWidth - finalWidth;
                            break;
                    }

                    switch (cropY)
                    {
                        case CropPositionY.Top:
                            offsetY = 0;
                            break;
                        case CropPositionY.Center:
                            offsetY = (scaledHeight - finalHeight) / 2;
                            break;
                        case CropPositionY.Bottom:
                            offsetY = scaledHeight - finalHeight;
                            break;
                    }

                    using (var canvas = new SKCanvas(outputBitmap))
                    {
                        var sourceRect = new SKRect(offsetX, offsetY, offsetX + finalWidth, offsetY + finalHeight);
                        var destRect = new SKRect(0, 0, finalWidth, finalHeight);

                        canvas.Clear(SKColors.Transparent); // Optional: Clear with white or another background color
                        canvas.DrawBitmap(resizedBitmap, sourceRect, destRect,
                            new SKPaint { FilterQuality = SKFilterQuality.High });
                    }
                }
            }

            var pathToSave = UploadsController.GetFullPath(fileName);

            using (var image = SKImage.FromBitmap(outputBitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Webp, 100))
            using (var stream = File.OpenWrite(pathToSave))
            {
                data.SaveTo(stream);
            }

            return fileName;
        }
    }
}