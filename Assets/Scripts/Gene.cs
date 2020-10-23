public class Gene {
    public GeneType type { get; internal set; }
    public float value { get; internal set; }

    public Gene(GeneType type, float value) {
        this.type = type;
        this.value = value;
    }

    public void SetValue(float value) => this.value = value;
}