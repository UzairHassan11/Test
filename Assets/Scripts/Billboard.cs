using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTrans;
    
    private void Start()
    {
        camTrans = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + camTrans.forward);
    }
}
