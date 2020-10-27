using MathUtility;
public class Gene
{
    public GeneType type { get; internal set; }
    private float value;

    public Gene(GeneType type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public float GetValue()
    {
        switch (type)
        {
            case GeneType.Undefined:
                throw new System.ArgumentException("Define the gene type");

            case GeneType.Acceleration:
                return MathUtils.map(value, 0, 1, 1, 5000f);

            case GeneType.FuelCapacity:
                return MathUtils.map(value, 0, 1, 1, 120f);

            case GeneType.Size:
                return MathUtils.map(value, 0, 1, 0.8f, 3);

            case GeneType.Weight:
                return MathUtils.map(value, 0, 1, 0, 800f);
        }

        throw new System.ArgumentException("You somehow got wrong args");
    }

    public void SetValue(float value) => this.value = value;
}

public enum GeneType
{
    Undefined,
    Acceleration,
    FuelCapacity,
    Size,
    Weight
}

namespace MathUtility
{
    public class MathUtils
    {
        static public float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }
    }
}
