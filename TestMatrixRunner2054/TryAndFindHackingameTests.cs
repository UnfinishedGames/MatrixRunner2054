using System;
using System.ComponentModel.Design;
using NUnit.Framework;


namespace TestMatrixRunner2054
{
    [TestFixture]
    public class TryAndFindHackingMinigameTests
    {
        [Test]
        public void TestInitializeTextFieldWithGivenRandoms()
        {
            var target = new TypeAndFindMinigameImplenetation(10);
            Assert.AreEqual("Y", target.Text);
        }
        
        [Test]
        public void TestInitializeTextWithSize()
        {
            var target = new TypeAndFindMinigameImplenetation(10, 3);
            Assert.AreEqual("YTT\nSSH\nKLF", target.Text);
        }
        
        [Test]
        public void TestInitializeTextWithSolution()
        {
            var target = new TypeAndFindMinigameImplenetation(10, 3);
            target.SetSolution("IS");
            Assert.AreEqual("YTT\nSSH\nISF", target.Text);
        }
        
        
              
        [Test]
        public void TestInitializeTextWithSolution_Random()
        {
            var target = new TypeAndFindMinigameImplenetation(1, 3);
            target.SetSolution("IST");
            Assert.AreEqual("YTT\nIST\nJYC", target.Text);
        }
    }

    public class TypeAndFindMinigameImplenetation
    {
        private Random random;
        public string Text { get; private set; }
        private string Solution { get; set; }
        private int size;
        
        public TypeAndFindMinigameImplenetation(int seed, int size = 1)
        {
            this.size = size;
            random = new Random(seed);
            for (var counter = 1; counter <= size*size; ++counter)
            {
                Text += ((char)random.Next('A', '[')).ToString();
                if (counter % 3 == 0)
                {
                    Text += "\n";
                }
            }
            Text = Text.TrimEnd('\n');
        }

        public void SetSolution(string solution)
        {
            Solution = solution;
            var index = random.Next(0, Text.Length-Solution.Length);
            var rowIndex = index % (size+1);
            var offset = (size+1) - (rowIndex + solution.Length);
            if (offset < 0)
            {
                index += offset;
            }
            Text = Text.Remove(index, Solution.Length);
            Text = Text.Insert(index, Solution);
        }
    }
}