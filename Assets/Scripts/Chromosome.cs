using System.Collections.Generic;

public class Chromosome {
    public List<Gene> genes { get; internal set; } = new List<Gene>();

    public Chromosome() {
        AddGene(GeneType.Food);
        AddGene(GeneType.Water);
        AddGene(GeneType.Wealth);
    }

    void AddGene(GeneType type) => genes.Add(new Gene(type, UnityEngine.Random.Range(0f, 1f)));
}