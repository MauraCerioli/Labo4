using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MacroExpansionNS;
using static MacroExpansionNS.MacroExpansionClass;

namespace MacroExpansionTests{
    [TestFixture]
    public class MacroExpansionTestClass{
        [Test]
        public void NullSequenceThrowsANE(){
            Assert.That(() => MacroExpansion(null, new[]{1, 2}, 6).ToArray(),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void NullNewValuesThrowsANE(){
            Assert.That(() => MacroExpansion(new[]{1, 2}, null, 6),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void ReplaceHeadOnceOk(){
            var result = MacroExpansion(new[]{"pippo", "pluto"}, new[]{"paperino"}, "pippo");
            var expected = new[]{"paperino", "pluto"};
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReplaceTailOnceOk(){
            var result = MacroExpansion(new[]{"pippo", "pluto"}, new[]{"paperino"}, "pluto");
            var expected = new[]{"pippo", "paperino"};
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ReplaceMiddleOnceOk(){
            var result = MacroExpansion(new[]{"pippo", "pluto", "minnie"}, new[]{"paperino"}, "pluto");
            var expected = new[]{"pippo", "paperino", "minnie"};
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase(new[]{"pippo", "pluto", "minnie"},new[]{"paperino"},"pluto",new[]{"pippo", "paperino", "minnie"})]
        [TestCase(new[]{"pippo", "pluto", "minnie"},new[]{"paperino", "qui", "quo"},"pluto",new[]{"pippo", "paperino", "qui", "quo", "minnie"})]
        [TestCase(new[]{"pippo", "pluto", "minnie"},new string[]{},"pluto",new[]{"pippo", "minnie"})]
        [TestCase(new[]{"pippo", "pluto", "minnie"},new[]{"paperino"},"paperinik",new[]{"pippo", "pluto", "minnie"})]
        public void TestOk(string[] sequence, string[] newValues, string value, string[]expected){
            var result = MacroExpansion(sequence, newValues, value);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
