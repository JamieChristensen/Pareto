using System.Collections.Generic;
using UnityEngine;

public class Chromosome {
    public List<Gene> genes { get; internal set; } = new List<Gene>();

    public void AddGene(GeneType type) => genes.Add(new Gene(type, Random.Range(0f, 1f)));

    public void AddGene(Gene gene) => genes.Add(gene);

    public void Mutate() {
        int rng = Random.Range(0, genes.Count);
        genes[rng].SetValue(Random.Range(0f, 1f));
    }
}