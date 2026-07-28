using Xunit;
using System.Linq;
using PulseCache.Server.Storage;

namespace PulseCache.Tests;

public class StorageEngineTests
{
    [Fact]
    public async Task StorageEngine_SetAndGet_ShouldStoreAndRetrieveValue()
    {
        var storageEngine = new StorageEngine();

        var tasks = Enumerable.Range(0, 100)
            .Select(i =>
                Task.Run(() =>
                {
                    storageEngine.AddEntry(
                        $"key-{i}",
                        new CacheEntry()
                    );
                })
            );

        await Task.WhenAll(tasks);

        for (int i = 0; i < 100; i++)
        {
            var result = storageEngine.GetEntry($"key-{i}");

            Assert.NotNull(result);
        }
    }
}