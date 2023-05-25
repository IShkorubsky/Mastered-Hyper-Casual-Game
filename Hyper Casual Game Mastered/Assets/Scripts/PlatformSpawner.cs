using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;

    private int _index;

    [SerializeField] private float platformWidth = 2f;
    [SerializeField] private float platformHeight = 0.5f;
    [SerializeField] private int minX;
    [SerializeField] private int maxX;
    [SerializeField] private float platformDistance;

    public static PlatformSpawner Instance;
    
    private List<GameObject> _platformList = new List<GameObject>();
    
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        CreateObjects();
    }

    private void CreateObjects()
    {
        for (var i = 0; i < 5; i++)
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
        _index++;
    }
}
