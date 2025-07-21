#include "Board.h"
#include <iostream>
#include <algorithm>

Board::Board() {
    int value = 1;
    for (int i = 0; i < SIZE; ++i) {
        for (int j = 0; j < SIZE; ++j) {
            cells_[i][j] = value % (SIZE*SIZE);
            ++value;
        }
    }
}

void Board::shuffle() {
    std::array<int, SIZE*SIZE> vals{};
    for (int i = 0; i < SIZE*SIZE; ++i) vals[i] = i;
    std::random_device rd;
    std::mt19937 gen(rd());
    do {
        std::shuffle(vals.begin()+1, vals.end(), gen);
        int k=0;
        for(int i=0;i<SIZE;i++)
            for(int j=0;j<SIZE;j++)
                cells_[i][j]=vals[k++];
        // check solvability
        int inversions=0;
        int blankRow=0;
        for(int i=0;i<SIZE*SIZE;i++) {
            if(vals[i]==0) blankRow=(i/SIZE)+1;
            for(int j=i+1;j<SIZE*SIZE;j++) {
                if(vals[i] && vals[j] && vals[i]>vals[j]) inversions++;
            }
        }
        if(((inversions+blankRow)%2)==0) break;
    } while(true);
}

void Board::findZero(int& r,int& c) const {
    for(int i=0;i<SIZE;i++) {
        for(int j=0;j<SIZE;j++) if(cells_[i][j]==0){ r=i;c=j; return; }
    }
}

bool Board::move(char d, bool mute) {
    int r,c; findZero(r,c);
    int nr=r, nc=c;
    switch(d){
        case 'w': nr=r-1; break;
        case 's': nr=r+1; break;
        case 'a': nc=c-1; break;
        case 'd': nc=c+1; break;
        default: return false;
    }
    if(nr<0||nr>=SIZE||nc<0||nc>=SIZE) return false;
    std::swap(cells_[r][c], cells_[nr][nc]);
    return true;
}

bool Board::isSolved() const {
    int value = 1;
    for(int i=0;i<SIZE;i++) {
        for(int j=0;j<SIZE;j++) {
            if(cells_[i][j] != value%(SIZE*SIZE)) return false;
            ++value;
        }
    }
    return true;
}

int Board::countCorrectPrefix(const Board& goal) const {
    int count=0;
    for(int i=0;i<SIZE;i++) {
        for(int j=0;j<SIZE;j++) {
            if(cells_[i][j]==goal.cells_[i][j]) count++; else return count;
        }
    }
    return count;
}

void Board::print(const Board& goal, bool helpOpened) const {
    int rightSec = countCorrectPrefix(goal);
    std::cout << "\n\t---------------------------------\n";
    for(int i=0;i<SIZE;i++) {
        std::cout << "\t|       |       |       |       |\n";
        for(int j=0;j<SIZE;j++) {
            std::cout << "\t|";
            if(cells_[i][j]==0) {
                ;
            } else if(!helpOpened && rightSec >= i*SIZE + j +1) {
                std::cout << "   \x1b[32m" << cells_[i][j] << "\x1b[0m";
            } else {
                std::cout << "   " << cells_[i][j];
            }
        }
        std::cout << "\t|\n";
        std::cout << "\t|       |       |       |       |\n";
        std::cout << "\t---------------------------------\n";
    }
    std::cout << std::endl;
}

