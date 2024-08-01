using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //0.6 8 -10
    //15 0 0
    [SerializeField]
    private Transform target;
    private float previousTargetZ;

    private Vector3 defaultOffset = new Vector3(0f, 3f, -4f); // Varsay�lan offset de�erleri
    private Vector3 rotatedOffset = new Vector3(0f, 3f, 4f); // 180 derece d�n��te kullan�lacak offset de�erleri

    [SerializeField]
    private float chaseSpeed = 5;
    [SerializeField]
    private float rotationX = 15f;

    TweenManager _tweenManager;
    BallSelect _ballSelect;

    // Start is called before the first frame update
    void Start()
    {
        _tweenManager = FindObjectOfType<TweenManager>();
        _ballSelect = FindObjectOfType<BallSelect>();
    }

    private void LateUpdate()
    {
        target = _ballSelect.selectedBallTransform;

        if (_tweenManager.isBallsUIActive == false && _tweenManager.isBackgroundUIActive == false)
        {
            CameraFollowSystem();
        }
    }

    private void CameraFollowSystem()
    {
        Vector3 targetPosition = target.position + (transform.rotation.eulerAngles.y == 180f ? rotatedOffset : defaultOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, chaseSpeed * Time.deltaTime);

        float currentTargetZ = target.position.z;

        if (currentTargetZ > previousTargetZ)
        {
            // Hedefin z pozisyonu artt���nda
            gameObject.transform.rotation = Quaternion.Euler(rotationX, 0f, transform.eulerAngles.z);
        }
        else if (currentTargetZ < previousTargetZ)
        {
            // Hedefin z pozisyonu k���ld���nde
            gameObject.transform.rotation = Quaternion.Euler(rotationX, 180f, transform.eulerAngles.z);
        }

        previousTargetZ = currentTargetZ;
    }
}