using System.Collections.Generic;
using System.Linq;
using Constants;
using StaticData.Data;
using UnityEngine;


namespace StaticData.Services
{
    public class StaticDataService
    {
        public PlayerStaticData PlayerStaticData { get; private set; }


        public void Init()
        {
            LoadStaticData();
        }


        private void LoadStaticData()
        {
            LoadPlayerStaticData();
        }


        private void LoadPlayerStaticData()
        {
            PlayerStaticData = Resources.Load<PlayerStaticData>(RuntimeConstants.StaticDataPaths.PLAYER_STATIC_DATA);
        }
    }
}
