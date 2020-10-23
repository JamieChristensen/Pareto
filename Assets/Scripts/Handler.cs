using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GeneType { Food, Water, Wealth }

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
        }
    }
}