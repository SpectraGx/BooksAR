using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackerPrefabs : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;


    [System.Serializable]
    public class PrefabInfo
    {
        public string imageName; 
        public GameObject prefab;   
    }

    public List<PrefabInfo> prefabInfos; 
    private Dictionary<string, GameObject> prefabsSpawns = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            string imageName = trackedImage.referenceImage.name;

            if (prefabsSpawns.ContainsKey(imageName))
            {
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    GameObject prefab = prefabsSpawns[imageName];
                    prefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
                    prefab.SetActive(true);
                }
                else
                {
                    prefabsSpawns[imageName].SetActive(false);
                }
            }
            else if (trackedImage.trackingState == TrackingState.Tracking)
            {
                GameObject prefabCorrect = SearchPrefab(imageName);

                if (prefabCorrect != null)
                {
                    /*
                    GameObject newPrefab = Instantiate(prefabCorrect, trackedImage.transform);

                    prefabsSpawns.Add(imageName, newPrefab);
                    */

                    GameObject newPrefab = Instantiate(prefabCorrect, trackedImage.transform);
                    prefabsSpawns.Add(imageName, newPrefab);

                    if (PlayerPrefs.GetInt(imageName, 0) == 0)
                    {
                        PlayerPrefs.SetInt(imageName, 1);
                        PlayerPrefs.Save();
                        Debug.Log("Libro desbloqueado: " + imageName);
                    }
                }
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            if (prefabsSpawns.ContainsKey(imageName))
            {
                Destroy(prefabsSpawns[imageName]); 
                prefabsSpawns.Remove(imageName);   
            }
        }
    }

    private GameObject SearchPrefab(string imageName)
    {
        foreach (PrefabInfo info in prefabInfos  )
        {
            if (info.imageName == imageName)
            {
                return info.prefab;
            }
        }
        return null; 
    }
}
