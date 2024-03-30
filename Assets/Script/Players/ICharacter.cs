using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    int Money { get; set; }         //プレイヤーの所持金をget,setするプロパティ
    int SkillLevel { get; set; }    //プレイヤーのスキルレベルをget,setするプロパティ
    int SkinId { get; set; }        //プレイヤーの使用するスキンをget,setするプロパティ
    void Dead();
}
