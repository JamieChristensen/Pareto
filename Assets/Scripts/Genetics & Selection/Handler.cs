using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Handler : MonoBehaviour {
    [SerializeField] private Transform spawnPoint1 = null;
    [SerializeField] private Transform spawnPoint2 = null;
    [SerializeField] private Transform spawnPoint3 = null;
    [SerializeField] private Transform oldAgents = null;
    [SerializeField] private CarAgent[] scaledCarPrefabs = null;
    [SerializeField] private int populationSize = 30;
    [SerializeField] private float timePerSimulation = 30;
    [SerializeField] private UIHandler uiHandler = null;
    [HideInInspector] public float time = 0;
    public List<Chromosome> chromosomes = new List<Chromosome>();

    private bool isRunningSimulation = false;
    private int currentGeneration = 1;
    private List<CarAgent> road1Agents = new List<CarAgent>();
    private List<CarAgent> road2Agents = new List<CarAgent>();
    private List<CarAgent> road3Agents = new List<CarAgent>();

    void Start() {
        //Time.fixedDeltaTime = 0.01f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Time.timeScale = 2;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isRunningSimulation) {
            BreedNewAgents();
            for (int i = 0; i < road1Agents.Count; i++) {
                road1Agents[i].GenotypeToPhenotype(road1Agents[i].genes);
                road2Agents[i].GenotypeToPhenotype(road2Agents[i].genes);
                road3Agents[i].GenotypeToPhenotype(road3Agents[i].genes);
            }
            isRunningSimulation = true;
        }

        if (isRunningSimulation) {
            time += Time.deltaTime;
            uiHandler.UpdateSimulationTime(time, timePerSimulation);

            if (time >= timePerSimulation) {
                isRunningSimulation = false;
                Selection();
            }
        }
    }

    void BreedNewAgents(List<CarAgent> prevAgents = null) {
        CarAgent tempAgent = null;
        CarAgent newCarAgent = null;
        Chromosome ch = null;
        bool isCleanAgent = false;

        for (int i = 0; i < populationSize; i++) {
            if (prevAgents != null && i < prevAgents.Count) {
                foreach (CarAgent agent in scaledCarPrefabs) {
                    if (agent.size == prevAgents[i].size) {
                        newCarAgent = agent;
                        isCleanAgent = false;
                        break;
                    }
                }
            } else {
                newCarAgent = scaledCarPrefabs[UnityEngine.Random.Range(0, scaledCarPrefabs.Length)];
                isCleanAgent = true;
            }

            if (isCleanAgent) {
                ch = new Chromosome();
            } else {
                ch = prevAgents[i].genes;
            }

            tempAgent = Instantiate(newCarAgent, transform).GetComponent<CarAgent>();
            tempAgent.handler = this;

            road1Agents.Add(Instantiate(tempAgent, spawnPoint1.position + Vector3.forward * 25 * road1Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint1).GetComponent<CarAgent>());
            road1Agents[i].genes = ch;
            road1Agents[i].handler = this;
            road1Agents[i].track = 1;
            road1Agents[i].genes.SetGeneValue(GeneType.Size, newCarAgent.size);

            road2Agents.Add(Instantiate(tempAgent, spawnPoint2.position + Vector3.forward * 25 * road2Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint2).GetComponent<CarAgent>());
            road2Agents[i].genes = ch;
            road2Agents[i].handler = this;
            road2Agents[i].track = 2;
            road2Agents[i].genes.SetGeneValue(GeneType.Size, newCarAgent.size);

            road3Agents.Add(Instantiate(tempAgent, spawnPoint3.position + Vector3.forward * 25 * road3Agents.Count, Quaternion.Euler(0, 270, 0), spawnPoint3).GetComponent<CarAgent>());
            road3Agents[i].genes = ch;
            road3Agents[i].handler = this;
            road3Agents[i].track = 3;
            road3Agents[i].genes.SetGeneValue(GeneType.Size, newCarAgent.size);


            Destroy(tempAgent.gameObject);
            tempAgent = null;
            ch = null;
        }
    }

    void BreedNextPopulation() {
        road1Agents = road1Agents.OrderBy(i => i.time).ToList();
        road2Agents = road2Agents.OrderBy(i => i.time).ToList();
        road3Agents = road3Agents.OrderBy(i => i.time).ToList();

        //We want a total of 15 cars in this list for double cloning (one with mutation, one without mutation)
        List<CarAgent> agentsForNewGeneration = new List<CarAgent>();

        //Get the best car from each track (3 total) (elitism)
        agentsForNewGeneration.Add(road1Agents[0]);
        agentsForNewGeneration.Add(road2Agents[0]);
        agentsForNewGeneration.Add(road3Agents[0]);
        uiHandler.UpdateTimes(road1Agents, road2Agents, road3Agents);

        //Get the car with the best overall time across all three tracks (1 total) (best total fitness)
        road1Agents = road1Agents.OrderBy(i => i.totalTimeAcrossTracks).ToList();
        agentsForNewGeneration.Add(road1Agents[0]);
        uiHandler.UpdateTotalTime(road1Agents[0]);

        for (int i = 0; i < road1Agents.Count; i++) {
            road1Agents[i].transform.parent = oldAgents;
            road2Agents[i].transform.parent = oldAgents;
            road3Agents[i].transform.parent = oldAgents;
        }
        road1Agents.Clear();
        road2Agents.Clear();
        road3Agents.Clear();

        BreedNewAgents(agentsForNewGeneration);

        foreach (Transform t in oldAgents) {
            Destroy(t.gameObject);
        }

        for (int i = 0, j = 0; i < agentsForNewGeneration.Count; i++, j += 2) {
            road1Agents[j].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road1Agents[j].handler = this;

            road1Agents[j + 1].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road1Agents[j + 1].genes.Mutate();
            road1Agents[j + 1].handler = this;


            road2Agents[j].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road2Agents[j].handler = this;

            road2Agents[j + 1].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road2Agents[j + 1].genes.Mutate();
            road2Agents[j + 1].handler = this;


            road3Agents[j].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road3Agents[j].handler = this;

            road3Agents[j + 1].genes.SetGeneSequence(agentsForNewGeneration[i].genes.genes);
            road3Agents[j + 1].genes.Mutate();
            road3Agents[j + 1].handler = this;
        }

        for (int i = 0; i < road1Agents.Count; i++) {
            road1Agents[i].GenotypeToPhenotype(road1Agents[i].genes);
            road2Agents[i].GenotypeToPhenotype(road2Agents[i].genes);
            road3Agents[i].GenotypeToPhenotype(road3Agents[i].genes);
        }

        time = 0;
        currentGeneration++;
        isRunningSimulation = true;
        uiHandler.UpdateGenerationCount(currentGeneration);
    }

    void Selection() {
        chromosomes.Clear();
        //TO DO: The selection should be rewritten to save the best cars from each track, before filling in the ranks in the Breed method
        for (int i = 0; i < road1Agents.Count; i++) {
            if (road1Agents[i].time == 0) road1Agents[i].time = timePerSimulation;
            if (road2Agents[i].time == 0) road2Agents[i].time = timePerSimulation;
            if (road3Agents[i].time == 0) road3Agents[i].time = timePerSimulation;
            road1Agents[i].totalTimeAcrossTracks = road1Agents[i].time + road2Agents[i].time + road3Agents[i].time;

            road1Agents[i].UpdateTime();
            road2Agents[i].UpdateTime();
            road3Agents[i].UpdateTime();

            chromosomes.Add(road1Agents[i].genes);
        }


        BreedNextPopulation();
    }
}