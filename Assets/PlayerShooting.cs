using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] ParticleSystem RifleparticleSystem;
    private ParticleSystem.EmissionModule em;
    private NetworkVariableBool shooting = new NetworkVariableBool(new NetworkVariableSettings {WritePermission = NetworkVariablePermission.OwnerOnly },false);
    float fireRate = 10f;
    float shootTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        em = RifleparticleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            shooting.Value = Input.GetMouseButton(0);
            shootTime += Time.deltaTime;

            if(shooting.Value && shootTime >= fireRate)
            {
                shootTime = 0f;
                // call shoot method

                ShootServerRpc();
            }

        }
        em.rateOverTime = shooting.Value ? fireRate : 0f;
    }
    [ServerRpc]
    void ShootServerRpc()
    {
        Ray ray = new Ray(RifleparticleSystem.transform.position, RifleparticleSystem.transform.forward);

        Debug.Log("Player is shooting!");

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            // hit player
            var player = hit.collider.GetComponent<Health>();
            Debug.Log(player.health.Value);
            if (player != null)
            {
                player.TakeDamageServerRpc(10f);
            }
        }
    }
}
