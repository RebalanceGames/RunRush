using UnityEngine;
using UnityEngine.AI;

public class Alt_karakter : MonoBehaviour
{
    private NavMeshAgent _Navmesh;
    public GameManager _GameManager;
    public GameObject Target;
    
    private float gecenzaman = 0.00f;
    private float beklemesüresi = 1f;

    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();

        if (Target == null)
            Debug.LogError("Alt_karakter: Target atanmadı!");

        if (_GameManager == null)
            Debug.LogError("Alt_karakter: GameManager referansı atanmadı!");
    }

    void LateUpdate()
    {
        if (Target != null)
            _Navmesh.SetDestination(Target.transform.position);
    }

    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_GameManager == null) return; // Çökme engellenir

        if (other.CompareTag("igneliKutu") || other.CompareTag("Testere") || 
            other.CompareTag("Pervaneigneler") || other.CompareTag("Balyoz") || 
            other.CompareTag("Engeller"))
        {
            bool yukariZiplat = other.CompareTag("Balyoz") || other.CompareTag("Engeller");
            _GameManager.YokolmaEfektiOlustur(PozisyonVer(), yukariZiplat);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Dusman"))
        {
            var col = gameObject.GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;

            _GameManager.YokolmaEfektiOlustur(PozisyonVer(), true, false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Boss"))
        {
            var col = gameObject.GetComponent<BoxCollider>();
            if (col != null) col.enabled = false;

            if (Time.time > gecenzaman)
            {
                _GameManager.YokolmaEfektiOlustur(PozisyonVer(), true, false, false);
                gameObject.SetActive(false);
                gecenzaman = Time.time + beklemesüresi;
            }
        }
        else if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);
        }
    }
}