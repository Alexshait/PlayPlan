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

        //[Test]
        //public void AuthorizationInfo_GetUrlAuth_Test()
        //{

        //    var Auth = new VkAuthorization(_setting);
        //    var actual = Auth.GetUrlAuth();
        //    var expected = "https://oauth.vk.com/authorize?client_id=8073115&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.131";
        //    Assert.AreEqual(expected, actual);
        //}

        [Test]
        public void AccessToken_Test()
        {
            var Auth = new VkAuthorization(_setting);
            Auth.ResponseAuthUrl = @"https://oauth.vk.com/blank.html#access_token=524077ab8c36b5058b99553018e88650a6ebf95a6e7ffe4d82502225e72f465c1812daf12bba57d38e23b&expires_in=86400&user_id=2476387";
            var expected = "524077ab8c36b5058b99553018e88650a6ebf95a6e7ffe4d82502225e72f465c1812daf12bba57d38e23b";
            var result = Auth.AccessToken;
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AccessTokenExpiration_Test()
        {
            var Auth = new VkAuthorization(_setting);
            Auth.ResponseAuthUrl = @"https://oauth.vk.com/blank.html#access_token=524077ab8c36b5058b99553018e88650a6ebf95a6e7ffe4d82502225e72f465c1812daf12bba57d38e23b&expires_in=86400&user_id=2476387";
            var expected = 86400;
            var result = Auth.AccessTokenExiration;
            Assert.AreEqual(expected, result);
        }

        //Person person = new Person()
        //{
        //ID = 1,
        //PersonName = "Пирогов Сергей"
        //};


    }
}