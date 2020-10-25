using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Handler : MonoBehaviour {
    [SerializeField] private int populationSize = 10;
    private List<Agent> agents = new List<Agent>();
    private float previousOverallFitness = -1;
    
    void Start() {
        for (int i = 0; i < populationSize; i++) {
            agents.Add(new Agent(new Chromosome(), "Agent " + (i + 1)));
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            float overallFitness = 0;
            foreach (Agent a in agents) {
                overallFitness += a.CalculateFitness();
            }
            Debug.Log(previousOverallFitness);
            Debug.Log(overallFitness);
            previousOverallFitness = overallFitness;
            BreedNewPopulation();
        }
    }

    void BreedNewPopulation() {
        List<Agent> sortedAgents = SortAgentsUsingFitnessFunction();
        List<Agent> newAgents = new List<Agent>();
        for (int i = 0; i < sortedAgents.Count; i += 2) {
            newAgents.Add(new Agent(sortedAgents[i].chromosome, sortedAgents[i + 1].chromosome));
            newAgents.Add(new Agent(sortedAgents[i].chromosome, sortedAgents[i + 1].chromosome));
        }
        agents.Clear();
        agents = new List<Agent>(newAgents);
        Debug.Log(agents.Count);
    }

    List<Agent> SortAgentsUsingFitnessFunction() {
        List<Agent> sortedList = new List<Agent>();
        sortedList = agents;
        return sortedList;
    }
}