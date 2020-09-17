cmake_minimum_required(VERSION 3.10)

project("cpitool" C CXX)

include_directories(inc)
include_directories("${CMAKE_CURRENT_BINARY_DIR}/lib")

add_subdirectory(app)
add_subdirectory(lib)
add_subdirectory(dotnet)
