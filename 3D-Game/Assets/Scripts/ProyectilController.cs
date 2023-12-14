using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    
    public float fireDelta = 0.5F;

    private float nextFire = 0.5F;
    private GameObject newProjectile;
    private float myTime = 0.0F;
    
    void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire) 
        {
            GameObject fat = GameObject.Find("Level");
            GameObject player = GameObject.Find("Character");

            nextFire = myTime + fireDelta;
            var prefab = bulletPrefab;
            var gridTransform = bulletSpawnPoint.position;

            newProjectile  = Instantiate(prefab, gridTransform, Quaternion.identity, fat.transform) as GameObject;


            nextFire = nextFire - myTime;
            myTime = 0.0F;

            Destroy(newProjectile, 2f);
        }
    }
}
