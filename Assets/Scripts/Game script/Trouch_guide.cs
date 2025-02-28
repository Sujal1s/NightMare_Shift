using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trouch_guide : MonoBehaviour
{
    [SerializeField] private RealmShift rs;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (rs != null && rs.isRealmShifted)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
