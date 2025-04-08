using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public float maxHP = 100;
    public float currentHP = 100;
    public float attackPower = 10;
    public float speed = 3;
    public float shieldPower = 5;

    public void ApplyCrewBonus(CrewMember crew)
    {
        maxHP += crew.hpBonus;
        attackPower += crew.attackBonus;
        speed += crew.speedBonus;
        shieldPower += crew.shieldBonus;
    }
}
