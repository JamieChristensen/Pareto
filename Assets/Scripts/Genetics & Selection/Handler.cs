using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Handler : MonoBehaviour {
    [SerializeField] private Transform spawnPoint1 = null;
    [SerializeField] private Transform spawnPoint2 = null;
    [SerializeField] private Transform spawnPoint3 = null;
    [SerializeField] private int populationSize = 30;
    [SerializeField] private GameObject carAgent = null;

    private List<CarAgent> road1Agents = new List<CarAgent>();
    private List<CarAgent> road2Agents = new List<CarAgent>();
    private List<CarAgent> road3Agents = new List<CarAgent>();

    private bool isRunningSimulation = false;
    [HideInInspector] public float time = 0;
    [SerializeField] private float timePerSimulation = 30;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            BreedNewPopulation();
            isRunningSimulation = true;
        }

        if (isRunningSimulation) {
            time += Time.deltaTime;
        }
    }

    void SimulationDone() {
        List<float> totalTimePerAgent = new List<float>();
        for (int i = 0; i < road1Agents.Count; i++) {
            if (road1Agents[i].time == 0) road1Agents[i].time = 30;
            if (road2Agents[i].time == 0) road2Agents[i].time = 30;
            if (road3Agents[i].time == 0) road3Agents[i].time = 30;
            totalTimePerAgent.Add(road1Agents[i].time + road2Agents[i].time + road3Agents[i].time);
        }
        totalTimePerAgent.OrderByDescending(i => i);
    }

    void BreedNewPopulation() {
        for (int i = 0; i < populationSize; i++) {
            road1Agents.Add(Instantiate(carAgent, spawnPoint1.position + Vector3.forward * 25 * road1Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint1).GetComponent<CarAgent>());
            road1Agents[i].genes = new Chromosome();
            road1Agents[i].handler = this;
            road2Agents.Add(Instantiate(road1Agents[i], spawnPoint2.position + Vector3.forward * 25 * road2Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint2).GetComponent<CarAgent>());
            road2Agents[i].genes = new Chromosome();
            road2Agents[i].handler = this;
            road3Agents.Add(Instantiate(road1Agents[i], spawnPoint3.position + Vector3.forward * 25 * road3Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint3).GetComponent<CarAgent>());
            road3Agents[i].genes = new Chromosome();
            road3Agents[i].handler = this;
        }
    }
}