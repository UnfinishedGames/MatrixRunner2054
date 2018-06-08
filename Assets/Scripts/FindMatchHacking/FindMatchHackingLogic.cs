using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindMatchHacking
{
    public class FindMatchHacking
    {
        /// <summary>
        /// The length of the encrypted list
        /// </summary>
        public int _listLength = 1000;
        
        private List<string> currentWords = new List<string>();

        /// <summary>
        /// The searched word - that is the word the player needs to guess
        /// </summary>
        private string _searchedWord;
        private Random _random;
        /// <summary>
        /// The final list to show the player
        /// </summary>
        private string _encryptedList = "";

        /// <summary>
        /// Characters used to obfuscate the words in the list
        /// </summary>
        private string _gibberish = "!§$%&/()=?*'#_-:.;,°^0123456789";
        
        public List<string> CurrentWords
        {
            get { return currentWords; }
        }
        
        public string SearchedWord
        {
            get { return _searchedWord; }
            set { _searchedWord = value; }
        }

        public FindMatchHacking(int seed)
        {
            _random = new Random(seed);
        }

        public string EncryptedList
        {
            get { return _encryptedList; }
        }
        
        private string GetGibberishString(int length)
        {
            string result = "";
            for (int index = 0; index < length; ++index)
            {
                result += _gibberish[_random.Next(_gibberish.Length)];
            }

            return result;
        }
        
        private string GetNextWord(string[] wordList)
        {
            string nextWord = wordList[_random.Next(wordList.Length)];
            if (nextWord == _searchedWord)
            {
                // Call again - its very unlikely that we get our searched word twice
                nextWord = wordList[_random.Next(wordList.Length)];
            }
            currentWords.Add(nextWord);
            return nextWord;
        }
        
        public string GenerateList()
        {
            var wordList = ReadWordList();
            _encryptedList = "";
            _searchedWord = wordList[_random.Next(0, wordList.Length)];
            while (_encryptedList.Length < _listLength)
            {
                _encryptedList += GetGibberishString(_random.Next(10, 50));
                _encryptedList += GetNextWord(wordList);
            }
            
            // Now we place our searched word in the list - we surround it with gibberish, just to be sure
            int sizeOfSurrounding = 4;
            var positionForSearchedWord = _random.Next(_listLength - _searchedWord.Length - (sizeOfSurrounding * 2));
            string phraseToBePlaced = GetGibberishString(sizeOfSurrounding)
                                      + _searchedWord
                                      + GetGibberishString(sizeOfSurrounding);
            _encryptedList = _encryptedList.Remove(positionForSearchedWord, phraseToBePlaced.Length + sizeOfSurrounding * 2);
            _encryptedList = _encryptedList.Insert(positionForSearchedWord, phraseToBePlaced);
            return _encryptedList;
        }

        public string[] ReadWordList()
        {
            try
            {
                return File.ReadAllLines(@"/storage/projects/MatrixRunner2054/Resources/FindMatchHacking/top1000de_utf-8.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool CheckPassword(string password)
        {
            return password == _searchedWord;
        }

        public string GetListOfMatchingChars(string password)
        {
            string result = "";
            foreach (var character in password)
            {
                if (_searchedWord.Contains(character))
                {
                    result += character;
                }
            }

            return result;
        }
    }
}
