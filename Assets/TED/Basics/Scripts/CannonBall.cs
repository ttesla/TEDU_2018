using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float ExpForceAmount;
    public float ExpRadius;
    public GameObject ExpParticlePrefab;

    private Rigidbody mRigidBody;

    void Awake()
    {
        mRigidBody = GetComponent<Rigidbody>();
        mRigidBody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Carptigi her seyden degilde, sadece "Brick" leri etkilesin diye Brick tag'i ile de
        // kontrol ekledik. Tugla objelerine dikkat ederseniz, "Brick" tag'i var. 
        if (collision.contacts.Length > 0 && collision.gameObject.tag == "Brick")
        {
            Vector3 pos = collision.contacts[0].point;
            Collider[] colliders = Physics.OverlapSphere(pos, ExpRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                GameObject obj = colliders[i].gameObject;

                if (obj.tag == "Brick")
                {
                    Rigidbody brickRigidBody = obj.GetComponent<Rigidbody>();
                    brickRigidBody.AddExplosionForce(ExpForceAmount, pos, ExpRadius);
                }
            }

            // Her çarpmada bir patlama partickle yaratıyoruz, bu kısım da pool'ed yapılabilir
            // Patlama efektini top objesinin içine koymadık, çünkü top döndükçe, patlama efektini de döndürecektir.
            // Patlama partickle'ı bağımsız ayrı bir obje olmalıdır. 
            GameObject.Instantiate(ExpParticlePrefab, pos, Quaternion.identity);

            // Carpma aninda topu hemen disable et
            AutoDisable();
        }
    }

    public void Shoot()
    {
        Invoke("AutoDisable", 2.0f);
    }

    private void AutoDisable()
    {
        // IsKinematic true demek, bu rigidbody fizik kurallarının dışında tut demek.
        // Üzerine uygulanacak force ve velocity'den etkilenme demek. 
        // Bkz: https://docs.unity3d.com/ScriptReference/Rigidbody-isKinematic.html
        mRigidBody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
