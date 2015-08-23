using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Inferred
{
    public abstract class BehavioralComparer : IComparer<Behavior>
    {
        protected BehavioralComparer(WeightScale scale)
        {
            Scale = scale;
        }

        public readonly WeightScale Scale;

        public static BehavioralComparer GetComparer(WeightScale scale)
        {
            return scale.Series.Count > 0 ? (BehavioralComparer)new SeriesComparer(scale) : new RangeComparer(scale);
        }

        public abstract int Compare(Behavior x, Behavior y);

        public static int CompareAsHeat(Behavior x, Behavior y)
        {
            var xValue = x.GetHeatValue();
            var yValue = y.GetHeatValue();

            var result = yValue.CompareTo(xValue);

            return result;
        }

    }
}
