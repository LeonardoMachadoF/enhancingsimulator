using Enhance.Enums;

namespace Enhance.Utils;

public class InputInfo
{
    public static InputInfoResponse Get()
    {
        Console.WriteLine("Refino inicial (9, 10, 11, 12): ");
        var startLevel = (EnhanceOptions)Enum.Parse(typeof(EnhanceOptions), Console.ReadLine());

        Console.WriteLine("Refino final (10, 11, 12, 13): ");
        var endLevel = (ToEnhanceOptions)Enum.Parse(typeof(ToEnhanceOptions), Console.ReadLine());

        Console.WriteLine("Numero de simulações: ");
        var simulationsCount = int.Parse(Console.ReadLine());

        Console.WriteLine(
            "Para quais leveis deve ser utilizado martelo (ex: 10,11 (separado por virgula)): ");
        var hammerLevelsInput = Console.ReadLine();
        var hammerLevels = string.IsNullOrEmpty(hammerLevelsInput)
            ? new List<ToEnhanceOptions>()
            : hammerLevelsInput
                .Split(',')
                .Select(level => level.Trim())
                .Where(level => Enum.TryParse<ToEnhanceOptions>(level, out _))
                .Select(level => (ToEnhanceOptions)Enum.Parse(typeof(ToEnhanceOptions), level))
                .ToList();

        return new InputInfoResponse
        {
            StartLevel = startLevel,
            EndLevel = endLevel,
            SimulationsCount = simulationsCount,
            HammerLevels = hammerLevels
        };
    }
}

public record InputInfoResponse
{
    public EnhanceOptions StartLevel { get; set; }
    public ToEnhanceOptions EndLevel { get; set; }
    public int SimulationsCount { get; set; }
    public List<ToEnhanceOptions> HammerLevels { get; set; }
}