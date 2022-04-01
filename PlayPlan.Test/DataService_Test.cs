using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayPlan.Test
{
    public class DataService_Test
    {
        private IDataService _dataService;
        [SetUp]
        public void Setup()
        {
            _dataService = new SettingsMock();
        }
        [Test]
        public void GetAuthUrl_Test()
        {
            var Auth = _dataService.GetAuthUrl();
            var expected = @"https://oauth.vk.com/access_token?client_id=8073115&client_secret=0gx7Of99iqrtqPLwQmUA&redirect_uri=http://1gb.ru&code=558e4a22e5ac353dcc";
            Assert.AreEqual(expected, Auth);
        }
    }
}
