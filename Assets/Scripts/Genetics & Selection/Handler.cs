using System.Collections.Generic;
using UnityEngine;


public class Handler : MonoBehaviour {
    [SerializeField] private int populationSize = 10;
    private List<CarAgent> agents = new List<CarAgent>();
    [SerializeField] private GameObject carAgent = null;
    [SerializeField] private GameObject road = null;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            BreedNewPopulation();
        }
    }

    void BreedNewPopulation() {
        Vector3 position = Vector3.zero + Vector3.up * 5;
        for (int i = 0; i < populationSize; i++) {
            Instantiate(road, position, Quaternion.identity);
            agents.Add(Instantiate(carAgent, position + Vector3.up * 2, Quaternion.identity).GetComponent<CarAgent>());
            agents[i].genes = new Chromosome();
            position += Vector3.right * 10;
        }
    }
}