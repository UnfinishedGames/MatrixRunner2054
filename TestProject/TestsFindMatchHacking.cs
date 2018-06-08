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
            Assert.AreEqual("$*%/:*%:&/*(=/*um#=%§-;'!-%°-*%!(#;!'.:=%!§._$/.(_%!#*§=-_&ist/§:§?!.?**!;!!§_!°°?)#!)anderen.'=(°#:!,^(-%$?(.%$&#*^.-:(müßten_!)°!&/!&#=§^$$((:;!!;,&°;/'__&(%!&°^.$,%(''-$#=,Juni^?'°;$$!%°:=#=$°^,%)'#°Auto/)'./,_%/&;_§=.,.^°?,^%#&_*-$.)*:&'.#&°vor&(%(/,?)?._^Sonntag(%°##=^§?,(°,,!(#!,=-_^$'./jedem#§$__#(_$$;(stärker!#)%%_$°%_&__,)#==&.''-=Vater-&(';-(#:?^*,!==%'=!^?(*:=$%.?°°*&°,__;*(§$,_°'%ebenfalls)$!_!&°;;=#$.('oder_§'.':,-,)nehmen.;°;_§.°;%-^.;?°&/;)#°°§;);=$^$,*^!'_($&:$%?Ziel^-/^*#'!/,°,^(/=;,§;?;.,/,$_#?.?bereit)$$$**)?-%_.);^)//='$werde=:#*°?;$?&-°$(:)'*^%_/?'(_/%*!$%&&:),!?!,!%'-,,Das,,§$^($/§%/%__?§*:,')**!°(#$,;?.konnte#%!§&°#$;,&,&=:*%%^*&=)!%--!)&-?°!Sie!?*^^/-_;§§%==-(;:=%Beispiel,'&#:#&:^%Aus?-_!*°::&$#'_°')_%_,?!#°:)'§daran$,.°^^_;/?=§.Ob-^.§;$.'*!?$-/$=_($!:::^'-.°%?)&;!§=:!#^()(,',indem$!..-:&°&.*$(,)!-&/-%:_#-$-)=;?_Sprache;$-#*§;%%^'*-?^/&(#bleibt=)%§#!;*=(;)§!&)§-%;^'-§&?liegt%_%§?;?:''°!^_._-$#=.°$#Monaten=;-§,%.*):%.,/#?;'.%,):!=§;;Seiten''_!=#§?-./*(§*,&)Diskussion"
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