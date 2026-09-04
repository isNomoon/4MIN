using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class renwuguanli : MonoBehaviour
{
    // Start is called before the first frame update
    public static renwuguanli instance;
    public List<string> renwu;
    [SerializeField]
    private int renwucount;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string  Finish(string give)
    {
        renwucount++;
        string renwubiaoti=renwu[renwucount];
        return renwubiaoti;
    }
}
