namespace TurboAzApi.Services;

public class ImageManipulationService
{
    public byte[] CreateThumbnail(byte[] imageBytes)
    {
        using MemoryStream ms = new MemoryStream(imageBytes);
        Image image = Image.Load(ms);

        Image thumbnail = image.Clone(x => x.Resize(154, 116));

        using MemoryStream thumbnailMs = new();
        thumbnail.Save(thumbnailMs, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
        return thumbnailMs.ToArray();
    }
}
