using NUnit.Framework;

namespace TestCase
{
    [TestFixture]
    public class IdWithType
    {
        [TestCase(Entity.IdType.Mission, 100000U)]
        public void Test(Entity.IdType type, uint id)
        {
            var idWithType = Entity.IdWithType.Create(type, id);
            Assert.AreEqual(type, idWithType.IdType);
            Assert.AreEqual(id, idWithType.Id);
        }
    }
}