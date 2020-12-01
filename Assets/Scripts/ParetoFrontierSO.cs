using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParetoFrontierSO", menuName = "Pareto")]
public class ParetoFrontierSO : ScriptableObject
{
    public List<Chromosome> ParetoFrontier;

    public void UpdateFrontier(List<Chromosome> newGenes)
    {
        List<Chromosome> dominantNewGenes = new List<Chromosome>();
        foreach (Chromosome gene in newGenes)
        {
            bool isGeneDominant = true;
            foreach (Chromosome otherGene in newGenes)
            {
                if (gene != otherGene)
                {
                    //compare to other tracktimes - if there is another gene that is dominant on all 3 tracks, flag isGeneDominant as false;

                }
            }
        }


        foreach (Chromosome paretoGene in ParetoFrontier)
        {

            foreach (Chromosome gene in newGenes)
            {
                bool isGeneDominated = false;
                if (gene != paretoGene)
                {
                    //compare tracktime of track1, track2, track3 for each gene - the gene is pareto-dominated if there is a gene
                    //that is better than it on all tracks, if there isn't, it is pareto-optimal.
                    // if it is pareto-optimal, add it to 
                }
            }


        }


    }
}
