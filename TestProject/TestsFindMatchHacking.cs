using System;
using System.IO;
using System.Xml.Schema;
using NUnit.Framework;
using System.Linq;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestReadWordListNoThrow()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            Assert.DoesNotThrow(() => testClass.ReadWordList());
        }

        [Test]
        public void TestReadWordList()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            var list = testClass.ReadWordList();
            Assert.AreEqual(1000, list.Length);
        }

        [Test]
        public void TestGenerateList()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            var list = testClass.GenerateList();
            Assert.AreEqual("%./=1:/2)):?'=.um;#&$04.§1&70./§?,4!.32'&!§3°%)2=^&§,:§_0°(ist=$1$-!3_::!4!!§^§77-'°§'anderen3.#?7,1!68*1/%-*3&&(,:92^1=müßten^!*7!(=§(,_$9%%?*24!§56)75=.°°/?/!)793&6/?;;1%,#6Juni8-.84%%!/81#,#%786/*.°8Auto=*.3)6^/=)4°$_36388-69&,(°:0%3*.1(.3,(7vor(?/?=6-'-2^9Sonntag?/8,,#8$-6?766!?;!6#^^9%;4=jedem,$%^stärker!;*&/°%8/^(°^5'°__(3;.^#Vater0(?.40?,2_9:5§#_/;#!8-?:1#$/3-78:(86°°5:*§%7°7;&ebenfalls'%!^!)744#,%3=.oder^$.3.16^6*nehmen3584^$375/1834_7(=4';77§5*5#%9&6:9!;^?&(2%&-Ziel91=9-°.§=6768?)#46§4-436)6&°°-2-bereit'&%%::*-0&°3*48*==#;%werde'2,-7_4%-(08%?1'.-9&°=-.*^=/:§%/((1'6§_§5§&;^55Das65$%9?%=$&)&^°_$:16;'::§8?,%75-3konnte,/§§)7,$46/6(#1:/&9-(#'§/01!'(0_7§Sie§-:98=^°5§$/'#0?42#/Beispiel5.(,1,(28/Aus-0^!:811(%°.°8.'°/°6_!,82'.$daran%73789^4)-_$4Ob^83§4%3.:§_%0)&#^?%!1119.147/-'(4§$#2§,9?'?6.6indem%§2212(7)3:%?5'§^(=^&1^°0%0*_5-^Sprache4&0,:$4&/9.:^_9=(?,bleibt''&§,§5:'?5'$§)*$0/59.0$(_liegt&^/§-4_2..7§9^3^0&,#27%,Monaten#50§5&3:'1&36),-4;3&5*1§#$55Seiten.;°!#,$-^2)-?$:6)'Diskussion"
            , list);
        }
        
        [Test]
        public void TestIsSearchedWordPartOfGenerateList()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            var list = testClass.GenerateList();
            var searchedWord = testClass.SearchedWord;
            Assert.IsTrue(list.Contains(searchedWord));
        } 
        
        [Test]
        public void TestCheckPassword()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            testClass.SearchedWord = "FooBarBaz";
            Assert.IsTrue(testClass.CheckPassword("FooBarBaz"));
            Assert.IsFalse(testClass.CheckPassword("FooBarbaz"));
            Assert.IsFalse(testClass.CheckPassword("FooBarBazboo"));
            Assert.IsFalse(testClass.CheckPassword("Foo"));
            Assert.IsFalse(testClass.CheckPassword("feFooBarBaz"));
        }        
        
        [Test]
        public void TestGetListOfMatchingChars()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42);
            testClass.SearchedWord = "FooBarBaz";
            Assert.AreEqual("ao", testClass.GetListOfMatchingChars("Hallo"));
            Assert.AreEqual("", testClass.GetListOfMatchingChars("Nichts"));
            Assert.AreEqual("Foo", testClass.GetListOfMatchingChars("Foo"));
            Assert.AreEqual("Bao", testClass.GetListOfMatchingChars("Bahnhof"));
            Assert.AreEqual("FooBarBaz", testClass.GetListOfMatchingChars("FooBarBaz"));
        }
    }
}