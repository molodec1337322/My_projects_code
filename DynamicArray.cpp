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
		unsigned int newSize = size + 1;
		int* newArr = new int[newSize];

		for (unsigned int i = 0; i < size; i++)
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
		unsigned int newSize = size - 1;
		int* newArr = new int[newSize];
		for (unsigned int i = 0; i < newSize; i++)
		{
			*(newArr + i) = *(arr + i);
		}
		delete[] arr;
		this->size = newSize;
		this->arr = newArr;
	}

	//удаляет элемент из массива
	void remove(unsigned int position)
	{
		if (position < size)
		{
			unsigned int newSize = size - 1;
			int* newArr = new int[newSize];
			for (unsigned int i = 0; i < size; i++)
			{
				if (i < position) *(newArr + i) = *(arr + i);
				else if (i > position) *(newArr + i - 1) = *(arr + i);
			}
			delete[] arr;
			this->size = newSize;
			this->arr = newArr;
		}
		else
		{
			cout << "\n\nError! Can\'t remove element from " << position << " position";
			cout << "\nLast possible position: " << size - 1 << " called: " << position;
		}
	}
	
	//очищает массив
	void clear()
	{
		delete[] arr;
		this->size = 0;
		this->arr = new int[size];
	}

	//выводит все элементы массива
	void showArr()
	{
		cout << "\n\n";

		if (size > 0)
		{
			for (unsigned int i = 0; i < size; i++)
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
		if (position < size)
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
			cout << "\n\nError! Can\'t put element to " << position << " position";
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
