using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chromosome
{
    public int id = -1;
    public List<Gene> genes { get; internal set; } = new List<Gene>();
    public float bestTrack1 = 30;
    public float bestTrack2 = 30;
    public float bestTrack3 = 30;
    public List<float> timesTrack1 = new List<float>();
    public List<float> timesTrack2 = new List<float>();
    public List<float> timesTrack3 = new List<float>();

    public Chromosome() {
        genes.Add(new Gene(GeneType.Acceleration, Random.Range(0f, 1f)));
        genes.Add(new Gene(GeneType.FuelCapacity, Random.Range(0f, 1f)));
        genes.Add(new Gene(GeneType.Size, 0));
        genes.Add(new Gene(GeneType.Weight, Random.Range(0f, 1f)));
    }

    public void NewTime(float time, int track) {
        switch (track) {
            case 1:
                if (time < bestTrack1) {
                    bestTrack1 = time;
                }
                timesTrack1.Add(time);
                break;
            case 2:
                if (time < bestTrack2) {
                    bestTrack2 = time;
                }
                timesTrack2.Add(time);
                break;
            case 3:
                if (time < bestTrack3) {
                    bestTrack3 = time;
                }
                timesTrack3.Add(time);
                break;
            default:
                throw new System.ArgumentNullException("Track not inputted correctly");
        }
    }

    public void SetGeneSequence(List<Gene> _genes) => genes = _genes;

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