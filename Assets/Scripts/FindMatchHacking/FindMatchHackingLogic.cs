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

        /// <summary>
        /// The List of words to choose from
        /// </summary>
        private string[] _words;

        /// <summary>
        /// The list of words that are present in the current game
        /// </summary>
        private List<string> currentWords = new List<string>();

        /// <summary>
        /// Used to generate random values, for example to access the word list
        /// </summary>
        private Random _random;

        /// <summary>
        /// The searched word - that is the word the player needs to guess
        /// </summary>
        private string _searchedWord;

        /// <summary>
        /// The final list to show the player
        /// </summary>
        private string _encryptedList = "";

        /// <summary>
        /// Characters used to obfuscate the words in the list
        /// </summary>
        private string _gibberish = "!§$%&/()=?*'#_-:.;,°^0123456789";

        #region Properties Region

        public List<string> CurrentWords
        {
            get { return currentWords; }
        }

        public string SearchedWord
        {
            get { return _searchedWord; }
            set { _searchedWord = value; }
        }

        public string EncryptedList
        {
            get { return _encryptedList; }
        }

        #endregion

        public FindMatchHacking(int seed, string[] words)
        {
            _random = new Random(seed);
            _words = words;
            for (int index = 0; index < _words.Length; ++index)
            {
                _words[index] = _words[index].ToLower().Trim();
            }
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
            _encryptedList = "";
            if (_words != null)
            {
                _searchedWord = _words[_random.Next(0, _words.Length)];
                while (_encryptedList.Length < _listLength)
                {
                    _encryptedList += GetGibberishString(_random.Next(10, 50));
                    _encryptedList += GetNextWord(_words);
                }

                // Now we place our searched word in the list - we surround it with gibberish, just to be sure
                int sizeOfSurrounding = 4;
                var positionForSearchedWord =
                    _random.Next(_listLength - _searchedWord.Length - (sizeOfSurrounding * 2));
                string phraseToBePlaced = GetGibberishString(sizeOfSurrounding)
                                          + _searchedWord
                                          + GetGibberishString(sizeOfSurrounding);
                _encryptedList = _encryptedList.Remove(positionForSearchedWord,
                    phraseToBePlaced.Length + sizeOfSurrounding * 2);
                _encryptedList = _encryptedList.Insert(positionForSearchedWord, phraseToBePlaced);
            }

            return _encryptedList;
        }

        public bool CheckPassword(string password)
        {
            return password.ToLower() == _searchedWord;
        }

        public string GetListOfMatchingChars(string password)
        {
            string result = "";
            foreach (var character in password.ToLower())
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