using PlayPlan.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan
{
    internal class Settings : ISettings
    {
        public IEnumerable<string> GetAllTrailners()
        {
            throw new NotImplementedException();
        }

        public int GetApiId()
        {
            throw new NotImplementedException();
        }

        public int GetGroupID()
        {
            throw new NotImplementedException();
        }

        public string GetVer()
        {
            throw new NotImplementedException();
        }

        public void TrainerAddNew(Person person)
        {
            throw new NotImplementedException();
        }

        public void TrainerRemove(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
