using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fractal :ScriptableObject
{
    public string name;
    public string start;
    public List<Rule> rules;
    public float turnRate;
   
    public void init(string n, string s, List<Rule> r, float t)
    {
        name = n;
        start= s;
        rules = r;
        turnRate = t;
       
    }
}

