using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using TurboAzApi.Configurations;
using TurboAzApi.DTOs;
using TurboAzApi.Interface;

namespace TurboAzApi.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly AzureConfig _azureConfig;

    private readonly BlobServiceClient _blobServiceClient;

    private readonly ImageManipulationService _imageManipulationService;

    public AzureBlobStorageService(AzureConfig azureConfig)
    {
        _azureConfig = azureConfig;
        _blobServiceClient = new(_azureConfig.ConnectionString);
        _imageManipulationService = new ImageManipulationService();
    }

    public async Task<List<ImageResponse>> UploadImagesAsync(
        List<IFormFile> images,
        DateTime dateTime
    )
    {
        List<ImageResponse> imageResponses = new();

        return imageResponses;
    }

    public async Task<List<ImageResponse>> UploadImagesAsync(List<string> images, DateTime dateTime)
    {
        List<ImageResponse> imageResponses = new();

        // Full Size Images Container
        BlobContainerClient fullSizeContainerClient = _blobServiceClient.GetBlobContainerClient(
            _azureConfig.FullSizeContainerName
        );

        // Thumbnail Images Container
        BlobContainerClient thumbnailContainerClient = _blobServiceClient.GetBlobContainerClient(
            _azureConfig.ThumbnailContainerName
        );

        try
        {
            foreach (var image in images)
            {
                string imageType = GetImageTypeFromBase64(image);

                string blobName =
                    $"{dateTime.ToString("yyyy/MM/dd/HH/mm/ss").Replace('/', '_')}{Guid.NewGuid()}.{imageType}";

                BinaryData binaryData = new(RemovePrefixFromBase64(image));

                BlobClient fullSizeBlobClient = fullSizeContainerClient.GetBlobClient(blobName);
                await fullSizeBlobClient.UploadAsync(binaryData);
                string fullSizeImageUrl = fullSizeBlobClient.Uri.ToString();

                BlobClient thumbnailBlobClient = thumbnailContainerClient.GetBlobClient(blobName);

                // Generating Thumbnail version of current image
                var thumbnailImageByteData = _imageManipulationService.CreateThumbnail(
                    binaryData.ToArray()
                );
                var thumbnailImageData = new BinaryData(thumbnailImageByteData);

                await thumbnailBlobClient.UploadAsync(thumbnailImageData);
                string thumbnailImageUrl = thumbnailBlobClient.Uri.ToString();

                imageResponses.Add(
                    new() { Original = fullSizeImageUrl, Thumbnail = thumbnailImageUrl }
                );
            }
        }
        catch (Exception)
        {
            throw;
        }

        return imageResponses;
    }

    public string GetImageTypeFromBase64(string base64String)
    {
        int prefixEndIndex = base64String.IndexOf(';');

        string prefix = base64String.Substring(0, prefixEndIndex);

        string imageType = prefix.Replace("data:image/", "");

        return imageType;
    }

    public byte[] RemovePrefixFromBase64(string base64String)
    {
        // Öneki içeren kısmın başlangıç indeksini bul
        int prefixEndIndex = base64String.IndexOf(',') + 1;

        // Öneksiz base64 dizesini al
        string base64WithoutPrefix = base64String.Substring(prefixEndIndex);

        // Base64 dizesini byte dizisine dönüştür
        byte[] bytes = Convert.FromBase64String(base64WithoutPrefix);

        return bytes;
    }
}
