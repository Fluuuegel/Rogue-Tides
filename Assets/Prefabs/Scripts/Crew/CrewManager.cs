using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CrewManager : MonoBehaviour
{
    public List<CrewMember> crewList = new List<CrewMember>();
    public PlayerStats playerStats;

    public void AddCrew(CrewMember crew)
    {
        crewList.Add(crew);
        playerStats.ApplyCrewBonus(crew);
    }
}
