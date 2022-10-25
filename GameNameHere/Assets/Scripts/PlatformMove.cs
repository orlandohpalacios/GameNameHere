using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Pos1, Pos2;
    public float speed;
    public Transform startPos;
    Vector3 NextPos;
    void Start()
    {
        NextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == Pos1.position) 
        {
            NextPos = Pos2.position;
        }
        if (transform.position == Pos2.position)
        {
            NextPos = Pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, NextPos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Pos1.position, Pos2.position);
    }
}
