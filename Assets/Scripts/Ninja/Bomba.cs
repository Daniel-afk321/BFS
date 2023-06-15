using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomba : MonoBehaviour
{
    public GameObject bombaPrefab;
    public int maxSpawnCount = 3;
    public GameObject vfxPrefab;
    public float bombaDuration = 5f;
    public float vfxDuration = 6f;

    private int spawnCount = 0;
    public Text bombCountText;
    public AudioSource bomba;
    private void Start()
    {
        UpdateBombCountText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && spawnCount < maxSpawnCount)
        {
            SpawnBomba();
            SpawnVFX();
            spawnCount++;
            bomba.Play();
            if (spawnCount >= maxSpawnCount)
            {
                enabled = false;
            }

            UpdateBombCountText();
        }
    }

    private void SpawnBomba()
    {
        GameObject bomba = Instantiate(bombaPrefab, transform.position, Quaternion.identity);
        Destroy(bomba, bombaDuration);
    }

    private void SpawnVFX()
    {
        GameObject vfx = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, vfxDuration);
    }

    private void UpdateBombCountText()
    {
        if (bombCountText != null)
        {
            int remainingBombs = maxSpawnCount - spawnCount;
            bombCountText.text =  remainingBombs.ToString();
        }
    }
}