public class Agent {
    public Chromosome chromosome { get; internal set; }
    public string name { get; internal set; }

    public Agent(Chromosome chromosome, string name) {
        this.chromosome = chromosome;
        this.name = name;
 
    }

    public Agent(Chromosome parent1, Chromosome parent2) {
        chromosome = new Chromosome();
        for (int i = 0; i < parent1.genes.Count; i++) {
            int rng = UnityEngine.Random.Range(0, 2);
            if (rng == 0) {
                //chromosome.AddGene(parent1.genes[i]);
            } else {
                //chromosome.AddGene(parent2.genes[i]);
            }
        }        
    }

    public float CalculateFitness() {
        float fitness = 0;
        foreach (Gene g in chromosome.genes) {
            //fitness += g.value;
        }
        return fitness;
    }
}