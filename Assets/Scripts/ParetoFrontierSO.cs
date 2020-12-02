using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ParetoFrontierSO", menuName = "Pareto")]
public class ParetoFrontierSO : ScriptableObject
{
    public List<Chromosome> paretoFrontier = new List<Chromosome>();




    public void UpdateFrontier(List<Chromosome> newGenes)
    {
        List<Chromosome> unfilteredFrontier = new List<Chromosome>();
        List<Chromosome> updatedFrontier = new List<Chromosome>();

        List<Chromosome> dupeList = new List<Chromosome>(); 
        dupeList.AddRange(newGenes);
        dupeList.AddRange(paretoFrontier);
        unfilteredFrontier = dupeList.Distinct().ToList();

        foreach (Chromosome chrom in unfilteredFrontier)
        {
            bool isDominated = false;

            foreach (Chromosome chrom2 in unfilteredFrontier)
            {
                if (chrom == chrom2 || chrom.id == chrom2.id) continue;
                if (isDominated) break;

                bool isDominantOnTrack1 = chrom.bestTrack1 < chrom2.bestTrack1;
                bool isDominantOnTrack2 = chrom.bestTrack2 < chrom2.bestTrack2;
                bool isDominantOnTrack3 = chrom.bestTrack3 < chrom2.bestTrack3;

                bool isDominantOnAnyTrack = isDominantOnTrack1 || isDominantOnTrack2 || isDominantOnTrack3;

                isDominated = !isDominantOnAnyTrack;
            }

            if (isDominated)
            {
                continue;
            }
            else
            {
                updatedFrontier.Add(chrom);
            }
        }

        paretoFrontier = new List<Chromosome>(updatedFrontier);
    }


    public void UpdateFrontierOriginal(List<Chromosome> newGenes)
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
                    bool isDominatedOnTrack1 = gene.bestTrack1 < otherGene.bestTrack1;
                    bool isDominatedOnTrack2 = gene.bestTrack2 < otherGene.bestTrack2;
                    bool isDominatedOnTrack3 = gene.bestTrack3 < otherGene.bestTrack3;

                    if ((isDominatedOnTrack1 && isDominatedOnTrack2 && isDominatedOnTrack3))
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
            Debug.Log("List tostring: " + dominantNewGenes.ToString() + "\n length: " + dominantNewGenes.Count);
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
                    bool isDominatedOnTrack1 = paretoGene.bestTrack1 < gene.bestTrack1;
                    bool isDominatedOnTrack2 = paretoGene.bestTrack2 < gene.bestTrack2;
                    bool isDominatedOnTrack3 = paretoGene.bestTrack3 < gene.bestTrack3;
                    if ((isDominatedOnTrack1 && isDominatedOnTrack2 && isDominatedOnTrack3))
                    {
                        isGeneDominant = false;
                    }
                    if (paretoGene.bestTrack1 == 30) isGeneDominant = false;
                    if (paretoGene.bestTrack2 == 30) isGeneDominant = false;
                    if (paretoGene.bestTrack3 == 30) isGeneDominant = false;

                }
            }
            if (!isGeneDominant)
            {
                continue;
            }
            //If gene is dominant:
            newParetoFront.Add(paretoGene);
            Debug.Log("List tostring: " + newParetoFront.ToString() + "\n length: " + newParetoFront.Count);
        }

        paretoFrontier = newParetoFront;

    }
}
