#include "pch.h"
#include <iostream>

using namespace std;

class DynamicArray
{
private:
	unsigned int size;
	int* arr;

public:
	DynamicArray(unsigned int size = 0)
	{
		this->size = size;
		this->arr = new int[size];
	}

	//добавляет элемент в массив
	void append(int value)
	{
		int newSize = size + 1;
		int* newArr = new int[newSize];

		for (int i = 0; i < size; i++)
		{
			*(newArr + i) = *(arr + i);
		}
		delete[] arr;
		*(newArr + newSize - 1) = value;
		this->size = newSize;
		this->arr = newArr;
	}

	//удаляет последний элемент массива
	void deleteLast()
	{
		int newSize = size - 1;
		int* newArr = new int[newSize];
		for (int i = 0; i < newSize; i++)
		{
			*(newArr + i) = *(arr + i);
		}
		delete[] arr;
		this->size = newSize;
		this->arr = newArr;
	}

	//выводит все элементы массива
	void showArr()
	{
		cout << "\n\n";

		if (size > 0)
		{
			for (int i = 0; i < size; i++)
				cout << *(arr + i) << " ";
		}

		else
		{
			cout << "empty";
		}

		cout << "\n\n";
	}

	//возвращает значение элемента в указанной позиции
	int get(unsigned int position)
	{
		if (position < size && position >= 0)
		{
			return *(arr + position);
		}
		else 
		{ 
			cout << "\n\nError! Can\'t get element from " << position << " position"; 
			cout << "\nLast possible position: " << size - 1 << " called: " << position;
			cout << "\nReturn 0\n";
			return 0;
		}
	}

	//устанавливает новое значение в указанной позиции
	void set(unsigned int position, int value)
	{
		if (position < size && position >= 0 && size > 0)
		{
			*(arr + position) = value;
		}
		else 
		{
			cout << "Error! Can\'t put element to " << position << " position";
			cout << "\nLast possible position: " << size - 1 << " called: " << position;
		}
	}

	int getSize()
	{
		return size;
	}
};



int main()
{
	setlocale(LC_ALL, "Russian");

	int N;
	cin >> N;

	if (N < 2) return 0;

	DynamicArray arr(0);
	for (int i = 0; i < N; i++)
	{
		arr.append(i);
	}
	arr.showArr();

	return 0;
}