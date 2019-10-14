#include <iostream>
#include <vector>

using namespace std;

void showArr(vector <int>* arr) 
{
	cout << "\n";
	for (int i = 0; i < arr->size(); i++)
	{
		cout << arr->at(i) << " ";
	}
}

void fillArr(vector <int>* arr, int size)
{
	int num;
	for (int i = 0; i < size; i++)
	{
		cin >> num;
		arr->at(i) = num;
	}
}

int smallestIndexFunc(vector <int>* arr, int size)
{
	int smallest = arr->at(0);
	int smallestIndex = 0;
	for (int i = 0; i < size; i++)
	{
		if (arr->at(i) < smallest)
		{
			smallest = arr->at(i);
			smallestIndex = i;
		}
	}
	return smallestIndex;
}

//сортировка выбором
vector <int> selectionSort(vector <int>* arr, int size)
{
	vector <int> newArr(0);
	int smallest;
	for (int i = 0; i < size; i++)
	{
		smallest = smallestIndexFunc(arr, arr->size());
		newArr.push_back(arr->at(smallest));
		arr->erase(arr->begin() + smallest); //удаление n-ного элемента
	}
	return newArr;
}

void swap(int& first, int& second)
{
	int temp = first;
	first = second;
	second = temp;
}

//гномья сортировка
void gnomeSort(vector <int>* arr, int size)
{
	int i = 1;
	int j = 2;
	while (i < size)
	{
		if (arr->at(i - 1) > arr->at(i)) i = j++; // < для сортировки по возрастанию
		else
		{
			swap(arr->at(i - 1), arr->at(i));
			i--;
			if (i == 0)
			{
				i = j++;
			}
		}
	}
}



//бинарный поиск
bool binarySearch(vector <int>* arr, int value)
{
	int mediumValue = arr->size() / 2;
	int right = arr->size();
	int left = 0;

	while (right - left > 2)
	{
		if (value > arr->at(mediumValue)) 
		{
			left = mediumValue;
			mediumValue = (right - left) / 2;
		}
		else if (value < arr->at(mediumValue))
		{
			right = mediumValue;
			mediumValue = (right - left) / 2;
		}
		else
		{
			return true;
		}
	}

	return false;
}

int main()
{
	setlocale(LC_ALL, "Russian");
	int size = 10;
	vector <int> arr(size);

	fillArr(&arr, size);
	showArr(&arr);
	//arr = selectionSort(&arr, size);
	gnomeSort(&arr, size);
	cout << "\n" <<arr.size();
	showArr(&arr);
	//cout << "\n" << binarySearch(&arr, 17);
}
