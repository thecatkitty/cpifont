cmake_minimum_required(VERSION 3.10)

project("cpifont" CXX C)

include_directories(inc)

add_executable(${PROJECT_NAME}
    src/cpitool.cpp
    src/common.c
    src/header.c
    src/entries.c
    src/fonts.c
    src/names.c
)
