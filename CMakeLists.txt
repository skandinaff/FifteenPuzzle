cmake_minimum_required(VERSION 3.16)
project(FifteenPuzzle LANGUAGES CXX)
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_executable(FifteenPuzzle
    src/main.cpp
    src/Game.cpp
    src/Board.cpp
    src/ScoreBoard.cpp
)

target_include_directories(FifteenPuzzle PRIVATE include)
