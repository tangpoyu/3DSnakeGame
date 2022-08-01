using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector]
    public float step_Length = 0.2f;

    [HideInInspector]
    public float movement_Frequency = 0.1f;
    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tailPrefab;
    private List<Vector3> delta_Position;
    private List<Rigidbody> nodes;

    private Rigidbody mainBody;
    private Rigidbody headBody;
    private Transform tr;
    private bool createNodeAtTail;

    private void Awake()
    {
        tr = transform;
        mainBody = GetComponent<Rigidbody>();
        InitSnakeNodes();
        InitPlayer();

        delta_Position = new List<Vector3>() {
            new Vector3(-step_Length, 0f),
            new Vector3(0f, step_Length),
            new Vector3(step_Length, 0f),
            new Vector3(0f, -step_Length)
        };
    }

    private void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        headBody = nodes[0];
    }


    private void InitPlayer()
    {
        SetDirectionRandom();
        switch(direction)
        {
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f); 
                break;
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f , Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f , Metrics.NODE * 2f, 0f);
                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
        }
    }

    private void SetDirectionRandom()
    {
        direction = (PlayerDirection) UnityEngine.Random.Range(0, (int) PlayerDirection.COUNT);
    }

    void Move()
    {
        Vector3 dPosition = delta_Position[(int) direction];
        Vector3 parentPos = headBody.position;
        Vector3 prevPosition;

        mainBody.position = mainBody.position + dPosition;
        headBody.position = headBody.position + dPosition;

        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;
            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }
        if(createNodeAtTail)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();
    }

    private void FixedUpdate()
    {
        if(move)
        {
            move = false;
            Move();
        }
    }

    private void CheckMovementFrequency()
    {
        counter += Time.deltaTime; 

        if (counter >= movement_Frequency)
        {
            counter = 0;
            move = true;
        }
    }
}
