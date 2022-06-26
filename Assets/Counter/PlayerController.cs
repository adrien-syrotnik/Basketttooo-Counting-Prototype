using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float timeReload = 2f;
    [SerializeField]
    private GameObject basketballPrefab;
    [SerializeField]
    private Vector3 spawnBasketballOffset;
    public bool canShoot;

    private Rigidbody currentBall;

    public UnityEvent onShoot =  new UnityEvent();

    public bool canPlay;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        canPlay = true;
        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && canShoot && canPlay)
        {
            shootBall();
            StartCoroutine(reload());
        }
    }


    private void spawnBall()
    {
        if(canPlay)
        {
            GameObject basketball = Instantiate(basketballPrefab, transform.position + spawnBasketballOffset, basketballPrefab.transform.rotation);
            currentBall = basketball.GetComponent<Rigidbody>();
            currentBall.useGravity = false;
            canShoot = true;
        }
        
    }

    private void shootBall()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.Play();
        onShoot.Invoke();
        //Get mouse Ray on screen into viewPort
        Ray mouseRay = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //Create direction from mousePosition

        //control force to not be to strong
        Vector3 direction = mouseRay.direction * 20 + new Vector3(0 , 10, 0);
        canShoot = false;
        currentBall.useGravity = true;
        currentBall.AddForce(direction, ForceMode.Impulse);
    }

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(timeReload);
        spawnBall();
    }
}
