using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shootParticles;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f; 
    public float fireInterval = 0.1f;
    public float reloadTime = 2f; 
    public int clipSize = 30; 
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public TMP_Text ammoText;
    public float spreadAngle = 5f;

    private int currentAmmo; 
    private float fireCooldown = 0f;
    private bool isReloading = false;
    private AudioSource source; 

    private void Start()
    {
        currentAmmo = clipSize;
        source = GetComponent<AudioSource>();
        UpdateAmmoText();
    }

    private void Update()
    {
        if (!isReloading)
        {
            UpdateAmmoText();
        }

        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireCooldown <= 0 && !isReloading)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < clipSize && !isReloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            source.PlayOneShot(shootSound);
            Instantiate(shootParticles, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Vector3 spread = new Vector3(
                Random.Range(-spreadAngle, spreadAngle),
                Random.Range(-spreadAngle, spreadAngle),
                0);
            Quaternion spreadRotation = Quaternion.Euler(spread) * bulletSpawnPoint.rotation;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, spreadRotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            fireCooldown = fireInterval;
            currentAmmo--;
        }
        else
        {
            Reload();
        }
    }

    private async void Reload()
    {
        source.PlayOneShot(reloadSound);
        ammoText.text = "...";
        isReloading = true;
        Debug.Log("Reloading...");
        await new WaitForSeconds(reloadTime);
        currentAmmo = clipSize;
        Debug.Log("Reloaded!");
        isReloading = false;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ammoText.text = currentAmmo.ToString() + "/" + clipSize.ToString();
    }
}
