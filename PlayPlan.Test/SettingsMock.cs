using PlayPlan.DataModel;
using System.Collections.Generic;

namespace PlayPlan.Test
{
    internal class SettingsMock : ISettings
    {
        public SettingsMock()
        {
        }

        public IEnumerable<string> GetAllTrailners()
        {
            return new List<string>()
            {
                "Бтрюков Алексей",
                "Пирогов Сергей"
            };
        }

        public int GetApiId()
        {
            return 8073115;
        }

        public int GetGroupID()
        {
            return 190120334;
        }

        public string GetVer()
        {
            return "v=5.131";
        }

        public void TrainerAddNew(Person person)
        {
            throw new System.NotImplementedException();
        }

        public void TrainerRemove(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}