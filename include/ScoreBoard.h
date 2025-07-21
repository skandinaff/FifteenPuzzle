#ifndef SCOREBOARD_H
#define SCOREBOARD_H
#include <vector>

class ScoreBoard {
public:
    void record(int moves);
    void display() const;
private:
    std::vector<int> scores_;
};

#endif
