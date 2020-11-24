using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipLoader : MonoBehaviour
{
    #region fields
    private Chip _White;
    private Chip _Black;
    #endregion

    public void Init()
    {
        LoadFromResources();
    }

    //загружаем префабы фишек из ресурсов
    void LoadFromResources()
    {
        _White = Resources.Load<Chip>("_prefabs/ChipWhite");
        _Black = Resources.Load<Chip>("_prefabs/ChipBlack");
    }

    public List<Chip> GetListOfWhiteChips(int numb)
    {
        return GetListOfChips(numb, _White);
    }

    public List<Chip> GetListOfBlackChips(int numb)
    {
        return GetListOfChips(numb, _Black);
    }

    //создаем нужное кол-во нужных фишек
    List<Chip> GetListOfChips(int numb, Chip chip)
    {
        List<Chip> chipList = new List<Chip>();
        for (int i = 0; i < numb; i++)
        {
            Chip newChip = Instantiate(chip);
            chipList.Add(newChip);
        }
        return chipList;
    }
}
