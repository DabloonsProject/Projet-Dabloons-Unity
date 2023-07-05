using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Entity
{
    public string nom;
    public int vie;
    public int mana;
    public int armure;
}

[System.Serializable]
public class Character : Entity
{
    public int vitesse;
}

[System.Serializable]
public class PNJ : Entity
{
    public string attribute_1;
    public int attribute_2;
}