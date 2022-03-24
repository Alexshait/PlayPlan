using NUnit.Framework;


namespace PlayPlan.Test
{
    public class Tests
    {
        private IDataService _setting;
        [SetUp]
        public void Setup()
        {
            _setting = new SettingsMock();
        }

        [Test]
        public void AuthorizationInfo_GetUrlAuth_Test()
        {
            
            var Auth = new AuthorizationInfo(_setting);
            var actual = Auth.GetUrlAuth();
            var expected = "https://oauth.vk.com/authorize?client_id=8073115&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.131";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AuthorizationInfo_GetAccessToken_Test()
        {
            var Auth = new AuthorizationInfo(_setting);
            var actual = Auth.GetAccessToken();
            var expectedLenth = 85;
            Assert.AreEqual(expectedLenth, actual.ToString().Length);
        }

        //Person person = new Person()
        //{
        //ID = 1,
        //PersonName = "Пирогов Сергей"
        //};


    }
}