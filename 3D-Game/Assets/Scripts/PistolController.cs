using UnityEngine;

public class PistolController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public GameObject UI;

    public AudioSource bang;

    public float fireDelta = 0.5F;

    private float nextFire = 0.5F;
    private GameObject newProjectile;
    private float myTime = 0.0F;


    public int bullets;

    void Start() {
        bullets = 10;
    }


    void Update()
    {
        if (bullets > 0)
        {
            myTime = myTime + Time.deltaTime;

            if (Input.GetMouseButtonDown(1) && myTime > nextFire)
            {
                bang.Play();
                GameObject fat = GameObject.Find("Level");
                GameObject player = GameObject.Find("Character");

                nextFire = myTime + fireDelta;
                var prefab = bulletPrefab;
                var gridTransform = bulletSpawnPoint.position;

                newProjectile = Instantiate(prefab, gridTransform, Quaternion.identity, fat.transform) as GameObject;

                nextFire = nextFire - myTime;
                myTime = 0.0F;

                bullets = bullets - 1;

                Destroy(newProjectile, 4f);
                changeUIBUllet(-1);
            }
        }
        if(Input.GetKeyDown(KeyCode.M)){
            collectChest();
        }
    }

    public void collectChest() {
        this.bullets = bullets + 5;
        if(this.bullets > 13){
            changeUIBUllet(5 - (this.bullets - 13));
            this.bullets = 13;
            
        }else{
            changeUIBUllet(5);
        }
    }

    void changeUIBUllet(int i){
        if(i < 0){
            if(i == -1){
                UI.transform.Find("Bullets").Find("bullet1 (" + (bullets+14) + ")").gameObject.SetActive(false);
            }
        }else if(i > 0){
            for(int j = 0; j < i; j++){
                UI.transform.Find("Bullets").Find("bullet1 (" + (bullets + j + 9 ) + ")").gameObject.SetActive(true);
            }
        }
    }
}
