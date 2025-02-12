using System.Diagnostics;
using System.Threading.Channels;
using Enhance.Enums;
using Enhance.Utils;

namespace Enhance;

public static class SimulateEnhancement
{
    private static int _totalBlessedScroll;
    private static int _totalCrystal;
    private static int _totalFluorite;

    public static async Task RunSimulation(EnhanceOptions start, ToEnhanceOptions end, int numberOfSimulations,
        List<ToEnhanceOptions> hammerLevels)
    {
        _totalFluorite = 0;
        _totalBlessedScroll = 0;
        _totalCrystal = 0;

        var channel = Channel.CreateUnbounded<int[]>();
        var stopwatch = Stopwatch.StartNew();

        var pity = DefaultEnhanceChance.GetPity();

        var tasks = new List<Task>();
        for (var i = 0; i < numberOfSimulations; i++)
            tasks.Add(Task.Run(async () =>
            {
                var currentLevel = start;
                var blessedScroll = 0;
                var crystal = 0;
                var fluorite = 0;
                var failure = 0;

                while ((int)currentLevel < (int)end)
                {
                    var levelToEnhance = (ToEnhanceOptions)currentLevel + 1;
                    var chances = DefaultEnhanceChance.GetChances(levelToEnhance,
                        hammerLevels.Contains(levelToEnhance));
                    var roll = Random.Shared.NextDouble() * 100;

                    if (pity.TryGetValue(levelToEnhance, out var pityLimit) && failure == pityLimit)
                    {
                        currentLevel++;
                        failure = 0;
                        continue;
                    }

                    if (currentLevel >= EnhanceOptions.Eleven)
                        crystal++;
                    else
                        fluorite++;

                    failure++;

                    if (roll <= chances[EnhancePossibleResults.Success])
                        currentLevel++;
                    else if (roll <= chances[EnhancePossibleResults.Success] + chances[EnhancePossibleResults.Destroy])
                        blessedScroll++;
                    else if (chances[EnhancePossibleResults.Downgrade] > 0 &&
                             roll <= chances[EnhancePossibleResults.Success] +
                             chances[EnhancePossibleResults.Destroy] +
                             chances[EnhancePossibleResults.Downgrade])
                        currentLevel = (EnhanceOptions)((int)currentLevel - 1);
                }

                await channel.Writer.WriteAsync(new[] { fluorite, blessedScroll, crystal });
            }));

        await Task.WhenAll(tasks);
        
        channel.Writer.Complete();
        stopwatch.Stop();

        await ChannelService.ProcessResults(channel.Reader.ReadAllAsync(), stopwatch.Elapsed.TotalSeconds,
            _totalFluorite, _totalBlessedScroll, _totalCrystal);
    }
}