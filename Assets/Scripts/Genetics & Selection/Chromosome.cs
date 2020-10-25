using System.Collections.Generic;
using UnityEngine;

public class Chromosome
{
    public List<Gene> genes { get; internal set; } = new List<Gene>();

    public void AddGene(GeneType type) => genes.Add(new Gene(type, Random.Range(0f, 1f)));

    public void AddGene(Gene gene) => genes.Add(gene);

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
        return GetGeneOfType(type).value;
    }
}