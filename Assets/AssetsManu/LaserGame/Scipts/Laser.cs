using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Laser : MonoBehaviour
{
    SoundPlayerScript soundPlayer;

    [SerializeField] Transform Gun;
    [SerializeField] TMPro.TextMeshProUGUI Finito;
    [SerializeField] AudioClip laserSound;
    public LayerMask layersToHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundPlayer = GetComponent<SoundPlayerScript>();
        Finito.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        soundPlayer.PlaySound(laserSound, transform, 0.5f);
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 50f, layersToHit);
        if (hit.collider == null)
        {
            transform.localScale = new Vector3(50f, transform.localScale.y, 1);
            return;
        }
        
        if(hit.collider.tag == "UVO")
        {
            transform.localScale = new Vector3((hit.collider.transform.position.x - Gun.transform.position.x) / 2.3f, transform.localScale.y, 1);

        }
        if (hit.collider.tag == "Ennemi")
        {
            transform.localScale = new Vector3((hit.collider.transform.position.x - Gun.transform.position.x ) /2.3f , transform.localScale.y, 1);
            Destroy(hit.collider.gameObject);
            Finito.enabled = true;


        }
    }
}
