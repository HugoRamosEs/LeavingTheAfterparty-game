using System.Collections;
using UnityEngine;

/// <summary>
/// This class is responsible for the player's shooting attack.
/// </summary>
public class PlayerAttackShooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    private Animator anim;
    public bool isShooting = false;

    public ItemPanel itemPanel;
    public ToolBar toolbar;
    public string attackItem;

    public GameObject puntoRotacionDisparo;

    /// <summary>
    /// This method is used to initialize some variables.
    /// </summary>
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        anim = GetComponent<Animator>();
        puntoRotacionDisparo.SetActive(false);
    }

    /// <summary>
    /// This method is used to update the player's shooting attack.
    /// </summary>
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        bool isSlingshotSelected = false;

        for (int i = 0; i < itemPanel.inventory.slots.Count; i++)
        {
            ItemSlot slot = itemPanel.inventory.slots[i];
            if (i == toolbar.selectedTool && slot.item != null && slot.item.Name == attackItem)
            {
                isSlingshotSelected = true;

                Vector3 rotation = mousePos - puntoRotacionDisparo.transform.position;
                float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                puntoRotacionDisparo.transform.rotation = Quaternion.Euler(0, 0, rotZ);

                if (Input.GetMouseButtonDown(0) && canFire)
                {
                    canFire = false;
                    isShooting = true;
                    anim.SetBool("isShooting", true);
                    StartCoroutine(ShootBullet());
                }
            }
        }

        puntoRotacionDisparo.SetActive(isSlingshotSelected);
    }
    /// <summary>
    /// This method is used to instantiate the bullet and stop the shooting animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootBullet()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(bullet, bulletTransform.position, puntoRotacionDisparo.transform.rotation);
        StartCoroutine(StopShootingAnimation());
    }

    /// <summary>
    /// This method is used to stop the shooting animation.
    /// </summary>
    /// <returns></returns>
    IEnumerator StopShootingAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isShooting", false);
        isShooting = false;
    }
}