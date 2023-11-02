
using System.Collections.Generic;
using UnityEngine;

namespace Corners
{

    public class ChipLoader
    {
        private Chip whiteChipPrefab;
        private Chip blackChipPrefab;

        private Transform chipRoot;
        public ChipLoader(Transform chipRoot)
        {
            this.chipRoot = chipRoot;
            LoadFromResources();
        }

        //загружаем префабы фишек из ресурсов
        void LoadFromResources()
        {
            whiteChipPrefab = Resources.Load<Chip>("_prefabs/ChipWhite");
            blackChipPrefab = Resources.Load<Chip>("_prefabs/ChipBlack");
        }

        public List<Chip> CreateListOfWhiteChips(int numb)
        {
            return CreateListOfChips(numb, whiteChipPrefab);
        }

        public List<Chip> CreateListOfBlackChips(int numb)
        {
            return CreateListOfChips(numb, blackChipPrefab);
        }


        private List<Chip> CreateListOfChips(int numb, Chip chip)
        {
            List<Chip> chipList = new List<Chip>();
            for (int i = 0; i < numb; i++)
            {
                Chip newChip = GameObject.Instantiate(chip, chipRoot);
                chipList.Add(newChip);
            }
            return chipList;
        }
    }

}
