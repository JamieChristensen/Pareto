using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathUtility;

public class CarAgent : MonoBehaviour
{
    public bool isCreatedManually = false;
    public float time = 0;
    public float totalTimeAcrossTracks = 0;
    //Handler has a CarAgent type prefab, that it instatiates with this chromosome:
    [SerializeField] public Chromosome genes;
    [Tooltip("The max speed of the vehicle, as speed approaches this, acceleration approaches 0")]
    public float maxSpeed;
    [HideInInspector] public Handler handler = null;
    [HideInInspector] public int track = 0;


    [Header("Car attributes")]
    public float acceleration;

    [Tooltip("Fuel consumptions as units/second")]
    public float fuelConsumptionRate;
    [Tooltip("Total fuel capacity")]
    public float fuelCapacity;

    public float size; //as a uniform scale.
    public float totalWeight;

    [Header("GameObject references")]
    [SerializeField] private Rigidbody[] wheelRigidBodies = new Rigidbody[4];


    [Header("Visualization")]
    public GameObject fuelContent;

    //Object state & consts:
    private float currentFuel;
    private float deltaTime;

    const float baseWeight = 30f;

    void Start()
    {
        if (isCreatedManually)
        {
            genes = new Chromosome();
            GenotypeToPhenotype(genes);
        }

        foreach (Rigidbody rb in wheelRigidBodies)
        {
            rb.maxAngularVelocity = Mathf.Infinity;
        }
        deltaTime = Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        //Drive
        foreach (Rigidbody rb in wheelRigidBodies)
        {
            rb.AddRelativeTorque(new Vector3(1, 0, 0) * acceleration, ForceMode.Force);

        }

        currentFuel -= fuelConsumptionRate * deltaTime;

        Visualize();
    }

    public void GenotypeToPhenotype(Chromosome genes)
    {
        //Just set a bunch of the above parameters using the genes.

        //TODO: Size-weight relationship.
        genes.SetGeneValue(GeneType.Size, MathUtils.map(size, 0.8f, 3, 0, 1));

        totalWeight =
            genes.GetGeneValueOfGeneType(GeneType.FuelCapacity) * 0.1f +
            genes.GetGeneValueOfGeneType(GeneType.Weight);

        totalWeight += totalWeight * (size / 8);

        fuelConsumptionRate = totalWeight / baseWeight;


        GetComponent<Rigidbody>().mass = totalWeight;

        acceleration = genes.GetGeneValueOfGeneType(GeneType.Acceleration);
    }

    private void Visualize()
    {
        //Lerp scale of fuel content object (y-scale) by current fuel as t and range of fuel contents.

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            if (time == 0) {
                time = handler.time;
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public void UpdateTime() {
        genes.NewTime(time, track);
    }
}