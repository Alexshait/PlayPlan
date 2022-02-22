using NUnit.Framework;


namespace PlayPlan.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Authorize_Test()
        {
            var setting = new SettingsMock();
            var Auth = new Authorize(setting);
            Assert.Pass();
        }
    }
}