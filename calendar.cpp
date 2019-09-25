#include <iostream>

using namespace std;

//разделяет знак по указанному знаку на элементы массива
void splitToInt(string str, int maxwords, int* formattedInt, char ch)
{
	int wordscount = 0;
	string temp = "";

	str += ch;

	for (int i = 0; i < str.length(); i++)
	{
		if (str[i] != ch)
		{
			temp += str[i];
		}
		else
		{
			formattedInt[wordscount++] = atoi(temp.c_str());
			temp = "";
		}
	}
}

//проверяет, является ли год високосным
bool checkLeap(int year)
{
	if (year % 4 == 0 && year % 100 != 0) return true;
	return false;
}
 // проверяет дату на корректность ввода
bool checkDateCorrect(int years, int months, int days)
{
	if (months <= 12 && months > 0)
	{
		if (days < 32 && days > 0)
		{
			if ((months == 1 || months == 3 || months == 5 || months == 7 || months == 8 || months == 10 || months == 12) && days <= 31)
			{
				return true;
			}
			else
			{
				if (months != 2 && days <= 30)
				{
					return true;
				}
				if (checkLeap(years))
				{
					if (months == 2 && days <= 29 || months != 2)
					{
						return true;
					}
				}
				else
				{
					if (months == 2 && days <= 28 || months != 2)
					{
						return true;
					}
				}
				return false;
			}
		}
		return false;
	}
	return false;
}

//подсчитывает кол-во прошедших дней с начала года
int countDays(int years, int months, int days)
{
	int daysCount = days;
	for (int i = 1; i < months; i++)
	{
		if (i == 4 || i == 6 || i == 9 || i == 11) daysCount += 30;
		else if (i == 2 && checkLeap(years)) daysCount += 29;
		else if (i == 2 && !checkLeap(years)) daysCount += 28;
		else daysCount += 31;
	}
	
	daysCount += years * 365 + (years / 4 - 13);

	return daysCount;
}

int main()
{
	setlocale(LC_ALL, "Russian");

	string date;
	const int maxWords = 3;
	int formattedDate[maxWords]; //дата, разбитая на 3 элемента и записанная в int'овском формате (в фомате дд/мм/гггг)

	cin >> date;
	
	splitToInt(date, maxWords, formattedDate, '.');


	string weekDays[7] = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
	int years = formattedDate[2];
	int months = formattedDate[1];
	int days = formattedDate[0];
	bool isLeap = checkLeap(years);

	if (!checkDateCorrect(years, months, days))
	{
		cout << "Введенная дата не явсляется корректной";
		return 0;
	}

	cout << weekDays[(countDays(years, months, days) + 3) % 7];
}
