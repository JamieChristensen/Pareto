using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParetoFrontierSO", menuName = "Pareto")]
public class ParetoFrontierSO : ScriptableObject
{
    public List<Chromosome> paretoFrontier = new List<Chromosome>();



    public void UpdateFrontier(List<Chromosome> newGenes)
    {
        List<Chromosome> dominantNewGenes = new List<Chromosome>();
        foreach (Chromosome gene in newGenes)
        {
            bool isGeneDominant = true;
            foreach (Chromosome otherGene in newGenes)
            {
                if (!isGeneDominant)
                {
                    continue;
                }
                if (gene != otherGene)
                {
                    //compare to other tracktimes - if there is another gene that is dominant on all 3 tracks, flag isGeneDominant as false;
                    bool bestTrack1 = gene.bestTrack1 < otherGene.bestTrack1;
                    bool bestTrack2 = gene.bestTrack2 < otherGene.bestTrack2;
                    bool bestTrack3 = gene.bestTrack3 < otherGene.bestTrack3;

                    if (!(bestTrack1 && bestTrack2 && bestTrack3))
                    {
                        isGeneDominant = false;
                    }
                }
            }
            if (!isGeneDominant)
            {
                continue;
            }
            //If gene is dominant:
            dominantNewGenes.Add(gene);
        }

        paretoFrontier.AddRange(dominantNewGenes);
        List<Chromosome> newParetoFront = new List<Chromosome>();

        foreach (Chromosome paretoGene in paretoFrontier)
        {
            bool isGeneDominant = true;
            foreach (Chromosome gene in paretoFrontier)
            {
                if (gene != paretoGene)
                {
                    bool bestTrack1 = paretoGene.bestTrack1 < gene.bestTrack1;
                    bool bestTrack2 = paretoGene.bestTrack2 < gene.bestTrack2;
                    bool bestTrack3 = paretoGene.bestTrack3 < gene.bestTrack3;
                    if (!(bestTrack1 && bestTrack2 && bestTrack3))
                    {
                        isGeneDominant = false;
                    }
                }
            }
            if (!isGeneDominant)
            {
                continue;
            }
            //If gene is dominant:
            newParetoFront.Add(paretoGene);
        }

        paretoFrontier = newParetoFront;

    }
}
