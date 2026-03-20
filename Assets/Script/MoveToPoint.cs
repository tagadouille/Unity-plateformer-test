using UnityEngine;
using System.Collections.Generic;

public class MoveToPoint : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 1;
    [SerializeField] private float threshold = 0.05f;
    private Vector3[] waypointPositions;
    private int index = 0;

    private bool reverse = false;

    void Start()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            waypointPositions = new Vector3[0];
            return;
        }

        // If the waypoints are parented to this object, we keep their local positions. Otherwise, we use their world positions.
        List<Vector3> positions = new List<Vector3>(waypoints.Length);
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                positions.Add(waypoints[i].position);
            }
        }

        waypointPositions = positions.ToArray();
    }

    void Update()
    {
        if (waypointPositions == null || waypointPositions.Length == 0) return;
        
        if (waypointPositions.Length == 1)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                waypointPositions[0],
                speed * Time.deltaTime
            );
            return;
        }

        Vector3 target = waypointPositions[index];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // When the object is close enough
        if (Vector3.Distance(transform.position, target) <= threshold)
        {
            transform.position = target;
            index += reverse ? -1 : 1;

            if (index >= waypointPositions.Length)
            {
                index = waypointPositions.Length - 2;
                reverse = true;
            }
            else if (index < 0)
            {
                index = 1;
                reverse = false;
            }
        }
    }
}
