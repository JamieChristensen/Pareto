public class Agent {
    private Chromosome chromosome;
    public string name { get; internal set; }

    public Agent(Chromosome chromosome, string name) {
        this.chromosome = chromosome;
        this.name = name;
    }

    public float CalculateFitness() {
        float fitness = 0;
        foreach (Gene g in chromosome.genes) {
            fitness += g.value;
        }
        return fitness;
    }
}