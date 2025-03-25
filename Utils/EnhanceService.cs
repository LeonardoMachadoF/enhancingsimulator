using System.Text;

namespace Enhance.Utils;

public static class EnhanceService
{
    public static void ExportInfo((int Fluorite, int BlessedScroll, int Crystal)[] results, int tryCount,
        double totalExecutionTime)
    {
        var totalFluorite = results.Sum(r => r.Fluorite);
        var totalBlessedScroll = results.Sum(r => r.BlessedScroll);
        var totalCrystal = results.Sum(r => r.Crystal);

        var simulationsCompleted = results.Length;

        var averageFluorite = Calculate.Average(totalFluorite, simulationsCompleted);
        var averageBlessedScroll = Calculate.Average(totalBlessedScroll, simulationsCompleted);
        var averageCrystal = Calculate.Average(totalCrystal, simulationsCompleted);

        var medianFluorite = Calculate.Median(results.Select(r => r.Fluorite).ToList());
        var medianBlessedScroll = Calculate.Median(results.Select(r => r.BlessedScroll).ToList());
        var medianCrystal = Calculate.Median(results.Select(r => r.Crystal).ToList());

        var spentMoreFluoriteThan = (double)results.Count(r => r.Fluorite > tryCount) / simulationsCompleted * 100;

        var sortedFluorite = results.Select(r => r.Fluorite).OrderBy(f => f).ToList();
        var percentiles = new Dictionary<int, double>();
        for (var i = 10; i <= 100; i += 1)
            percentiles[i] =
                sortedFluorite[Math.Min((int)(i / 100.0 * simulationsCompleted), simulationsCompleted - 1)];


        Console.WriteLine($"Média de recursos consumidos após {simulationsCompleted} simulações:");
        Console.WriteLine($"Fluorite: {averageFluorite:F2} (Mediana: {medianFluorite})");
        Console.WriteLine($"BlessedScroll: {averageBlessedScroll:F2} (Mediana: {medianBlessedScroll})");
        Console.WriteLine($"Crystal: {averageCrystal:F2} (Mediana: {medianCrystal})");
        Console.WriteLine($"{spentMoreFluoriteThan.ToString("F2")}% gastaram mais que {tryCount} fluorites.");
        var percentilesString = new StringBuilder();
        foreach (var percentile in percentiles)
            percentilesString.Append($"{percentile.Key}%: {percentile.Value}f | ");
        Console.WriteLine(percentilesString.ToString());
        Console.WriteLine($"Tempo total de execução: {totalExecutionTime:F2} segundos");
    }
}