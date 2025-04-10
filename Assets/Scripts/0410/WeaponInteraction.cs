using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    public LayerMask tableLayer;
    private PlayerAttack player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            Collider[] tables = Physics.OverlapSphere(transform.position, 1.5f, tableLayer);

            foreach (var table in tables)
            {
                WeaponTable temp = table.GetComponent<WeaponTable>();

                if (temp != null && player != null)
                {
                    player.NewWeapon(temp.GetWeapon());
                }
            }
        }
    }
}
