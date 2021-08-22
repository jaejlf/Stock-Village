using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCtrl : MonoBehaviour
{
    private Camera cam; //���� ȭ�� ī�޶�
    private GameObject mouseOn; //�̺�Ʈ ���Ǻ� �߻��ϴ� ����ƮUI
    private Text symbol;
    private RaycastHit hit;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mouseOn = GameObject.Find("Canvas").transform.Find("mouseOn").gameObject;
        symbol = mouseOn.transform.GetChild(0).GetComponent<Text>();
        mouseOn.SetActive(false); //��ǳ�� off
    }

    //�ش� ������Ʈ ���� ���콺�� �ö󰡸� ��ǳ�� on
    private void OnMouseEnter()
    {
        if (GameObject.Find("InGameControl").GetComponent<pageCtrl>().popUp) { return; }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var wantedPos = cam.WorldToScreenPoint(transform.position);

        Physics.Raycast(ray, out hit); //���콺 Ŀ���� �ö� �ǹ� ������Ʈ

        symbol.text = hit.collider.gameObject.name; //��ǳ�� �ؽ�Ʈ ���� �̸����� ����
        mouseOn.transform.position = new Vector3(wantedPos.x + 50f, wantedPos.y + 100f, wantedPos.z); //��ǳ�� ��ġ ����
        mouseOn.SetActive(true);

    }

    //�ش� ������Ʈ ���� ���콺�� �ö󰡸� ��ǳ�� off
    private void OnMouseExit()
    {
        mouseOn.SetActive(false);
    }
}
