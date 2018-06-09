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
        private string[] wordList =
        {
            "der", "die", "und", "in", "den", "von", "zu", "das", "mit", "sich", "des", "auf", "für", "ist", "im",
            "dem", "nicht", "ein", "Die", "eine", "als", "auch", "es", "an", "werden", "aus", "er", "hat", "daß", "sie",
            "nach", "wird", "bei", "einer", "Der", "um", "am", "sind", "noch", "wie", "einem", "über", "einen", "Das",
            "so", "Sie", "zum", "war", "haben"
        };


        [Test]
        public void TestGenerateList()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42, wordList);
            var list = testClass.GenerateList();
            Assert.AreEqual(
                "%./=1:/2)):?'=.die;#&$04.§1&70./§?,4!.32'&!§3°%)2=^&§,:§_0°(der=$1$-!3_::!4!!§^§77-'°§'zu3.#?7,1!68*1/%-*3&&(,:92^1=hat^!*7!(=§(,_$9%%?*24!§56)75=.°°/?/!)793&6/?;;1%,#6auch8-.84%%!/81#,#%786/*.°8eine52-:bei;8;!8-69&,(°:0%3*.1(.3,(7und(?/?=6-'-2^9ein?/8,,#8$-6?766!?;!6#^^9?82!*^§0.'*(,°,?^%&5?war!;*&/°%8/^(°^5'°__(3;.^#war(?.40?,2_9:5§#_/;#!8-?:1#$/3-78:(86°°über*§%7°7;&'&'%!^!)744#,%3=.§!^$.noch16^6*#53584^$375/1834_7(=4';77und*5#%9&6:9!;^?&(2%&-';91=9-°.§=6768?)#46§4-4noch)6&°°-2-0='&%%::*-0&°3*48*==#;%&7'2,-7_4%-(08den1'.-9&°=-.*^=/:§%/((1ein§_§5§&;^55§;65$%9?%=$&)&^°_$:16;'::§8?,%75-3)sie/§§)7,$46/6(#1:/&9-(#'§/01!'(0_7§und§-:98=^°5§$/'#0?42#/wie5.(,1,(28/die-0^!:811(%°.°8.'°/°6_!,82'.$als%73789^4)-_$4sind^83§4%3.:§_%0)&#^?%!1119.147/-'(4§$#2§,9?'?6.6war%§2212(7)3:%?5'§^(=^&1^°0%0*_5-^am4&0,:$4&/9.:^_9=(?,im''&§,§5:'?5'$§)*$0/59.0$(_für&^/§-4_2..7§9^3^0&,#27%,als#50§5&3:'1&36),-4;3&5*1§#$55daß.;°!#,$-^2)-?$:6)'noch%;4=,$%^^3?87&/,&64!#ein!6'§^.:4:§?%92'_*-_36-?;&672*.261^3'(!-%&846°der"
                , list);
        }

        [Test]
        public void TestIsSearchedWordPartOfGenerateList()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42, wordList);
            var list = testClass.GenerateList();
            var searchedWord = testClass.SearchedWord;
            Assert.IsTrue(list.Contains(searchedWord));
        }

        [Test]
        public void TestCheckPassword()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42, wordList);
            testClass.SearchedWord = "FooBarBaz".ToLower();
            Assert.IsTrue(testClass.CheckPassword("FooBarBaz"));
            Assert.IsTrue(testClass.CheckPassword("FooBarbaz"));
            Assert.IsFalse(testClass.CheckPassword("FooBarBazboo"));
            Assert.IsFalse(testClass.CheckPassword("Foo"));
            Assert.IsFalse(testClass.CheckPassword("feFooBarBaz"));
        }

        [Test]
        public void TestGetListOfMatchingChars()
        {
            var testClass = new FindMatchHacking.FindMatchHacking(42, wordList);
            testClass.SearchedWord = "FooBarBaz".ToLower();
            Assert.AreEqual("ao", testClass.GetListOfMatchingChars("Hallo"));
            Assert.AreEqual("", testClass.GetListOfMatchingChars("Nichts"));
            Assert.AreEqual("foo", testClass.GetListOfMatchingChars("Foo"));
            Assert.AreEqual("baof", testClass.GetListOfMatchingChars("Bahnhof"));
            Assert.AreEqual("foobarbaz", testClass.GetListOfMatchingChars("FooBarBaz"));
        }
    }
}