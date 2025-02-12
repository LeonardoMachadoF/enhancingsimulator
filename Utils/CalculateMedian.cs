namespace Enhance.Utils;

public static class Calculate
{
    public static double Median(List<int> values)
    {
        var sortedValues = values.OrderBy(v => v).ToList();
        var count = sortedValues.Count;
        if (count % 2 == 0)
            return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2.0;

        return sortedValues[count / 2];
    }

    public static double Average(double fluorites, double simulations)
    {
        return fluorites / simulations;
    }
}