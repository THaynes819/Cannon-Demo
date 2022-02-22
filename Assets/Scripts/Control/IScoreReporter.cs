using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Control
{
    public interface IScoreReporter
    {
        void ScorePoints(int score, int multiplier, int streak);
    }
}