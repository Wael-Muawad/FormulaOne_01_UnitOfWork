﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Entities.Dtos.Responses.Achievement
{
    public class DriverAchievementResponse
    {
        public Guid AchievementId { get; set; }
        public Guid DriverId { get; set; }
        public int Wins { get; set; }
        public int PolePosition { get; set; }
        public int FastestLap { get; set; }
        public int WorldChampionship { get; set; }
    }
}
