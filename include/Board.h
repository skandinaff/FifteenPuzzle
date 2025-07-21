#ifndef BOARD_H
#define BOARD_H
#include <array>
#include <random>

class Board {
public:
    static constexpr int SIZE = 4;
    Board();
    void shuffle();
    bool move(char direction, bool mute);
    bool isSolved() const;
    int countCorrectPrefix(const Board& goal) const;
    void print(const Board& goal, bool helpOpened) const;
private:
    std::array<std::array<int, SIZE>, SIZE> cells_{};
    void findZero(int& r, int& c) const;
};

#endif
