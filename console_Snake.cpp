#include <iostream>
#include <conio.h>
#include <ctime>
#include <cstdlib>
#include <vector>
#include <windows.h>

using namespace std;

class Segment
{
    public:
        Segment(int newX, int newY, string newChar);
        ~Segment();
        int GetX() const {return itsX;};
        int GetY() const {return itsY;};
        string GetChar() const {return itsChar;};
        void SetX(int newX) {itsX = newX;};
        void SetY(int newY) {itsY = newY;};
        void SetChar(char newChar) {itsChar = newChar;};
    private:
        unsigned short int itsX;
        unsigned short int itsY;
        string itsChar;
};

Segment::Segment(int newX, int newY, string newChar)
{
    itsX = newX;
    itsY = newY;
    itsChar = newChar;
}

Segment::~Segment(){}



void DrawScreen();
bool IsSnakeAlive(int, int);
void MoveSegments(int, int);
void IsAppleAte();
void WaitForCharInput(int);


const unsigned short int SIZE_Y = 30;
const unsigned short int SIZE_X = 25;
unsigned short int SnakeSize = 3;
string GameField[SIZE_X][SIZE_Y];
vector <Segment> Segments;
Segment Apple(20, 20, "O");
char direction = '6';


int main()
{
    for(int i=0; i < SIZE_X; i++)
    {
        for(int j=0; j < SIZE_Y; j++)
        {
            if(i == 0 or i == SIZE_X-1 or j == 0 or j == SIZE_Y-1){
                GameField[i][j] = "*";
            }else{
                GameField[i][j] = " ";
            }
        }
    }
    Segments.push_back(Segment(5, 10 ,"@"));
    Segments.push_back(Segment(5, 9,"%"));
    Segments.push_back(Segment(5, 8,"%"));
    Segments.push_back(Segment(5, 7,"%"));
    GameField[Segments[0].GetX()][Segments[0].GetY()] = Segments[0].GetChar();
    GameField[Segments[1].GetX()][Segments[1].GetY()] = Segments[1].GetChar();
    GameField[Segments[2].GetX()][Segments[2].GetY()] = Segments[2].GetChar();
    GameField[Segments[3].GetX()][Segments[3].GetY()] = Segments[3].GetChar();
    GameField[Apple.GetX()][Apple.GetY()] = Apple.GetChar();
    DrawScreen();

    char action;
    unsigned short int PosX = 5;
    unsigned short int PosY = 10;
    
    while(true)
    {
        if(IsSnakeAlive(PosX, PosY)){
            WaitForCharInput(1);
            if(direction == '8'){
                PosX--;
                MoveSegments(PosX, PosY);
                IsAppleAte();
                system("cls");
                DrawScreen();
                direction = '8';
            }else if(direction == '2'){
                PosX++;
                MoveSegments(PosX, PosY);
                IsAppleAte();
                system("cls");
                DrawScreen();
                direction = '2';
            }else if(direction == '6'){
                PosY++;
                MoveSegments(PosX, PosY);
                IsAppleAte();
                system("cls");
                DrawScreen();
                direction = '6';
            }else if(direction == '4'){
                PosY--;
                MoveSegments(PosX, PosY);
                IsAppleAte();
                system("cls");
                DrawScreen();
                direction = '4';
            }
        }else{
            system("cls");
            cout << "GAME OVER!";
            break;
        }
    }
}


void DrawScreen()
{
    for(int i=0; i < SIZE_X; i++)
    {
        for(int j=0; j < SIZE_Y; j++)
        {
            cout << " " << GameField[i][j];
        }
        cout << endl;
    }
}

bool IsSnakeAlive(int Location_X, int Location_Y)
{
    if(Location_X == 0 or Location_X == SIZE_X or Location_Y == 0 or Location_Y == SIZE_Y){
        return false;
    }else{
        for(int i = 1; i < SnakeSize; i++)
        {
            if(Segments[0].GetX() == Segments[i].GetX() and Segments[0].GetY() == Segments[i].GetY()){
                return false;
            }
        }
    }
}

void MoveSegments(int PosX, int PosY)
{
    GameField[Segments[SnakeSize].GetX()][Segments[SnakeSize].GetY()] = " ";
    for(int i = SnakeSize; i > 0; i--)
    {
        Segments[i].SetX(Segments[i-1].GetX());
        Segments[i].SetY(Segments[i-1].GetY());
        GameField[Segments[i].GetX()][Segments[i].GetY()] = Segments[i].GetChar();
    }
    Segments[0].SetX(PosX);
    Segments[0].SetY(PosY);
    GameField[Segments[0].GetX()][Segments[0].GetY()] = Segments[0].GetChar();
}

void IsAppleAte()
{
    if(Segments[0].GetX() == Apple.GetX() and Segments[0].GetY() == Apple.GetY()){
        Segments.push_back(Segment(Segments[SnakeSize].GetX(), Segments[SnakeSize].GetY(), "%"));
        SnakeSize += 1;
        srand(time(0));
        Apple.SetY(rand()%28+1);
        Apple.SetX(rand()%23+1);
        GameField[Apple.GetX()][Apple.GetY()] = Apple.GetChar();
    }
}

void WaitForCharInput(int seconds){
    while(seconds != 0)
    {
        if(kbhit()){
            direction = getch();
            break;
        }
        Sleep(150);
        seconds--;
    }
}
