using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thanhMau : MonoBehaviour
{
    public Image _thanhMau;
    public void capNhatThanhMau(float mauHientai, float mauToiDa){
        _thanhMau.fillAmount = mauHientai / mauToiDa;
    }
}
