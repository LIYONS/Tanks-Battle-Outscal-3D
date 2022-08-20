using UnityEngine;
using Cinemachine;
public class FollowPlayer : MonoBehaviour
{
    private Transform target;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cinemachineVirtualCamera.Follow = target;
        cinemachineVirtualCamera.LookAt = target;
    }
}
