using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool SonaGeldikmi;
    public GameObject Gidecegiyer;

    private void Start()
    {
        if (target != null)
            target_offset = transform.position - target.position;
        else
            Debug.LogError("Kamera: target referansı atanmadı!");
    }

    private void LateUpdate()
    {
        if (!SonaGeldikmi)
        {
            if (target != null)
                transform.position = Vector3.Lerp(transform.position, target.position + target_offset, 0.125f);
        }
        else
        {
            if (Gidecegiyer != null)
                transform.position = Vector3.Lerp(transform.position, Gidecegiyer.transform.position, 0.009f);
        }
    }
}