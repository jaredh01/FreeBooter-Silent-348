using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    /// <summary>
    /// Visualize the spawn point in the editor; Credit to 376 HW code for function
    /// </summary>
    internal void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(new Vector3(0.4f, 0, 0)));
    }

    /// <summary>
    /// Find the distance of the closest player to the spawn point.
    /// </summary>
    public float DistanceFromPlayer()
    {
        return FindObjectsOfType<Player>().Min( p => Vector3.Distance( transform.position, p.transform.position ) );
    }

    /// <summary>
    /// Samples all respawn points, and returns the one farthest from other players.
    /// </summary>
    public static RespawnPoint FindBestPoint()
    {
        var all = FindObjectsOfType<RespawnPoint>();
        RespawnPoint best = all[0];
        var score = 0f;

        foreach ( var point in all )
        {
            var s = point.DistanceFromPlayer();
            if ( !( s > score ) ) continue;
            score = s;
            best = point;
        }

        return best;
    }
}
