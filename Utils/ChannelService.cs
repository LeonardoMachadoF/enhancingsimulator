namespace Enhance.Utils;

public static class ChannelService
{
    public static async Task ProcessResults(IAsyncEnumerable<int[]> results, double totalExecutionTime,
        int totalFluorite, int totalBlessedScroll, int totalCrystal)
    {
        var fluoriteResults = new List<int>();
        var blessedScrollResults = new List<int>();
        var crystalResults = new List<int>();
        var simulationsCompleted = 0;

        await foreach (var result in results)
        {
            totalFluorite += result[0];
            totalBlessedScroll += result[1];
            totalCrystal += result[2];

            fluoriteResults.Add(result[0]);
            blessedScrollResults.Add(result[1]);
            crystalResults.Add(result[2]);

            simulationsCompleted++;
            Console.WriteLine(
                $"[Simulação {simulationsCompleted}] Finalizada! Recursos usados: Fluorite={result[0]}, BlessedScroll={result[1]}, Crystal={result[2]}");
            Console.WriteLine("------------------------------------------------------");
        }

        var averageFluorite = Calculate.Average(totalFluorite, simulationsCompleted);
        var averageBlessedScroll = Calculate.Average(totalBlessedScroll, simulationsCompleted);
        var averageCrystal = Calculate.Average(totalCrystal, simulationsCompleted);

        var medianFluorite = Calculate.Median(fluoriteResults);
        var medianBlessedScroll = Calculate.Median(blessedScrollResults);
        var medianCrystal = Calculate.Median(crystalResults);

        Console.WriteLine($"Média de recursos consumidos após {simulationsCompleted} simulações:");
        Console.WriteLine($"Fluorite: {averageFluorite:F2} (Mediana: {medianFluorite})");
        Console.WriteLine($"BlessedScroll: {averageBlessedScroll:F2} (Mediana: {medianBlessedScroll})");
        Console.WriteLine($"Crystal: {averageCrystal:F2} (Mediana: {medianCrystal})");

        Console.WriteLine($"Tempo total de execução: {totalExecutionTime:F2} segundos");
    }
}