using System.Collections.Generic;
using UnityEngine;

public class Chromosome
{
    public List<Gene> genes { get; internal set; } = new List<Gene>();

    public Chromosome() {
        genes.Add(new Gene(GeneType.Acceleration, Random.Range(0f, 1f)));
        genes.Add(new Gene(GeneType.FuelCapacity, Random.Range(0f, 1f)));
        genes.Add(new Gene(GeneType.Size, Random.Range(0f, 1f)));
        genes.Add(new Gene(GeneType.Weight, Random.Range(0f, 1f)));
    }

    public Chromosome(Chromosome parent1, Chromosome parent2) {
        for (int i = 0; i < parent1.genes.Count; i++) {
            int rng = Random.Range(0, 2);
            if (rng == 0) {
                genes.Add(parent1.genes[i]);
            } else {
                genes.Add(parent2.genes[i]);
            }
        }
    }

    public void SetGeneValue(GeneType type, float value) => GetGeneOfType(type).SetValue(value);

    public void Mutate()
    {
        int rng = Random.Range(0, genes.Count);
        genes[rng].SetValue(Random.Range(0f, 1f));
    }

    public Gene GetGeneOfType(GeneType type)
    {
        //Works under the assumption that there is one and only one of each genetype in the chromosome.
        foreach (Gene gene in genes)
        {
            if (gene.type == type)
            {
                return gene;
            }
        }
        
        throw new System.ArgumentNullException("No gene of corresponding type. Make better chromosomes you downie");
    }

    public float GetGeneValueOfGeneType(GeneType type)
    {
        return GetGeneOfType(type).GetValue();
    }
}