using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Path : MonoBehaviour
{
    Transform[] ChildNodes;

    public List<Transform> ChildNodeList = new List<Transform>();

    void Start()
    {
        FillNodes();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        FillNodes();

        for (int i = 0; i < ChildNodeList.Count; i++)
        {
            Vector3 pos = ChildNodeList[i].position;
            if (i > 0)
            {
                Vector3 prev = ChildNodeList[i - 1].position;
                Gizmos.DrawLine(pos, prev);
            }
        }
    }

    void FillNodes()
    {
        ChildNodeList.Clear();
        ChildNodes = GetComponentsInChildren<Transform>();

        foreach(Transform Child in ChildNodes)
        {
            if(Child != this.transform)
            {
                ChildNodeList.Add(Child);
            }
        }
    }

    public int RequestPosition(Transform nodeTransform)
    {
        return ChildNodeList.IndexOf(nodeTransform);
    }

    
    
}
