using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class skillsinfo : MonoBehaviour
{
    public TextAsset skillsinfotext;

    public static skillsinfo _instance;

    private Dictionary<int, SillInfo> skillinfodict = new Dictionary<int, SillInfo>();

    void Awake()
    {
        _instance = this;
        initSkillinfodict();//初始化字典
    }

    public SillInfo GetSkillInfoById(int id)
    {
        SillInfo info = null;
        skillinfodict.TryGetValue(id,out info);
        return info;
    }

    void initSkillinfodict()
    {
        string text = skillsinfotext.text;
        string[] skillinfoArray = text.Split('\n');
        foreach (string skillinfoStr in skillinfoArray)
        {
            string[] pa = skillinfoStr.Split(',');
            SillInfo info = new SillInfo();
            info.id = int.Parse(pa[0]);
            info.name = pa[1];
            info.icon_name = pa[2];
            info.des = pa[3];
            string str_applytype = pa[4];
            switch (str_applytype)
            {
                case "Passive":
                    info.applytype = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applytype = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applytype = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applytype = ApplyType.MultiTarget;
                    break;
            }
            string str_applypro = pa[5];
            switch (str_applypro)
            {
                case "Attack":
                    info.applyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    info.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.MP;
                    break;
            }
            info.applyValue = int.Parse(pa[6]);
            info.applyTime = int.Parse(pa[7]);
            info.mp = int.Parse(pa[8]);
            info.coldTime = int.Parse(pa[9]);
            switch (pa[10])
            {
                case "Swordman":
                    info.applicableRole = ApplicableRole.Swordman;
                    break;
                case "Magician":
                    info.applicableRole = ApplicableRole.Magician;
                    break;
            }
            info.level = int.Parse(pa[11]);
            switch (pa[12])
            {
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;
            }
            info.distance = float.Parse(pa[13]);
            info.efx_name = pa[14];
            info.aniname = pa[15];
            info.anitime = float.Parse(pa[16]);
            skillinfodict.Add(info.id, info);
        }
    }
}


//适用角色
public enum ApplicableRole
{
    Swordman,
    Magician
}

public enum ApplyType
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}

public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

public enum ReleaseType
{
    Self,
    Enemy,
    Position
}

public class SillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applytype;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    public string efx_name;
    public string aniname;
    public float anitime = 0;
}