using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{
    public List<GameObject> CannonBalls;

    public GameObject CannonBallPrefab;
    public float ForceAmount;
    public Transform TurretPointTrans;
    public float RotSpeed;

    private int mBallIndex = 0;
    private bool mShoot = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mShoot = true;
        }

        // Delta time ve speed carpanları horizontal ve vertical degiskenlerine en bastan uygulanıyor
        var horizontalVal = Input.GetAxis("Horizontal") * Time.deltaTime * RotSpeed;
        var verticalVal   = Input.GetAxis("Vertical")   * Time.deltaTime * RotSpeed;

        // Euler angles rotasyon
        //var rot = transform.rotation.eulerAngles;
        //rot += new Vector3(-verticalVal, horizontalVal, 0);
        //transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        // Unity'nin rotate fonksiyonları ile rotasyon
        transform.Rotate(Vector3.up, horizontalVal);
        transform.Rotate(Vector3.left, verticalVal);
    }

    void FixedUpdate()
    {
        // Pool kullanmadan, her seferinde yeni top yaratan kod blogu
        //if (mShoot)
        //{
        //    GameObject cannonBall = (GameObject)GameObject.Instantiate(CannonBallPrefab, TurretPointTrans.position, Quaternion.identity);
        //    Rigidbody rigidBody   = cannonBall.GetComponent<Rigidbody>();
        //    rigidBody.AddForce(TurretPointTrans.forward * ForceAmount, ForceMode.Impulse);
        //    mShoot = false;
        //}

        // Pool kullanan, daha onceden hiyerarşide bululan topları kullanan kod bloğu
        if (mShoot)
        {
            if (mBallIndex >= CannonBalls.Count)
                mBallIndex = 0;

            GameObject cannonBall = CannonBalls[mBallIndex];
            cannonBall.transform.position = TurretPointTrans.position;
            cannonBall.SetActive(true);
            cannonBall.GetComponent<CannonBall>().Shoot();

            mBallIndex++;

            Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();

            // Kinematic kısmı önemli, hep aynı topları kullandığımız için, topların üzerinde bir önceki atıştan kalan
            // hız ve eylemsizlik değerleri kalıyor, atmadan önce bunları sıfırlamamız gerek.
            // Veya kaybolan toplar kinematik'lerini "true" yapar, yeni atılanlar da tekrar "false" yaparak benzer bir 
            // çözüm yapabiliriz, burda ikinci çözüm uygulanıyor. CannonBall.cs scriptine de göz atın.
            rigidBody.isKinematic = false;
            rigidBody.AddForce(TurretPointTrans.forward * ForceAmount, ForceMode.Impulse);
            mShoot = false;
        }
    }
}
