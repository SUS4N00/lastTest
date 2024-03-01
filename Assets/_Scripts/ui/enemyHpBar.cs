using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHpBar : MonoBehaviour
{
    public Image _thanhMau;
    public void capNhatThanhMau(float mauHientai, float mauToiDa, bool isFacingRight){
        if(isFacingRight){
            _thanhMau.fillOrigin = 1;
        }else{
            _thanhMau.fillOrigin = 0;
        }
        _thanhMau.fillAmount = mauHientai / mauToiDa;
    }
}
