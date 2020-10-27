using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour
{
    public bool isCreatedManually = false;
    //Handler has a CarAgent type prefab, that it instatiates with this chromosome:
    [SerializeField] public Chromosome genes;
    [Tooltip("The max speed of the vehicle, as speed approaches this, acceleration approaches 0")]
    public float maxSpeed;


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
    const float baseWeight = 500f;

    void Start()
    {
        if (!isCreatedManually)
        {
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

    private void GenotypeToPhenotype(Chromosome genes)
    {
        //Just set a bunch of the above parameters using the genes.

        //TODO: Size-weight relationship.
        size = genes.GetGeneValueOfGeneType(GeneType.Size);

        transform.localScale = new Vector3(1, 1, 1);

        totalWeight = baseWeight +
            genes.GetGeneValueOfGeneType(GeneType.FuelCapacity) +
            genes.GetGeneValueOfGeneType(GeneType.Weight);

        fuelConsumptionRate = totalWeight / baseWeight;

        totalWeight += totalWeight * (size / 8);

        acceleration = genes.GetGeneValueOfGeneType(GeneType.Acceleration);
    }

    private void Visualize()
    {
        //Lerp scale of fuel content object (y-scale) by current fuel as t and range of fuel contents.

    }
}
