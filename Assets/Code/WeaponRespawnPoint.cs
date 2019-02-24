using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponRespawnPoint : MonoBehaviour
{
    public RarityType PointRarityType;
    public float AverageSpawnTime;
    public float InitialDelay;
    public bool Active;
    public GameObject SwordPrefab;
    public GameObject SpearPrefab;
    public GameObject AxePrefab;

    private float _spawnDelay;
    private float _initialDelay;
    private List<GameObject> _weaponsAvailable = new List<GameObject>();

    /// <summary>
    /// Visualize the spawn point in the editor; Credit to 376 HW code for function
    /// </summary>
    internal void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(new Vector3(0.4f, 0, 0)));
    }

    public void Start()
    {
        GenerateWeaponPool();
        _initialDelay = InitialDelay;
    }

    private void FixedUpdate()
    {
        if ( !Active ) return;
        if ( _initialDelay > 0 )
        {
            _initialDelay -= Time.fixedDeltaTime;
            return;
        }
        if ( _spawnDelay > 0 )
        {
            _spawnDelay -= Time.fixedDeltaTime;
        }
        else
        {
            SpawnWeapon();
            _spawnDelay = UnityEngine.Random.Range( ( AverageSpawnTime * 0.75f ), ( AverageSpawnTime * 1.25f ) );
        }
    }

    private void SpawnWeapon()
    {
        var index = Random.Range( 0, _weaponsAvailable.Count );
        var prefabToSpawn = _weaponsAvailable[index];
        var weapon = Instantiate( prefabToSpawn, transform.position, transform.rotation );
    }

    private void GenerateWeaponPool()
    {
        switch ( PointRarityType )
        {
            case RarityType.High:
                _weaponsAvailable.Add(AxePrefab);
                _weaponsAvailable.Add(SpearPrefab);
                break;
            case RarityType.Medium:
                _weaponsAvailable.Add(SpearPrefab);
                _weaponsAvailable.Add(SwordPrefab);
                break;
            case RarityType.Common:
                _weaponsAvailable.Add(SwordPrefab);
                break;
        }
    }

    public enum RarityType
    {
        Common = 1,
        Medium = 2,
        High = 3,
    }
}