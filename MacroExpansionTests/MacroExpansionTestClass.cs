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
        [Test]//Accepts all implementations
        public void NullSequenceThrowsANE(){
            Assert.That(() => MacroExpansion(null, new[]{1, 2}, 6).ToArray(),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]//Accepts only implementation throwing at "call moment" (no iterator blocks)
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
        [TestCase(new string[]{},new[]{"paperino"},"pluto",new string[]{})]
        public void TestOk(string[] sequence, string[] newValues, string value, string[]expected){
            var result = MacroExpansion(sequence, newValues, value);
            Assert.That(result, Is.EqualTo(expected));
        }

        private IEnumerable<int> AllPositive(){
            var i = 0;
            while (true){
                yield return ++i;
            }
        }
        [Test]//Better if only this of interest
        public void InfiniteSequenceOKSimple(){
            var result = MacroExpansion(AllPositive(), new[]{-1, -2, -3}, 1);
            var expected = new[]{-1, -2, -3, 2, 3, 4, 5, 6, 7, 8};
            Assert.That(result.Take(10),Is.EqualTo(expected));
        }
        private IEnumerable<int> Expected(int[] rep){
            foreach (var i1 in rep){
                yield return i1;
            }
            var i = 1;
            while (true){
                yield return ++i;
            }
        }
        [Test]
        public void InfiniteSequenceOK([Random(1,1000,5)]int n){
            var result = MacroExpansion(AllPositive(), new[]{-1, -2, -3}, 1);
            Assert.That(result.Take(n),Is.EqualTo(Expected(new[]{-1, -2, -3}).Take(n)));
        }
    }
}
