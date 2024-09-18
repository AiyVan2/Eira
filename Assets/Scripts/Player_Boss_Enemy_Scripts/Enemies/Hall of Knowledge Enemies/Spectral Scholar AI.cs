using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralScholarAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float teleportCooldown = 5.0f;
    public float spellCooldown = 3.0f;
    public GameObject spellPrefab;
    public Transform spellSpawnPoint;
    public GameObject illusionPrefab;
    public float illusionDuration = 5.0f;
    public int maxIllusions = 2;

    private Transform player;
    private bool isTeleporting = false;
    private bool isCastingSpell = false;

    // Define two fixed teleport positions
    public Transform teleportPosition1;
    public Transform teleportPosition2;
    private bool useFirstPosition = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(TeleportRoutine());
        StartCoroutine(SpellRoutine());
    }

    private void Update()
    {
        if (!isTeleporting && !isCastingSpell)
        {
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        // Implement movement logic here if needed
        // e.g., random movement or following a pattern
    }

    private IEnumerator TeleportRoutine()
    {
        while (true)
        {
            if (!isTeleporting)
            {
                isTeleporting = true;
                Teleport();
                yield return new WaitForSeconds(teleportCooldown);
                isTeleporting = false;
            }
            yield return null;
        }
    }

    private void Teleport()
    {
        if (player == null) return;

        // Choose between the two predefined positions
        Transform targetPosition = useFirstPosition ? teleportPosition1 : teleportPosition2;
        transform.position = targetPosition.position;

        // Toggle between the two positions
        useFirstPosition = !useFirstPosition;
    }

    private IEnumerator SpellRoutine()
    {
        while (true)
        {
            if (!isCastingSpell)
            {
                isCastingSpell = true;
                CastSpell();
                yield return new WaitForSeconds(spellCooldown);
                isCastingSpell = false;
            }
            yield return null;
        }
    }

    private void CastSpell()
    {
        if (spellPrefab != null && spellSpawnPoint != null)
        {
            Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);
        }
        CreateIllusions();
    }

    private void CreateIllusions()
    {
        for (int i = 0; i < maxIllusions; i++)
        {
            Instantiate(illusionPrefab, transform.position, Quaternion.identity);
        }
        // Optionally, destroy illusions after some time
        StartCoroutine(DestroyIllusionsAfterTime(illusionDuration));
    }

    private IEnumerator DestroyIllusionsAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        // Destroy all illusions here (if needed)
    }
}