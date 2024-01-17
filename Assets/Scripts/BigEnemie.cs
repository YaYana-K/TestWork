using UnityEngine;

public class BigEnemie : Enemie
{
    [SerializeField] GameObject enemiePrefab;
    [SerializeField] int enemieCount = 2;

    private void OnEnable()
    {
        Enemie.OnDeath += OnEnemieDeath;
    }
    private void OnDisable()
    {
        Enemie.OnDeath -= OnEnemieDeath;
    }
    private void OnEnemieDeath(Enemie enemie)
    {
        if (enemie == this)
        {
            for (int i = 0; i < enemieCount; i++)
            {
                Vector3 pos = new Vector3(transform.position.x + Random.Range(0, 4), 0, transform.position.y + Random.Range(0, 4));
                Instantiate(enemiePrefab, pos, Quaternion.identity);
                Invoke("DestroyObj", 2);
            }
        }
    }
}
