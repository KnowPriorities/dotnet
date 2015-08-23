using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowPriorities.v1.Models;

namespace KnowPriorities.Tests.v1
{
    public static class Simulate
    {

        public static Subject Tiny_Set_Of_25_Stakeholders()
        {
            return Subject(1, 25);
        }

        public static Subject Small_Set_Of_250_Stakeholders()
        {
            return Subject(5, 50);
        }

        public static Subject Medium_Set_of_2k_Stakeholders()
        {
            return Subject(4, 500);
        }

        public static Subject Large_Set_Of_20k_Stakeholders()
        {
            return Subject(4, 5000);
        }

        public static Subject Extreme_Set_Of_1m_Stakeholders()
        {
            return Subject(10, 100000);
        }



        public static Subject Subject(int groupCount, int stakeholderCount)
        {
            var percentage = 1m / groupCount;
            
            var subject = new Subject();

            for (var g = 0; g < groupCount; g++)
            {
                var group = new Group() {Id = g.ToString(), Percentage = percentage};

                for (var s = 0; s < stakeholderCount; s++)
                {
                    var stakeholder = new Stakeholder();

                    for (var x = 0; x < 25; x++)
                    {
                        stakeholder.Priorities.Add((g + s + x).ToString());
                    }

                    group.Stakeholders.Add(stakeholder);
                }

                subject.Groups.Add(group);
            }

            return subject;
        }




    }
}
