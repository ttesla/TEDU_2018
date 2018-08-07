using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Fireball,
    Pistol
}

public class Player : MonoBehaviour
{
    public Camera MainCam;
    public GameObject CannonBallPrefab;
    public float ForceAmount;
    public Transform FireBallMuzzleTrans;
    public GameObject DecalPrefab;
    public GameObject BloodPrefab;
    public WeaponType CurrentWeaponType;
    public Animator GunAnimController;
    public LayerMask HouseLayer;

    private bool mFireBallShoot = false;
    private Vector3 mTargetDirection;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            mTargetDirection = ray.direction;

            if (CurrentWeaponType == WeaponType.Fireball)
            {
                mFireBallShoot = true;
            }
            else if (CurrentWeaponType == WeaponType.Pistol)
            {
                ShootGun(ray);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeaponType = WeaponType.Pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeaponType = WeaponType.Fireball;
        }
    }

    private void FixedUpdate()
    {
        // Not pooled
        if (mFireBallShoot)
        {
            ShootFireBall();
            mFireBallShoot = false;
        }
    }

    private void ShootFireBall()
    {
        GameObject cannonBall = (GameObject)GameObject.Instantiate(CannonBallPrefab, FireBallMuzzleTrans.position, Quaternion.identity);
        Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();
        rigidBody.AddForce(mTargetDirection * ForceAmount, ForceMode.Force);
    }

    private void ShootGun(Ray ray)
    {
        GunAnimController.SetTrigger("Shoot");

        // House Layer 9 olduğu için 9 bit sola kaydırma yapıyoruz
        // yada bunun yerine doğrudan Layermask değişkeni kullanabilirsiniz
        // Böylelikle editörden hangi raycast hangi layerlara çarpsın ayarlayabilirsiniz
        //int layerMask = (1 << 9);

        RaycastHit rayHitInfo;
        if (Physics.Raycast(ray, out rayHitInfo, 1000, HouseLayer.value))
        {
            if(rayHitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                var obj = GameObject.Instantiate(DecalPrefab, rayHitInfo.point, Quaternion.identity);

                // Decal'in yönünü ayarla, Normal vektörü, çarmanın olduğu vertex'ten dışarı doğru olan dik vektördür
                obj.transform.rotation = Quaternion.LookRotation(rayHitInfo.normal);
            }
            else if (rayHitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Organic"))
            {
                var obj = GameObject.Instantiate(BloodPrefab, rayHitInfo.point, Quaternion.identity);

                // Decal'in yönünü ayarla, Normal vektörü, çarmanın olduğu vertex'ten dışarı doğru olan dik vektördür
                obj.transform.rotation = Quaternion.LookRotation(rayHitInfo.normal);
            }
        }
    }
}
