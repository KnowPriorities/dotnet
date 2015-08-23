using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.v1.Engine.Inferred
{
    public class RangeComparer : BehavioralComparer
    {
        public RangeComparer(WeightScale scale) : base(scale)
        {
            if (scale.HighValue)
            {
                CompareInOrder = (x, y) => CompareRange(y, x);
                DefaultValue = decimal.MinValue;
            }
            else
            {
                CompareInOrder = CompareRange; // x, y are correct order
                DefaultValue = decimal.MaxValue;
            }

            DepreciationAmount = GetDepreciationAmount(scale);
        }

        private readonly decimal DefaultValue;
        private readonly decimal DepreciationAmount;
        public readonly ComparisonOrderDelegate CompareInOrder;

        public delegate int ComparisonOrderDelegate(Behavior x, Behavior y);
        

        public override int Compare(Behavior x, Behavior y)
        {
            var result = CompareInOrder(x, y);

            return result != 0 ? result : CompareAsHeat(x, y);
        }


        public int CompareRange(Behavior first, Behavior second)
        {
            var value1 = GetRangeValue(first);
            var value2 = GetRangeValue(second);

            return value1.CompareTo(value2);
        }

        public decimal GetRangeValue(Behavior behavior)
        {
            if (!behavior.RangeValue.HasValue)
                return DefaultValue;

            var result = behavior.RangeValue.Value.Depreciate(DepreciationAmount, DateTime.UtcNow, behavior.UpdatedAt);

            return result.HasValue ? result.Value : DefaultValue;
        }

        public static decimal GetDepreciationAmount(WeightScale scale)
        {
            if (!scale.Depreciation.HasValue)
                return 0m;

            var result = Math.Abs(scale.Depreciation.Value);

            // Low value is inverted depreciation
            return scale.HighValue ? result : (result * -1);
        }

    }
}
