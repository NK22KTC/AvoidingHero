using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    int Money { get; set; }         //�v���C���[�̏�������get,set����v���p�e�B
    int SkillLevel { get; set; }    //�v���C���[�̃X�L�����x����get,set����v���p�e�B
    int SkinId { get; set; }        //�v���C���[�̎g�p����X�L����get,set����v���p�e�B
    void Dead();
}
