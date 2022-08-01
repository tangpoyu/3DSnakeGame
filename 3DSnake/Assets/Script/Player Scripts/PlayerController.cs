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
    }

    private void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetChild(0).GetComponent<Rigidbody>());

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

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
