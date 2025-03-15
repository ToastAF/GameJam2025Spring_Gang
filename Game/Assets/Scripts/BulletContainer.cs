using UnityEngine;

[System.Serializable]
public class BulletContainer
{
    public GameObject bullet;
    public Sprite UIBullet;

    public BulletContainer(GameObject _bullet, Sprite _UIBullet)
    {
        bullet = _bullet;
        UIBullet = _UIBullet;
    }
}
