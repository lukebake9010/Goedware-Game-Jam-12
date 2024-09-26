using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject camAnchor;

    [SerializeField]
    private Vector3 camOffset = new Vector3(0f,6f,-3.5f);

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerCameraAnchor.Instance == null)
        {
            if (!findingAnchor) StartCoroutine(FindCamAnchor());
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(camAnchor == null)
        {
            if(!findingAnchor) StartCoroutine (FindCamAnchor());
            return;
        }
        gameObject.transform.position = camAnchor.transform.position + camOffset;
    }

    private bool findingAnchor = false;
    private IEnumerator FindCamAnchor()
    {
        while (true && camAnchor == null) 
        {
            findingAnchor = true;
            if(PlayerCameraAnchor.Instance != null)
            {
                camAnchor = PlayerCameraAnchor.Instance.gameObject;
                findingAnchor = false;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }

}
