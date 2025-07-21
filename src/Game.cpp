#include "Game.h"
#include <iostream>
#include <fstream>

void printWelcome() {
    std::cout << "\n\tWeclome in my fifteen puzzle game!\n\tFor help, feel free to press H!\n\tTo mute the sound press M!\n\tIf you are too weak for that, ESC is for you." << std::endl;
}

static void showPie() {
    std::ifstream f("resources/pie.txt");
    if(f) {
        std::string line;
        while(std::getline(f,line)) {
            std::cout << line << "\n";
        }
    }
}

void Game::run() {
    Board board;
    Board goal; // solved board
    ScoreBoard scores;
    bool mute=false;
    bool helpOpened=false;

    while(true) {
        board.shuffle();
        int moves=0;
        printWelcome();
        board.print(goal, helpOpened);
        scores.display();
        std::cout << "\tPlease, enjoy the game, mate!" << std::endl;
        char key;
        while(std::cin >> key) {
            if(key=='h' || key=='H') {
                helpOpened=true;
                board.print(goal, helpOpened);
                std::cout << "Press any key to continue..." << std::endl;
                std::cin >> key;
                helpOpened=false;
            } else if(key=='m' || key=='M') {
                mute=!mute;
                std::cout << (mute?"\n\tYou turned sound off mate!":"\n\tYou turned sound on mate!") << std::endl;
            } else if(key==27) {
                return; // ESC
            } else if(key=='w'||key=='a'||key=='s'||key=='d') {
                if(board.move(key,mute)) {
                    moves++;
                }
            }
            printWelcome();
            board.print(goal, helpOpened);
            std::cout << "\tYou made " << moves << (moves==1?" move":" moves") << std::endl;
            scores.display();
            if(board.isSolved()) {
                std::cout << "Congratulations, you have won the bloody game!" << std::endl;
                showPie();
                scores.record(moves);
                std::cout << "Another one? (y/n)" << std::endl;
                char ans; std::cin >> ans;
                if(ans=='y' || ans=='Y') break; else return;
            }
        }
    }
}

