using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public GameObject cubePrefab;

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
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            Instantiate(cubePrefab, trackedImage.transform.position, trackedImage.transform.rotation, trackedImage.transform);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                trackedImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Destroy(trackedImage.transform.GetChild(0).gameObject);
        }
    }
}
