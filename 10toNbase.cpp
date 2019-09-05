#include <iostream>
#include <string>

using namespace std;

string reverseWord(string word){
    string reversedWord = "";
    int wordSize = word.size() - 1;
    int i;
    for(i = wordSize; i>=0; i--){
        reversedWord = reversedWord + word[i];
    }
    return reversedWord;
}


string convertFromTenToNBase(int numberWithBaseTen, int toBase){
    string newNumber;
    string alphabet[36] = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                           "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                           "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                           "U", "V", "W", "X", "Y", "Z",};
    while (numberWithBaseTen > 0){
        newNumber = newNumber + alphabet[numberWithBaseTen % toBase];
        numberWithBaseTen = numberWithBaseTen / toBase;
    }
    return newNumber;
}


int main()
{
    int withBaseTen, toBase;
    cout << "\nEnter number with base 10:" << endl;
    cin >> withBaseTen;
    cout << "\nEnter base (less than 36)" << endl;
    cin >> toBase;
    if (toBase <= 36){
        cout << "\n" << reverseWord(convertFromTenToNBase(withBaseTen, toBase));
        cout << "\nAgain Y/N:" <<endl;
        string userAnswer;
        cin >> userAnswer;
        if (userAnswer == "Y" or userAnswer == "y"){
            main();
        }
        else{
            return 0;
        }
    }
    else{
        cout << "\nToBase can't be more than 10";
    }
    return 0;
}
