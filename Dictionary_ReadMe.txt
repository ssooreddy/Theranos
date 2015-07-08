							Dictionary
..........................................................................................................................

1. Dictionary is stored using trie data structure.
2. Trie is implemented using dictionary.
 	Trie
	{
	   char value;
	   bool end;
	   Dictionary<char, Trie> children;
	}
3. Whenever we add a word from dictionary to trie, we check if the alphabet exist and proceed to next alphabet to see if it is present in its children and so on. 
If the letter is not found, then we add the new trie with this letter to the children.
Once we reach the end of the word, the last letter's end flag will be set to true, marking as an end of the word.

4. For spell checking the file with our dictionary, we traverse the letters of the words in the file with our existing trie.

5. Adding each word costs the O(length of word), checking also costs O(length of word).

6. For very large files, we can use multithreading by load each thread with dictionary trie and checking for multiple words simultaneously.
