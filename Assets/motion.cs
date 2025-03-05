using JetBrains.Annotations;
using UnityEngine;

public class motion : MonoBehaviour
{
    
    public Transform path;
    private float speed = 2f;
    private int currentTargetWaypointIndex = 0;
    private int TOTALWAYPOINTS = 3;

    // Update is called once per frame
    void Update()
    {
        int index = 0;
        foreach (Transform waypoint in path.GetComponentsInChildren<Transform>())
        {
            // Skip parent
            if (waypoint == path) continue;

            // check if this is our current target waypoint
            if (index == currentTargetWaypointIndex)
            {
                Vector2 direction = waypoint.position - transform.position;
                float angle = Vector2.SignedAngle(Vector2.right, direction);
                transform.eulerAngles = new Vector3(0, 0, angle);

                transform.position = Vector2.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);

                // check if we reached the waypoint
                if (Vector2.Distance(transform.position, waypoint.position) < 0.1f)
                {
                    currentTargetWaypointIndex = (currentTargetWaypointIndex + 1) % TOTALWAYPOINTS;
                }

                //found target waypoint we exit
                break;
            }

            // move to next waypoint in the foreach loop
            index++;
        }
    }
}
