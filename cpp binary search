#include <iostream>
#include <algorithm>

using namespace std;


void ArraySorting(int *arr, int arrLenth)

{
    int temp;
    for (int i=0; i<arrLenth; i++){
        for (int j=0; j<arrLenth; j++){
            if (arr[j] >= arr[j+1]){
                temp = arr[j+1];
                arr[j+1] = arr[j];
                arr[j] = temp;
            }
        }
    }
}


bool BinarySearching(int key, int *arr, int arrLength)

{
    bool flag = false;
    int leftBd = 0;
    int rightBd = arrLength - 1;
    int mid;

    while ((leftBd <= rightBd) and (!flag)){
        mid = (leftBd + rightBd) / 2;

        if (arr[mid] == key){
            flag = true;
            return flag;
        }
        if (arr[mid] > key){rightBd = mid - 1;}
        if (arr[mid] < key){leftBd = mid + 1;}
    }
    return flag;
}


int main()
{
    int arrLength;
    cout << "Enter number of array elements: " << endl;
    cin >> arrLength;

    int arr[arrLength];
    for(int i = 0; i < arrLength; i++){
        cout << "Enter element number " << i + 1 << ": " << endl;
        cin >> arr[i];
    }

    //sort (arr, arr + arrLength);
    ArraySorting(arr, arrLength);

    int key;
    cout << endl << "Enter key : " << endl;
    cin >> key;

    bool isKeyFound = BinarySearching(key, arr, arrLength);
    if (isKeyFound){
        cout << "key is found";
    }else{
        cout << "key isn't found";
    }

    return 0;
}
