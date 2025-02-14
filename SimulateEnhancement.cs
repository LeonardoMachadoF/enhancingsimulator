using System.Diagnostics;
using Enhance.Enums;
using Enhance.Utils;

namespace Enhance;

public static class SimulateEnhancement
{
    public static void RunSimulation(EnhanceOptions start, ToEnhanceOptions end, int numberOfSimulations,
        List<ToEnhanceOptions> hammerLevels)
    {
        var stopwatch = Stopwatch.StartNew();
        var pity = DefaultEnhanceChance.GetPity();
        var results = new (int Fluorite, int BlessedScroll, int Crystal)[numberOfSimulations];

        Parallel.For(0, numberOfSimulations, i =>
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

            results[i] = (fluorite, blessedScroll, crystal);
        });

        stopwatch.Stop();
        ChannelService.ProcessResults(results, stopwatch.Elapsed.TotalSeconds);
    }
}