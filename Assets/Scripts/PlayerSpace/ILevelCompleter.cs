using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PlayerSpace
{
    public interface ILevelCompleter
    {
        void HalfWayBonus();
        void CompleteLevel();
        void StageLevel();
    }
}