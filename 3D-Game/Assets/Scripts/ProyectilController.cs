using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 40;
            //bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 10f, ForceMode.Impulse);

            Destroy(bullet, 2.0f);
        }
    }
}
