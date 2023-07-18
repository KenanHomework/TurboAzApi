namespace TurboAzApi.Configurations;

public class AzureConfig
{
    public string ConnectionString { get; set; } = string.Empty;

    public string FullSizeContainerName { get; set; } = string.Empty;

    public string ThumbnailContainerName { get; set; } = string.Empty;
}
