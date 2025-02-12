using Enhance.Utils;

namespace Enhance;

internal abstract class Program
{
    public static async Task Main(string[] args)
    {
        var inputData = InputInfo.Get();

        await SimulateEnhancement.RunSimulation(
            inputData.StartLevel,
            inputData.EndLevel,
            inputData.SimulationsCount,
            inputData.HammerLevels
        );
    }
}