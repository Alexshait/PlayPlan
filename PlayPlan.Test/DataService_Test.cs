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
        public void GetAllPersons_Test()
        {
            var Auth = new AuthorizationInfo(_dataService);
            var actual = Auth.GetAccessToken();
            var expectedLenth = 85;
            Assert.AreEqual(expectedLenth, actual.ToString().Length);
        }
    }
}
