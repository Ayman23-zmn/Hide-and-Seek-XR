/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f; // Time interval for spawning monsters
    [SerializeField] private Vector3 spawnVelocityRange = new Vector3(5f, 5f, 5f); // Range for random velocity
    [SerializeField] private GameObject parentObject; // Parent object containing monster types
    [SerializeField] private int maxMonstersInScene = 5; // Maximum number of active monsters in the scene

    private List<GameObject> monsterTypes = new List<GameObject>(); // List of available monster types
    private List<MRUKAnchor> wallAnchors = new List<MRUKAnchor>(); // List of wall anchors
    private int activeMonsters = 0; // Counter for active monsters

    private void Start()
    {
        // Collect all child objects as monster types
        foreach (Transform child in parentObject.transform)
        {
            monsterTypes.Add(child.gameObject);
        }

        // Find all wall anchors in the scene
        FindWallAnchors();

        if (wallAnchors.Count == 0)
        {
            Debug.LogError("No WALL_FACE anchors found. Monsters cannot spawn.");
            return;
        }

        // Start the spawning routine
        StartCoroutine(SpawnRoutine());
    }

    private void FindWallAnchors()
    {
        MRUKAnchor[] allAnchors = FindObjectsOfType<MRUKAnchor>();

        foreach (var anchor in allAnchors)
        {
            if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.WALL_FACE))
            {
                wallAnchors.Add(anchor);
                Debug.Log($"Found WALL_FACE anchor: {anchor.name}");
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (activeMonsters < maxMonstersInScene)
            {
                SpawnMonster();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonster()
    {
        // Randomly pick a wall anchor for spawning
        MRUKAnchor spawnAnchor = wallAnchors[Random.Range(0, wallAnchors.Count)];

        // Randomly pick a monster type to duplicate
        GameObject selectedMonster = monsterTypes[Random.Range(0, monsterTypes.Count)];

        // Spawn the monster
        GameObject spawnedMonster = Instantiate(selectedMonster, spawnAnchor.GetAnchorCenter(), Quaternion.identity, parentObject.transform);
        spawnedMonster.name = $"{selectedMonster.name}_Clone";

        // Apply random velocity to the spawned monster
        Rigidbody rb = spawnedMonster.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomVelocity = new Vector3(
                Random.Range(-spawnVelocityRange.x, spawnVelocityRange.x),
                Random.Range(-spawnVelocityRange.y, spawnVelocityRange.y),
                Random.Range(-spawnVelocityRange.z, spawnVelocityRange.z)
            );

            rb.velocity = randomVelocity;
            Debug.Log($"Spawned {spawnedMonster.name} with velocity {randomVelocity}");
        }
        else
        {
            Debug.LogWarning($"Spawned monster {spawnedMonster.name} has no Rigidbody component. Cannot apply velocity.");
        }

        activeMonsters++; // Increase active monster count

        // Start bouncing behavior
        StartCoroutine(BounceBehavior(spawnedMonster));
    }

    private IEnumerator BounceBehavior(GameObject monster)
    {
        Rigidbody rb = monster.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"Monster {monster.name} has no Rigidbody. Cannot bounce.");
            yield break;
        }

        while (monster != null)
        {
            // Check if the monster is colliding with a surface (uses physics material for bouncing)
            if (rb.velocity.magnitude < 0.1f)
            {
                Vector3 randomBounce = new Vector3(
                    Random.Range(-spawnVelocityRange.x, spawnVelocityRange.x),
                    Random.Range(spawnVelocityRange.y / 2, spawnVelocityRange.y), // Ensure upward bounce
                    Random.Range(-spawnVelocityRange.z, spawnVelocityRange.z)
                );

                rb.velocity = randomBounce;
                Debug.Log($"{monster.name} bounced with velocity {randomBounce}");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs; // Array of monster prefabs
    [SerializeField] private float spawnInterval = 2f;    // Time interval for spawning monsters
    [SerializeField] private Vector3 spawnVelocity = new Vector3(5f, 5f, 5f); // Initial velocity for spawned monsters
    [SerializeField] private Transform monsterParent;     // Parent object to hold spawned monsters
    [SerializeField] private int maxMonstersInScene = 5;  // Maximum number of active monsters allowed

    private List<MRUKAnchor> wallAnchors = new List<MRUKAnchor>(); // List of wall anchors
    private List<GameObject> activeMonsters = new List<GameObject>(); // List of active monsters

    private void Start()
    {
        // Ensure monster prefabs are assigned
        if (monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogError("No monster prefabs assigned.");
            return;
        }

        // Find all wall anchors in the scene
        FindWallAnchors();

        if (wallAnchors.Count == 0)
        {
            Debug.LogError("No WALL_FACE anchors found. Monsters cannot spawn.");
            return;
        }

        // Start the spawning routine
        StartCoroutine(SpawnRoutine());
    }

    private void FindWallAnchors()
    {
        MRUKAnchor[] allAnchors = FindObjectsOfType<MRUKAnchor>();

        foreach (var anchor in allAnchors)
        {
            if (anchor.HasAnyLabel(MRUKAnchor.SceneLabels.WALL_FACE))
            {
                wallAnchors.Add(anchor);
                Debug.Log($"Found WALL_FACE anchor: {anchor.name}");
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (activeMonsters.Count < maxMonstersInScene)
            {
                SpawnMonster();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMonster()
    {
        // Randomly pick a wall anchor
        MRUKAnchor spawnAnchor = wallAnchors[Random.Range(0, wallAnchors.Count)];

        // Randomly pick a monster prefab
        GameObject selectedPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

        // Spawn the monster at the anchor position
        GameObject spawnedMonster = Instantiate(selectedPrefab, spawnAnchor.GetAnchorCenter(), Quaternion.identity, monsterParent);
        spawnedMonster.name = $"{selectedPrefab.name}_Clone";

        // Apply initial velocity
        Rigidbody rb = spawnedMonster.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomVelocity = new Vector3(
                Random.Range(-spawnVelocity.x, spawnVelocity.x),
                Random.Range(-spawnVelocity.y, spawnVelocity.y),
                Random.Range(-spawnVelocity.z, spawnVelocity.z)
            );

            rb.linearVelocity = randomVelocity;
            Debug.Log($"Spawned {spawnedMonster.name} with velocity {randomVelocity}");
        }
        else
        {
            Debug.LogWarning($"Spawned monster {spawnedMonster.name} has no Rigidbody. Cannot apply velocity.");
        }

        // Add to active monsters list
        activeMonsters.Add(spawnedMonster);

        // Handle monster removal on destroy
        spawnedMonster.AddComponent<MonsterRemovalHandler>().Initialize(this);
    }

    public void RemoveMonster(GameObject monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            Destroy(monster);
            Debug.Log($"{monster.name} removed from the scene.");
        }
    }
}

public class MonsterRemovalHandler : MonoBehaviour
{
    private MonsterSpawner spawner;

    public void Initialize(MonsterSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveMonster(gameObject);
        }
    }
}

