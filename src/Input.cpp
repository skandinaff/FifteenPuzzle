#include "Input.h"
#include <iostream>
#ifdef _WIN32
#include <conio.h>
char getch(){
    return _getch();
}
void clearScreen(){
    system("cls");
}
#else
#include <termios.h>
#include <unistd.h>
char getch(){
    termios oldt;
    tcgetattr(STDIN_FILENO, &oldt);
    termios newt = oldt;
    newt.c_lflag &= ~(ICANON | ECHO);
    tcsetattr(STDIN_FILENO, TCSANOW, &newt);
    char c;
    read(STDIN_FILENO, &c, 1);
    tcsetattr(STDIN_FILENO, TCSANOW, &oldt);
    return c;
}
void clearScreen(){
    std::cout << "\033[2J\033[H";
}
#endif
