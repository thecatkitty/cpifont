cmake_minimum_required(VERSION 3.10)

project("cpifont" CXX C)

include_directories(inc)

add_executable(${PROJECT_NAME}
    src/cpifont.cpp
)
