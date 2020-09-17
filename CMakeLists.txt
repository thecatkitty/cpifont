cmake_minimum_required(VERSION 3.10)

project("cpitool" C CXX)

include_directories(inc)
include_directories("${CMAKE_CURRENT_BINARY_DIR}/lib")

add_subdirectory(lib)

add_executable(${PROJECT_NAME}
    app/cpitool.cpp
    app/cmdump.cpp
    app/cmglyph.cpp
    app/print.cpp
    app/stream.cpp
)

target_link_libraries(${PROJECT_NAME} cpifont)
