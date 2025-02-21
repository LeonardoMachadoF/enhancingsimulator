namespace Enhance.Utils;

public static class ChannelService
{
    public static void ProcessResults((int Fluorite, int BlessedScroll, int Crystal)[] results,
        double totalExecutionTime, int spentMoreThan = 2620)
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

        var spentMoreFluoriteThan =
            results.Count(r => r.Fluorite > spentMoreThan);

        Console.WriteLine($"Média de recursos consumidos após {simulationsCompleted} simulações:");
        Console.WriteLine($"Fluorite: {averageFluorite:F2} (Mediana: {medianFluorite})");
        Console.WriteLine($"BlessedScroll: {averageBlessedScroll:F2} (Mediana: {medianBlessedScroll})");
        Console.WriteLine($"Crystal: {averageCrystal:F2} (Mediana: {medianCrystal})");
        Console.WriteLine($"{spentMoreFluoriteThan} gastaram mais que {spentMoreThan} fluorites.");
        Console.WriteLine($"Tempo total de execução: {totalExecutionTime:F2} segundos");
    }
}