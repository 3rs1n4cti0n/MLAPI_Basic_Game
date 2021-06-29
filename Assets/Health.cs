using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Health : NetworkBehaviour
{
    public NetworkVariableFloat health = new NetworkVariableFloat(100f);
    public const float maxHealth = 100f;
    MeshRenderer[] renderers;
    CharacterController controller;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    // running in server
    [ServerRpc]
    public void TakeDamageServerRpc(float damage)
    {
        health.Value -= damage;

        health.Value = Mathf.Clamp(health.Value, 0f, 100f);

        Debug.Log(health.Value);
        // check health
        if(health.Value <= 0)
        {
            health.Value = maxHealth;
            // respawn
            RespawnClientRpc(new Vector3(0,0,0));
        }
    }

    [ClientRpc]
    void RespawnClientRpc(Vector3 pos)
    {
        StartCoroutine(Respawn(pos));
    }

    IEnumerator Respawn(Vector3 pos)
    {
        foreach(var rend in renderers)
        {
            rend.enabled = false;
        }
        yield return new WaitForSeconds(2f);
        controller.enabled = false;
        transform.position = pos;
        controller.enabled = true;

        foreach (var rend in renderers)
        {
            rend.enabled = true;
        }
    }
}
