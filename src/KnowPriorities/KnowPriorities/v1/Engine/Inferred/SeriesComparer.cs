using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Inferred
{
    public class SeriesComparer : BehavioralComparer
    {
        public SeriesComparer(WeightScale scale) : base(scale)
        {
            Series = scale.Series;
            NotFoundIndex = scale.Series.Count + 1;

            DepreciationAmount = GetDepreciationAmount(scale);
        }

        public readonly int NotFoundIndex;
        private readonly List<string> Series;
        private readonly int DepreciationAmount;

        public override int Compare(Behavior x, Behavior y)
        {
            var xIndex = DepreciatedIndex(x);
            var yIndex = DepreciatedIndex(y);

            //Lower values are valued higher based upon left-to-right values being entered into the series
            return xIndex == yIndex ? CompareAsHeat(x, y) : xIndex.CompareTo(yIndex);
        }

        public static int GetDepreciationAmount(WeightScale scale)
        {
            if (!scale.Depreciation.HasValue)
                return 0;

            var amount = (int)Math.Abs(scale.Depreciation.Value);

            // Depreciation is inverted for series
            return amount < scale.Series.Count ? amount * -1 : 0;
        }


        public int DepreciatedIndex(Behavior behavior)
        {
            var baseIndex = Series.IndexOf(behavior.SeriesValue);

            if (baseIndex < 0)
                return NotFoundIndex;

            var index = baseIndex.Depreciate(DepreciationAmount, DateTime.UtcNow, behavior.UpdatedAt);

            return index > NotFoundIndex ? NotFoundIndex : index;
        }

    }
}
