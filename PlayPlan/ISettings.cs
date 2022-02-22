using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal interface ISettings
    {
        int GetGroupID();
        int GetApiId();
        string GetVer();

        IEnumerable<string> GetAllTrailners();
        void TrainerAddNew(Person person);
        void TrainerRemove(Person person);
    }
}
