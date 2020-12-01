using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private Text simulationTime = null;
    [SerializeField] private Text currentGen = null;
    [SerializeField] private Text currentRoad1 = null;
    [SerializeField] private Text currentRoad2 = null;
    [SerializeField] private Text currentRoad3 = null;
    [SerializeField] private Text currentGenes1 = null;
    [SerializeField] private Text currentGenes2 = null;
    [SerializeField] private Text currentGenes3 = null;
    [SerializeField] private Text currentTotalFitness = null;
    [SerializeField] private Text prevRoad1 = null;
    [SerializeField] private Text prevRoad2 = null;
    [SerializeField] private Text prevRoad3 = null;
    [SerializeField] private Text prevTotalFitness = null;
    [SerializeField] private Text prevGenes1 = null;
    [SerializeField] private Text prevGenes2 = null;
    [SerializeField] private Text prevGenes3 = null;

    void Start() {
        currentRoad1.text = prevRoad1.text = "Road 1: N/A";
        currentRoad2.text = prevRoad2.text = "Road 2: N/A";
        currentRoad3.text = prevRoad3.text = "Road 3: N/A";
        currentGenes1.text = currentGenes2.text = currentGenes3.text =
            prevGenes1.text = prevGenes2.text = prevGenes3.text =
            "Genes:\n  Accele: , Fuel: ,\n  Size: , Weight: ";
        currentTotalFitness.text = prevTotalFitness.text = "Overall: N/A";
        simulationTime.text = "Time: 0 / 30 seconds";
        currentGen.text = "Generation: 1";
    }

    public void UpdateTimes(List<CarAgent> road1Agents, List<CarAgent> road2Agents, List<CarAgent> road3Agents) {
        prevRoad1.text = currentRoad1.text;
        prevRoad2.text = currentRoad2.text;
        prevRoad3.text = currentRoad3.text;

        prevGenes1.text = currentGenes1.text;
        prevGenes2.text = currentGenes2.text;
        prevGenes3.text = currentGenes3.text;
    
        currentRoad1.text = "Road 1: " + RoundToDigits(road1Agents[0].time, 2) + " seconds";
        currentRoad2.text = "Road 2: " + RoundToDigits(road2Agents[0].time, 2) + " seconds";
        currentRoad3.text = "Road 3: " + RoundToDigits(road3Agents[0].time, 2) + " seconds";

        currentGenes1.text = "Genes:\n  Accele: " + RoundToDigits(road1Agents[0].genes.GetGeneValueOfGeneType(GeneType.Acceleration), 2)
                                       + ", Fuel: " + RoundToDigits(road1Agents[0].genes.GetGeneValueOfGeneType(GeneType.FuelCapacity), 2)
                                       + ",\n  Size: " + RoundToDigits(road1Agents[0].genes.GetGeneValueOfGeneType(GeneType.Size), 1)
                                       + ", Weight: " + RoundToDigits(road1Agents[0].genes.GetGeneValueOfGeneType(GeneType.Weight), 2);

        currentGenes2.text = "Genes:\n  Accele: " + RoundToDigits(road2Agents[0].genes.GetGeneValueOfGeneType(GeneType.Acceleration), 2)
                                       + ", Fuel: " + RoundToDigits(road2Agents[0].genes.GetGeneValueOfGeneType(GeneType.FuelCapacity), 2)
                                       + ",\n  Size: " + RoundToDigits(road2Agents[0].genes.GetGeneValueOfGeneType(GeneType.Size), 1)
                                       + ", Weight: " + RoundToDigits(road2Agents[0].genes.GetGeneValueOfGeneType(GeneType.Weight), 2);

        currentGenes3.text = "Genes:\n  Accele: " + RoundToDigits(road3Agents[0].genes.GetGeneValueOfGeneType(GeneType.Acceleration), 2)
                                       + ", Fuel: " + RoundToDigits(road3Agents[0].genes.GetGeneValueOfGeneType(GeneType.FuelCapacity), 2)
                                       + ",\n  Size: " + RoundToDigits(road3Agents[0].genes.GetGeneValueOfGeneType(GeneType.Size), 1)
                                       + ", Weight: " + RoundToDigits(road3Agents[0].genes.GetGeneValueOfGeneType(GeneType.Weight), 2);
    }

    public void UpdateTotalTime(CarAgent road1Agents) {
        currentTotalFitness.text = "Overall: " + (float)Math.Round((decimal)road1Agents.totalTimeAcrossTracks, 2) + " seconds";
    }

    public void UpdateGenerationCount(int generation) => currentGen.text = "Generation: " + generation;

    public void UpdateSimulationTime(float time, float timePerSimulation) => simulationTime.text = "Time: " + (int)time + " / " + (int)timePerSimulation + " seconds";

    float RoundToDigits(float value, int digits) {
        return (float)Math.Round((decimal)value, digits);
    }
}
