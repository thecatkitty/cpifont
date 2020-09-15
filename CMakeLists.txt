cmake_minimum_required(VERSION 3.10)

project("cpitool" CXX C)

include_directories(inc)

add_executable(${PROJECT_NAME}
    app/cpitool.cpp
    app/cmdump.cpp
    app/cmglyph.cpp
    app/print.cpp
    app/stream.cpp
    lib/common.c
    lib/header.c
    lib/entries.c
    lib/fonts.c
    lib/names.c
)
