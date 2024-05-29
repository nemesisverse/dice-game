using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int Coordinate = new Vector2Int();

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinate();
        }
    }

    void DisplayCoordinate()
    {
        Coordinate.x = Mathf.RoundToInt(transform.parent.position.x/10);
        Coordinate.y = Mathf.RoundToInt(transform.parent.position.z / 10);

        label.text = Coordinate.x + " " + Coordinate.y;
    }
}
