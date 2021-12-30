using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fearandgreed : MonoBehaviour
{
    public StockList stockList;
    public Slider fearAndGreedIndex;//������ Ž�� ���� 
    public Image stateImg; //���º� ������ �̹���
    public Image fill; //������ ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkIndex();
    }

    void checkIndex()
    {
        if (stockList.fearAndGreed < 50)
        {
            stateImg.sprite = Resources.Load("Prefabs/emoji/fearIcon", typeof(Sprite)) as Sprite;
            fill.sprite = Resources.Load("Prefabs/emoji/fear", typeof(Sprite)) as Sprite;
        }
        else
        {
            stateImg.sprite = Resources.Load("Prefabs/emoji/greedIcon", typeof(Sprite)) as Sprite;
            fill.sprite = Resources.Load("Prefabs/emoji/greed", typeof(Sprite)) as Sprite;
        }
        fearAndGreedIndex.value = stockList.fearAndGreed;
    }
}
