using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    /// <summary>
    /// Check for a <see cref="GameObject"/>'s <see cref="Respawner"/> and perform a respawn.
    /// </summary>
    /// <returns>IF a respawn was performed</returns>
    public bool TryRespawn(GameObject o)
    {
        var respawner = o.GetComponent<Respawner>();
        if ( !respawner ) return false;
        respawner.Respawn();
        return true;
    }

    /// <summary>
    /// Moves the <see cref="Rigidbody2D"/> of the parent <see cref="GameObject"/> to the best <see cref="RespawnPoint"/>
    /// </summary>
    private void Respawn()
    {
        var point = RespawnPoint.FindBestPoint().transform;
        var rigidBody = GetComponent<Rigidbody2D>();
        var playerHeight = GetComponent<Renderer>().bounds.size.y;
        rigidBody.MovePosition( point.position + 0.5f * playerHeight * Vector3.up );
    }
}
