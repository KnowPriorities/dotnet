using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Engine.Pipeline;
using KnowPriorities.v1.Models;
using Xunit;

namespace KnowPriorities.Tests.v1.Stages.Preparation.Steps
{
    public class AgeStakeholderVolume_Tests : PrioritizingHandlerTests
    {

        private readonly AgeStakeholderVolume Step = new AgeStakeholderVolume();

        [Fact]
        public void Attempts_To_Update_All_Stakeholders()
        {
            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(-1);
            _stakeholder2.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge * -100);

            Step.Process(_request);

            Assert.Equal(Stakeholder.Default_Volume, _stakeholder1.Volume);
            Assert.Equal(0, _stakeholder2.Volume);
        }


        [Fact]
        public void If_Offset_Results_In_Volume_Less_Than_0_Then_Volume_Is_Set_To_0()
        {
            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge * -100);

            Step.Process(_stakeholder1, _request);

            Assert.Equal(0, _stakeholder1.Volume);
        }


        [Fact]
        public void If_Stakeholder_Has_Not_Updated_Over_DaysToAge_Then_Volume_Is_Reduced()
        {
            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge * -1);

            Step.Process(_stakeholder1, _request);

            Assert.Equal(Stakeholder.Default_Volume - 1, _stakeholder1.Volume);
        }


        [Fact]
        public void If_Stakeholder_Recently_Updated_Their_Volume_Is_Not_Reduced()
        {
            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge + 1);

            Step.Process(_stakeholder1, _request);

            Assert.Equal(Stakeholder.Default_Volume, _stakeholder1.Volume);
        }


        [Fact]
        public void GetVolumeOffset_Returns_1_Per_DaysToAge()
        {
            const int count = 10;

            _stakeholder1.UpdatedAt = _subject.AsOf.AddDays(_subject.DaysToAge * count * -1);

            var result = Step.GetVolumeOffset(_stakeholder1, _request);

            Assert.Equal(count, result);
        }


    }
}
