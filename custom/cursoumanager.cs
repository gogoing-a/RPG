using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursoumanager : MonoBehaviour
{
    public static cursoumanager _instance;

    public Texture2D cursor_narmal;
    public Texture2D cursor_npc_talk;
    public Texture2D cuisor_attack;
    public Texture2D cusor_locktarget;
    public Texture2D cursor_pick;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    private void Start()
    {
        _instance = this;
    }

    public void SetNormal()
    {
        Cursor.SetCursor(cursor_narmal, hotspot, mode);
    }

    public void SetNpcTalk()
    {
        Cursor.SetCursor(cursor_npc_talk, hotspot,mode);
    }

    public void SatAttack()
    {
        Cursor.SetCursor(cuisor_attack, hotspot,mode);
    }

    public void SetLockTarget()
    {
        Cursor.SetCursor(cusor_locktarget, hotspot, mode);
    }
}
