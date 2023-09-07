using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Sprite> sprites;

    private bool[] crewState = new bool[12];
    private float timer = 0.5f;
    private float distance = 11f;

    void Start()
    {
        for(int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance));
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        if(!crewState[(int)playerColor])
        {
            crewState[(int)playerColor] = true;

            float angle = Random.Range(0f, 360f);
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            float floatingSpeed = Random.Range(1f, 4f);
            float rotateSpeed = Random.Range(-3f, 3f);

            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).
                GetComponent<FloatingCrew>();
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, 
                direction, floatingSpeed, rotateSpeed, Random.Range(0.5f, 1f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var crew = other.GetComponent<FloatingCrew>();
        if(crew != null)
        {
            crewState[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }
}
