using Enhance.Enums;

namespace Enhance;

public static class DefaultEnhanceChance
{
    public static Dictionary<EnhancePossibleResults, double> GetChances(ToEnhanceOptions option, bool hammer)
    {
        var enhancementRates = new Dictionary<ToEnhanceOptions, Dictionary<EnhancePossibleResults, double>>
        {
            {
                ToEnhanceOptions.ToNine,
                new Dictionary<EnhancePossibleResults, double>
                {
                    { EnhancePossibleResults.Success, hammer ? 4 : 2 },
                    { EnhancePossibleResults.Destroy, 20.0 },
                    { EnhancePossibleResults.NoChange, 10.0 },
                    { EnhancePossibleResults.Downgrade, 0 }
                }
            },
            {
                ToEnhanceOptions.ToTen,
                new Dictionary<EnhancePossibleResults, double>
                {
                    { EnhancePossibleResults.Success, hammer ? 2 : 1 },
                    { EnhancePossibleResults.Destroy, 25.0 },
                    { EnhancePossibleResults.NoChange, 7.0 },
                    { EnhancePossibleResults.Downgrade, 0 }
                }
            },
            {
                ToEnhanceOptions.ToEleven,
                new Dictionary<EnhancePossibleResults, double>
                {
                    { EnhancePossibleResults.Success, hammer ? 1.47 : 0.735 },
                    { EnhancePossibleResults.Destroy, 35.3 },
                    { EnhancePossibleResults.NoChange, 7.0 },
                    { EnhancePossibleResults.Downgrade, 0 }
                }
            },
            {
                ToEnhanceOptions.ToTwelve,
                new Dictionary<EnhancePossibleResults, double>
                {
                    { EnhancePossibleResults.Success, hammer ? 2 : 1.0 },
                    { EnhancePossibleResults.Destroy, 25.0 },
                    { EnhancePossibleResults.NoChange, 7.0 },
                    { EnhancePossibleResults.Downgrade, 27.0 }
                }
            },
            {
                ToEnhanceOptions.ToThirteen,
                new Dictionary<EnhancePossibleResults, double>
                {
                    { EnhancePossibleResults.Success, hammer ? 2 : 1.0 },
                    { EnhancePossibleResults.Destroy, 29.0 },
                    { EnhancePossibleResults.NoChange, 3.0 },
                    { EnhancePossibleResults.Downgrade, 27.0 }
                }
            }
        };

        return enhancementRates[option];
    }

    public static Dictionary<ToEnhanceOptions, int> GetPity()
    {
        return new Dictionary<ToEnhanceOptions, int>
        {
            { ToEnhanceOptions.ToNine, 90 },
            { ToEnhanceOptions.ToTen, 180 },
            { ToEnhanceOptions.ToEleven, 270 }
        };
    }
}