using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NodeGenerator : MonoBehaviour
{

    public GameObject player;
    public GameObject net, fish;
    public float netWidthCoef = 0.9f;
    public GameObject netPreFab;
    public int numberOfNodes;
    public GameObject nodePreFab;
 


    

    List<GameObject> nodes;
    LineRenderer lineRenderer;
    float netWidth = 1f;
    // Start is called before the first frame update
    void Start()
    {
        nodes = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        if (net == null)
            net = player.transform.Find("net").gameObject;

        if (fish == null)
            fish = player.transform.Find("fish").gameObject;

        var nsr = net.GetComponent<SpriteRenderer>();
        if (nsr != null) {
            netWidth = nsr.bounds.max.x - nsr.bounds.min.x;
            netWidth *= netWidthCoef;
        }
        else
        {
            Debug.LogError("No sprite renderer is present on the net object, or it is found incorrectly");
        }
        

        for (int i = 1; i < numberOfNodes; i++)
        {
            var fish2net = net.transform.TransformPoint(new Vector3(netWidth * 0.5f, 0f))- fish.transform.position;
            //GameObject temp = (GameObject)Instantiate(nodePreFab, fish.transform.position+(net.transform.position - fish.transform.position + new Vector3(  6,0,0   ))*i / numberOfNodes, Quaternion.identity);
            var temp = Instantiate(nodePreFab);
            temp.transform.position = fish.transform.position + fish2net * ( (float)i / numberOfNodes);
            temp.transform.rotation = Quaternion.identity;
            temp.name = "ropenode" + i;
            temp.transform.parent = player.transform;
            nodes.Add(temp);
        }
        //player.transform.Find("ropenode1").gameObject.GetComponent<DistanceJoint2D>().connectedBody = fish.GetComponent<Rigidbody2D>();
        nodes[0].GetComponent<DistanceJoint2D>().connectedBody = fish.GetComponent<Rigidbody2D>();

        for(int i = 1; i < nodes.Count; i++)
        {
            //string currentNode = "ropenode" + i;
            //int previousNodeInt = i - 1;
            //string previousNode = "ropenode" + previousNodeInt;
            //player.transform.Find(currentNode).gameObject.GetComponent<DistanceJoint2D>().connectedBody = player.transform.Find(previousNode).gameObject.GetComponent<Rigidbody2D>();
            var currentNode = nodes[i];
            var prevNode = nodes[i - 1];
            currentNode.GetComponent<DistanceJoint2D>().connectedBody = prevNode.GetComponent<Rigidbody2D>();
        }
        //int tempInt = numberOfNodes - 1;
        //string tempString = "ropenode" + tempInt;

        //var ndj = net.gameObject.GetComponent<DistanceJoint2D>();
        ////ndj.connectedBody = player.transform.Find(tempString).gameObject.GetComponent<Rigidbody2D>();
        //ndj.connectedBody = nodes[nodes.Count - 1].GetComponent<Rigidbody2D>();
        //ndj.anchor = new Vector2(netWidth * 0.5f, ndj.anchor.y);
        ConnectNetToRopeNodes();

        lineRenderer.positionCount = nodes.Count+2;
        //lineRenderer.SetPosition(0, fish.transform.position);
        //for (int i=1; i<=nodes.Count; i++)
        //{
        //    lineRenderer.SetPosition(i, nodes[i-1].transform.position);
        //}
        //lineRenderer.SetPosition(nodes.Count+1, net.transform.TransformPoint(new Vector3(netWidth * 0.5f, 0f)));
    }

    private DistanceJoint2D debugNetDistJoint;
    private float debugNDJLastDist;
    private void Update()
    {
        if (debugNetDistJoint.distance != debugNDJLastDist)
            Debug.LogFormat("NetWidthChanged: Prev: {0}; Current: {1}", debugNDJLastDist, debugNetDistJoint.distance);

        if (!GameController.instance.IsGameOver)
        {
            lineRenderer.SetPosition(0, fish.transform.position);
            for (int i = 1; i <= nodes.Count; i++)
            {
                lineRenderer.SetPosition(i, nodes[i - 1].transform.position);
            }
            lineRenderer.SetPosition(nodes.Count + 1, net.transform.TransformPoint(new Vector3(netWidth * 0.5f, 0f)));
        }
        //for(int i = 1; i < numberOfNodes; i++)
        //{
        //    string currentNode = "ropenode" + i;
        //    Vector3 currentNodePosition = player.transform.Find(currentNode).gameObject.transform.position;
        //    Vector3 previousNodePosition = player.transform.Find(currentNode)
        //        .gameObject.GetComponent<DistanceJoint2D>().connectedBody.gameObject.transform.position;
        //    GL.Begin(GL.LINES);
        //    lineMat.SetPass(0);
        //    GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
        //    GL.Vertex3(currentNodePosition.x, currentNodePosition.y, currentNodePosition.z);
        //    GL.Vertex3(previousNodePosition.x, previousNodePosition.y, previousNodePosition.z);
        //    GL.End();
        //}
    }

    private void ConnectNetToRopeNodes()
    {
        if (nodes == null || nodes.Count < 1)
            return;

        var ndj = net.gameObject.GetComponent<DistanceJoint2D>();
        ndj.connectedBody = nodes[nodes.Count - 1].GetComponent<Rigidbody2D>();
        ndj.anchor = new Vector2(netWidth * 0.5f, ndj.anchor.y);

        debugNetDistJoint = ndj;
        debugNDJLastDist = ndj.distance;
    }

    public void NewNet(Vector3 netPosition, Quaternion netRotation)
    {
        GameObject temp = Instantiate(netPreFab, netPosition,netRotation);
        temp.transform.parent = this.gameObject.transform;
        net = temp;

        //int tempInt = numberOfNodes - 1;
        //string tempString = "ropenode" + tempInt;
        //net.GetComponent<DistanceJoint2D>().connectedBody = player.transform.Find(tempString).gameObject.GetComponent<Rigidbody2D>();
        ConnectNetToRopeNodes();
    }
}

                
                
                
