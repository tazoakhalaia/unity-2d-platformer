using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinText;
    public TextMeshProUGUI money;
    public void AddMoney()
    {
        coinText++;
        money.text = coinText.ToString();
    }
}
