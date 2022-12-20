using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private Vector3 yellowSpawnPos = new Vector3(1,-4.5f,0);
    public GameObject yellowBulletPrefab;
    private Coroutine yellowShooter;
    
    private Vector3 redSpawnPos = new Vector3(-1,-4.5f,0);
    public GameObject redBulletPrefab;
    private Coroutine redShooter;

    //이런식으로 코루틴 변수에 코루틴 함수 자체를 넣고, 끌 수 있다.
    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)){
            yellowShooter = StartCoroutine(ShootYellow());
        }
        if(Input.GetKeyUp(KeyCode.A)){
            StopCoroutine(yellowShooter);
        }

        if(Input.GetKeyDown(KeyCode.D)){
            redShooter = StartCoroutine(ShootRed());
        }
        if(Input.GetKeyUp(KeyCode.D)){
            StopCoroutine(redShooter);
        }
    }

    private IEnumerator ShootYellow(){
        while(true){
            // Instantiate(yellowBulletPrefab, yellowSpawnPos, Quaternion.identity);
            Pooler.Spawn(yellowBulletPrefab, yellowSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShootRed(){
        while(true){
            // Instantiate(redBulletPrefab, redSpawnPos, Quaternion.identity);
            Pooler.Spawn(redBulletPrefab, redSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
