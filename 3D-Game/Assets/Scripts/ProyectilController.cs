using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    
    public float fireDelta = 0.5F;

    private float nextFire = 0.5F;
    private GameObject newProjectile;
    private float myTime = 0.0F;

    public int bullets;


    void Start() {
        bullets = 5;
    }

    void Update()
    {
        if (bullets > 0)
        {
            myTime = myTime + Time.deltaTime;

            if (Input.GetButton("Fire1") && myTime > nextFire)
            {
                GameObject fat = GameObject.Find("Level");
                GameObject player = GameObject.Find("Character");

                nextFire = myTime + fireDelta;
                var prefab = bulletPrefab;
                var gridTransform = bulletSpawnPoint.position;

                newProjectile = Instantiate(prefab, gridTransform, Quaternion.identity, fat.transform) as GameObject;


                nextFire = nextFire - myTime;
                myTime = 0.0F;


                bullets = bullets - 1;
            }
        }
    }


    public void collectChest() {
        this.bullets = this.bullets + 5;
    }
}
