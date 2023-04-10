using NUnit.Framework.Internal.Commands;
using NUnit.Framework.Internal.Execution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatusCache
{
    public int damage;
    public float speed;

    public WeaponStatusCache()
    {
        damage = 0;
        speed = 1;
    }
}

public class Weapon
{
    public string name; //무기 이름
    public List<TechTree> techTreeList; //무기 테크트리
    public WeaponStatusCache cache;

    public Weapon(string name, List<TechTree> techTreeList)
    {
        this.name = name;
        this.techTreeList = techTreeList;
        cache = new WeaponStatusCache();
    }

    public TechTree GetTech(string name)
    {
        foreach(TechTree tech in techTreeList)
        {
            if(tech.name == name)
            {
                return tech;
            }
        }
        return null;
    }

    public void ApplyChange(TechTree tech)
    {
        cache.damage += tech.damage;
        cache.speed *= tech.speed;
    }
}

public class TechTree
{
    public string name; // 테크트리 이름
    public int curlevel; // 현재 테크트리 레벨
    public int maxLevel; // 최대 테크트리 레벨
    public TechTree parent; // 부모 노드
    public List<TechTree> children; // 자식 노드

    //적용되는 수치들
    public int damage;
    public float speed;

    public TechTree(string name, int maxLevel, TechTree parent = null)
    {
        this.name = name;
        this.curlevel = 0;
        this.maxLevel = maxLevel;
        this.parent = parent;
        this.children = new List<TechTree>();

        this.damage = 0;
        this.speed = 1;
    }

    public void AddChild(TechTree child)
    {
        children.Add(child);
        child.parent = this;
    }
}

public class WeaponSystemManager : MonoBehaviour
{
    private static WeaponSystemManager instance;
    public static WeaponSystemManager Instance { get { return instance; } }

    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    public string currentWeaponName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddWeapon(string weaponName, Weapon weapon)
    {
        weapons.Add(weaponName, weapon);
    }

    public Weapon GetWeapon(string weaponName)
    {
        if(weapons.ContainsKey(weaponName))
        {
            return weapons[weaponName];
        }
        else
        {
            Debug.LogWarning("WeaponSystemManager: " + weaponName + "does not exist in weapons dictionary");
            return null;
        }
    }

    void Start()
    {
        TechTree swordTech1 = new TechTree("테크1", 1);
        TechTree swordsubTech1 = new TechTree("하위테크1", 1);
        TechTree swordsubTech2 = new TechTree("하위테크2", 1);
        TechTree swordsubTech3 = new TechTree("하위테크3", 1);
        TechTree swordsubsubTech1 = new TechTree("하위하위테크1", 1);
        TechTree swordsubsubTech2 = new TechTree("하위하위테크2", 1);

        swordTech1.AddChild(swordsubTech1);
        swordTech1.AddChild(swordsubTech2);
        swordTech1.AddChild(swordsubTech3);
        swordsubTech1.AddChild(swordsubsubTech1);
        swordsubTech1.AddChild(swordsubsubTech2);

        Weapon sword = new Weapon("검", new List<TechTree>() { swordTech1 });
        currentWeaponName = "검";
    }
}
