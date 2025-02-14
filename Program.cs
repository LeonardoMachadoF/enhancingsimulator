using Enhance.Utils;

namespace Enhance;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var inputData = InputInfo.Get();

        SimulateEnhancement.RunSimulation(
            inputData.StartLevel,
            inputData.EndLevel,
            inputData.SimulationsCount,
            inputData.HammerLevels
        );
    }
}