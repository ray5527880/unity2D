using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public UI MainUI;
    [SerializeField]
    public UI PlayHPUI;
    [SerializeField]
    public UI PlayMPUI;
    [SerializeField]
    public UI MonsterUI;

    public void SetPlayHPMax(float MaxHP)
    {
        PlayHPUI.MaxValue = MaxHP;
    }
    public void SetPlayHPNow(float damage)
    {
        PlayHPUI.MyCurenValue -=  damage;
    }public void SetPlayMPMax(float MaxMP)
    {
        PlayMPUI.MaxValue = MaxMP;
    }
    public void SetPlayMPNow(float damage)
    {
        PlayMPUI.MyCurenValue -=  damage;
    }
    public void SetMonsterMax(float MaxHP)
    {
        MonsterUI.MaxValue = MaxHP;
    }
    public void SetMonsterNow(float damage)
    {
        MonsterUI.MyCurenValue -=  damage;
    }
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
