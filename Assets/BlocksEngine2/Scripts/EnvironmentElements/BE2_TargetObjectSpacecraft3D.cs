using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.VFX;

namespace MG_BlocksEngine2.Environment
{
    public class BE2_TargetObjectSpacecraft3D : BE2_TargetObject
    {
        GameObject _bullet;

        public new Transform Transform => transform;
        public new string Name => name;
        // public VisualEffect _bulletExplosion;

        void Awake()
        {
            name = this.gameObject.name;
    
            // v2.6 - changed way to find "bullet" child of Target Object
            foreach (Transform child in transform)
            {
                // Debug.Log(child.name);
                if (child.name == "Bullet")
                    _bullet = child.gameObject;
            }

        }

        //void Start()
        //{
        //
        //}

        //void Update()
        //{
        //
        //}

        public void Shoot()
        {
            GameObject newBullet = Instantiate(_bullet, _bullet.transform.position, Quaternion.identity);
            newBullet.SetActive(true);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
            newBullet.transform.rotation = transform.rotation;
            // newBullet.GetComponent<BulletAlign>().Align();
            // _bulletExplosion.Play();
            // StartCoroutine(C_DestroyTime(newBullet));
            // StartCoroutine(AlignAgain(newBullet));
        }
        IEnumerator C_DestroyTime(GameObject go)
        {
            yield return new WaitForSeconds(1f);
            Destroy(go);
        }

        // IEnumerator AlignAgain(GameObject newBullet)
        // {
        //     yield return new WaitForSeconds(0.2f);
        //     newBullet.GetComponent<BulletAlign>().Align();
        // }
    }
}