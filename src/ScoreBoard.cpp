#include "ScoreBoard.h"
#include <iostream>
#include <algorithm>

void ScoreBoard::record(int moves) {
    scores_.push_back(moves);
    std::sort(scores_.begin(), scores_.end());
    if(scores_.size()>5) scores_.resize(5);
}

void ScoreBoard::display() const {
    if(scores_.empty()) return;
    std::cout << "\n\tBest previous results:\n";
    for(int s : scores_) {
        std::cout << "\t" << s << "\n";
    }
}
