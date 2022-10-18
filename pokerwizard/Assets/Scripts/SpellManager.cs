using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[ExecuteInEditMode]
public class SpellManager : MonoBehaviour
{
    public bool FetchDataInEditor;
    public List<SpellData> allSpell;
    private List<SpellData> tempSpell;
    void Update()
    {
        UpdateList();
    }
    void UpdateList(){
        if(FetchDataInEditor){
            tempSpell.Clear();
            string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Scripts/Spell" });

            foreach (string spellname in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(spellname);
                var spell = AssetDatabase.LoadAssetAtPath<SpellData>(SOpath);
                tempSpell.Add(spell);
            }
            allSpell.Clear();
            for(int i=0;i<tempSpell.Count;i++){
                if(tempSpell[i].ID==0&&tempSpell[i].SpellName!="Flame"){
                    tempSpell[i].ID=tempSpell.Count;
                }
            }
            allSpell=tempSpell.OrderBy(s => s.ID).ToList();
        }
    }
}