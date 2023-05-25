using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;

    private int _index;

    [SerializeField] private float platformWidth = 2f;
    [SerializeField] private float platformHeight = 1f;
    [SerializeField] private int minX;
    [SerializeField] private int maxX;
    [SerializeField] private float platformDistance;

    public static PlatformSpawner Instance;
    private Camera _mainCamera;

    private List<GameObject> _platformList = new List<GameObject>();

    [SerializeField] private float hue;

    private void Start()
    {
        _mainCamera = Camera.main;
        
        if (Instance == null)
        {
            Instance = this;
        }

        InitialColor();
        CreateObjects();

        for (var i = 0; i < 5; i++)
        {
            CreateNewPlatform();
        }
    }

    private void InitialColor()
    {
        hue = Random.Range(0f, 1f);
        _mainCamera.backgroundColor = Color.HSVToRGB(hue, 0.25f, 0.65f);
    }

    private void CreateObjects()
    {
        for (var i = 0; i < 6; i++)
        {
            var platform = Instantiate(platformPrefab);
            platformPrefab.SetActive(false);
            _platformList.Add(platform);
        }
    }

    private GameObject GetPlatform()
    {
        foreach (var platform in _platformList)
        {
            if (platform.activeInHierarchy) continue;
            var myPlatform = platform;
            return myPlatform;
        }
        return null;
    }
    
    public void CreateNewPlatform()
    {
        var randomPosition = _index == 0 ? 0 : Random.Range(minX, maxX);
        
        var newPosition = new Vector2(randomPosition,_index * platformDistance);

        var platform = GetPlatform();
        platform.SetActive(true);
        platform.transform.position = newPosition;
        platform.transform.rotation = Quaternion.identity;
        platform.transform.localScale = new Vector2(platformWidth,platformHeight);
        platform.transform.SetParent(transform);
        SetPlatformColor(platform);
        _index++;
    }

    private void SetPlatformColor(GameObject platform)
    {
        if (Random.Range(0,3) != 0)
        {
            hue += 0.15f;
            if (hue >= 1)
            {
                hue -= 1f;
            }
        }
        platform.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue, 0.25f, 0.65f);
    }
}
