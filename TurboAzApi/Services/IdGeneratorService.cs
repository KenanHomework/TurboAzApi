using System.Text;

namespace TurboAzApi.Services;

public static class IdGeneratorService
{
    public static int GenerateUniqueNumericId()
    {
        Random random = new Random();
        int uniqueId = random.Next(10000000, 99999999);
        return uniqueId;
    }
}
